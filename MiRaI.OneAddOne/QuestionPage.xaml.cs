using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MiRaI.OneAddOne.Creaters;
using MiRaI.OoeAddOne.BasicType;
using System.Text;
using System.Diagnostics;
using Windows.UI.Xaml.Media.Animation;
using System.Threading.Tasks;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace MiRaI.OneAddOne {
	/// <summary>
	/// 可用于自身或导航至 Frame 内部的空白页。
	/// </summary>
	public sealed partial class QuestionPage : Page {

		public delegate void EndTestAction(History res, QuestionPage questionPage);

		public class Info {
			/// <summary>
			/// 最大正确数
			/// </summary>
			public int maxAcNum;
			/// <summary>
			/// 最大错误数
			/// </summary>
			public int maxWaNum;
			/// <summary>
			/// 最大用时（秒）
			/// </summary>
			public int maxTime;
			/// <summary>
			/// 可以跳转到已做过的题
			/// </summary>
			public bool canBack;
			/// <summary>
			/// 题目总数，不设置为-1
			/// </summary>
			public int questionNum;
			/// <summary>
			/// 测试名
			/// </summary>
			public string testName;

			/// <summary>
			/// 由启动页面设置，不在Creater中指定
			/// </summary>
			public EndTestAction action;

			public ICreaterUi creater;

			public Info(ICreaterUi creater) {
				maxAcNum = -1;
				maxWaNum = -1;
				maxTime = 65535;
				canBack = false;
				questionNum = -1;
				this.creater = creater;
			}
		}
		//public class Resault {
		//	/// <summary>
		//	/// 正确数
		//	/// </summary>
		//	public int acNum = 0;
		//	/// <summary>
		//	/// 错误数
		//	/// </summary>
		//	public int waNum = 0;
		//	/// <summary>
		//	/// 用时
		//	/// </summary>
		//	public int useTime = 0;
		//	/// <summary>
		//	/// 最大连续正确数
		//	/// </summary>
		//	public int maxStreaks = 0;
		//	/// <summary>
		//	/// 测试名
		//	/// </summary>
		//	public string testName;
		//	/// <summary>
		//	/// 测试日期
		//	/// </summary>
		//	public DateTime testDate;

		//	/// <summary>
		//	/// 正确数的字符串
		//	/// </summary>
		//	public string AcNumStr { get { return acNum.ToString(); } }
		//	/// <summary>
		//	/// 错误数的字符串
		//	/// </summary>
		//	public string WaNumStr { get { return waNum.ToString(); } }
		//	/// <summary>
		//	/// 总题目数的字符串
		//	/// </summary>
		//	public string SumStr {
		//		get { return (acNum + waNum).ToString(); }
		//	}
		//	/// <summary>
		//	/// 使用时间
		//	/// </summary>
		//	public int UseTime { get { return useTime; } }
		//	/// <summary>
		//	/// 最大正确数的字符串
		//	/// </summary>
		//	public string MaxStreaksStr { get { return maxStreaks.ToString(); } }
		//	/// <summary>
		//	/// 测试名
		//	/// </summary>
		//	public string TestName { get { return testName; } }
		//	/// <summary>
		//	/// 测试日期
		//	/// </summary>
		//	public DateTime TestDate { get { return testDate; } }
		//	/// <summary>
		//	/// 正确率的字符串
		//	/// </summary>
		//	public string AcProportionsStr {
		//		get {
		//			int sum = acNum + waNum;
		//			if (sum == 0) return "N/A";
		//			Debug.WriteLine(sum);
		//			Debug.WriteLine(acNum);
		//			Debug.WriteLine(acNum * 100.0 / sum);
		//			return (acNum * 100.0 / sum).ToString("00.00") + "%";
		//		}
		//	}
		//	/// <summary>
		//	/// 使用时间的字符串
		//	/// </summary>
		//	public string UseTimeStr {
		//		get {
		//			return string.Format("{0} : {1:00}", useTime / 60, useTime % 60);
		//		}
		//	}
		//}


		public QuestionPage() {
			Window.Current.Closed += Current_Closed; //ParentWindow_Closing;
			this.InitializeComponent();
		}

		private void Current_Closed(object sender, Windows.UI.Core.CoreWindowEventArgs e) {
			EndTest();
		}


		#region 信息、状态
		/// <summary>
		/// 判定信息
		/// </summary>
		Info paddingInfo;
		/// <summary>
		/// 指示是否在运行
		/// </summary>
		bool running;
		/// <summary>
		/// running锁
		/// </summary>
		object runningobj = new object();

		/// <summary>
		/// 当前做对数量
		/// </summary>
		int nowacnum;
		/// <summary>
		/// 当前做错数量
		/// </summary>
		int nowwanum;
		/// <summary>
		/// 当前已做数量
		/// </summary>
		int nownum;

		private void FreshFlags() {
			if (paddingInfo.questionNum != -1) { //按照当前问题数量显示
				labFlagShow.Text = string.Format("{0} / {1}", nownum, paddingInfo.questionNum);
			}
			else if (paddingInfo.maxAcNum != -1) { //按照最大正确数显示
				labFlagShow.Text = string.Format("{0} / {1}", nowacnum, paddingInfo.maxAcNum);
			}
			else if (paddingInfo.maxWaNum != -1) { //按照最大错误数显示
				labFlagShow.Text = string.Format("{0} / {1}", nowwanum, paddingInfo.maxWaNum);
			}
			else { //时间决定
				labFlagShow.Text = string.Format("{0} / {1}", nowacnum, nowwanum);
			}
		}

		Storyboard AcStory, WaStory;
		#endregion

		#region Input
		private void AddChar(char c) {
			//•+-×÷
			switch (c) {
				case '+':
					c = '+';
					break;
				case '-':
					c = '-';
					break;
				case '*':
					c = '×';
					break;
				case '/':
					c = '÷';
					break;
				case '·':
					c = '.';
					break;
				default:
					break;
			}
			txtAnswer.Text += c;
			//Console.WriteLine($"Addc {c}");
		}
		private void DelChar() {
			if (txtAnswer.Text != "") {
				txtAnswer.Text = txtAnswer.Text.Substring(0, txtAnswer.Text.Length - 1);
			}
		}

		//private void txtAnswer_KeyDown(object sender, KeyEventArgs e) {
		//	OnKeyDown(e);
		//}
		//protected override void OnKeyDown(KeyEventArgs e) {
		//	bool handed = false;
		//	//Console.WriteLine(e.Key);
		//	if (e.Key == Key.Enter) {
		//		JudgeError();
		//		handed = true;
		//	} else if (e.Key == Key.Back) {
		//		DelChar();
		//		JudgeAccept();
		//		handed = true;
		//	} else if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift) {
		//		if (e.Key == Key.D8) AddChar('*');
		//		else if (e.Key == Key.D9) AddChar('(');
		//		else if (e.Key == Key.D0) AddChar(')');
		//		handed = true;
		//	} else {
		//		int kv = (int)e.Key;
		//		if (kv >= 34 && kv <= 43) {
		//			AddChar((char)(kv - 34 + '0'));
		//			handed = true;
		//		} else if (kv >= 74 && kv <= 83) {
		//			AddChar((char)(kv - 74 + '0'));
		//			handed = true;
		//		} else {
		//			switch (e.Key) {
		//				case Key.Multiply:
		//					AddChar('*');
		//					handed = true;
		//					break;
		//				case Key.Add:
		//				case Key.OemPlus:
		//					AddChar('+');
		//					handed = true;
		//					break;
		//				case Key.Subtract:
		//				case Key.OemMinus:
		//					AddChar('-');
		//					handed = true;
		//					break;
		//				case Key.Decimal:
		//				case Key.OemPeriod:
		//					AddChar('.');
		//					handed = true;
		//					break;
		//				case Key.Divide:
		//				case Key.OemQuestion:
		//					AddChar('/');
		//					handed = true;
		//					break;
		//				case Key.LeftShift:
		//				case Key.RightShift:
		//					handed = false;
		//					break;
		//				default:
		//					handed = true;
		//					break;
		//			}
		//		}
		//	}
		//	e.Handled = handed;

		//}
		protected override void OnKeyDown(KeyRoutedEventArgs e) {
			if (e.Key == Windows.System.VirtualKey.Enter) {
				btnSNext_Click(null, null);
				e.Handled = true;
			}
		}
		private void txtAnswer_KeyDown(object sender, KeyRoutedEventArgs e) {
			OnKeyDown(e);
		}

		private void btnDel_Click(object sender, RoutedEventArgs e) {
			DelChar();
		}

		private void btnNumber_Click(object sender, RoutedEventArgs e) {
			Button btn = sender as Button;
			char c = (btn.Content as string)[0];
			AddChar(c);
		}

		/// <summary>
		/// 获取一个标准的显示字符串，用于显示用户输入并过滤掉无关内容
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		private string GetProfessionalText(string str) {
			bool needchange = false;
			//•+-×÷
			foreach (var item in str) {
				if (!(item >= '0' && item <= '9') &&
					item != '.' &&
					item != '+' &&
					item != '-' &&
					item != '×' &&
					item != '÷' &&
					item != '(' &&
					item != ')') {
					needchange = true;
					break;
				}
			}
			if (!needchange) return null;
			StringBuilder sb = new StringBuilder(str.Length);
			foreach (var item in str) {
				if ((item >= '0' && item <= '9') ||
					item == '.' ||
					item == '+' ||
					item == '-' ||
					item == '×' ||
					item == '÷' ||
					item == '(' ||
					item == ')') {
					sb.Append(item);
					continue;
				}
				switch (item) {
					case '+':
						sb.Append('+');
						break;
					case '-':
						sb.Append('-');
						break;
					case '*':
						sb.Append('×');
						break;
					case '/':
						sb.Append('÷');
						break;
					case '·':
						sb.Append('.');
						break;
					default:
						break;
				}
			}
			return sb.ToString();
		}
		private void txtAnswer_TextChanged(object sender, TextChangedEventArgs e) {
			//Console.WriteLine(txtAnswer.Text);
			string estr = GetProfessionalText(txtAnswer.Text);
			if (estr != null) {
				txtAnswer.Text = estr;
			}
			txtAnswer.Select(txtAnswer.Text.Length, 0);
		}
		#endregion

		#region 计时
		/// <summary>
		/// 当前时间
		/// </summary>
		int nowtime = 0;
		int tmaxtime;
		private void FreshTime() {
			labTime.Text = string.Format("{0} : {1}", nowtime / 60, nowtime % 60);
		}

		private void TimeKeepDoFun() {
			Task.Delay(1000);
			lock (runningobj) {
				if (!running) return;
			}
			if (nowtime > tmaxtime) {
				//Dispatcher.BeginInvoke(new Action(EndTest), null);
				EndTest();
				return;
			}
			//Thread.Sleep(1000);
			nowtime++;

			//labTime.Dispatcher.BeginInvoke(new Action(FreshTime), null);
			FreshTime();
			//Console.WriteLine(string.Format("time : {0}", nowtime));
			TimeKeepDoFun();
		}
		private void Timekeep() {
			//Thread timeThread = new Thread(TimeKeepDoFun);
			//timeThread.Start();
			TimeKeepDoFun();
		}
		#endregion

		#region EndTest
		EndTestAction etAction;
		History res;
		/// <summary>
		/// 结束测试
		/// </summary>
		public void EndTest() {
			lock (runningobj) {
				if (!running) {
					Debug.WriteLine("hadend");
					return;
				}
				Debug.WriteLine("now end");
				running = false;

			}
			res = new History();

			res.TestDate = DateTime.Now;
			res.TestName = paddingInfo.testName;
			res.UseTime = nowtime;
			res.WaNum = nowwanum;
			res.AcNum = nowacnum;
			res.MaxStreaks = -1;

			EShowResault();
		}
		/// <summary>
		/// 显示测试结果
		/// </summary>
		private void EShowResault() {
			//labShow1.Text = string.Format ("{0} : {1}", res.useTime / 60, res.useTime % 60);
			//int qnums = res.acNum + res.waNum;
			//labShow2.Text = qnums.ToString ();
			//labShow3.Text = res.acNum.ToString ();
			//labShow4.Text = res.waNum.ToString ();
			//labShow5.Text = (res.acNum * 100.0 / qnums).ToString ("00.0");
			ResaultPanel.DataContext = res;

			ResaultPanel.Visibility = Visibility.Visible;
			if (etAction != null) etAction.Invoke(res, this);
		}

		private void btnBack_Click(object sender, RoutedEventArgs e) {
			Window.Current.Closed -= Current_Closed;

			//_contentWindow.NavgateToPage(this);
			if (paddingInfo.creater.EndTestFun != null) {
				paddingInfo.creater.EndTestFun.Invoke(res, this);
			}

		}

		private void btnEndTest_Click(object sender, RoutedEventArgs e) {
			EndTest();
		}
		#endregion

		#region 题目判定
		/// <summary>
		/// 所有错题
		/// </summary>
		List<IQuestionAble> waques;
		/// <summary>
		/// 当前题目
		/// </summary>
		IQuestionAble nowques;

		/// <summary>
		/// 判定
		/// </summary>
		public void Judge() {
			string ure = txtAnswer.Text;
			CheckResaults cre = nowques.Check(ure);
			nownum++;
			if (cre.resault == CheckResaultEnum.Accept) {
				JudgeAccept();
			}
			else {
				JudgeError();
			}
			if (running) {
				FreshFlags();
				nowques = paddingInfo.creater.Creater.NextQuestion();
				labQuestion.Text = nowques.Description;
				txtAnswer.Text = "";
			}
		}

		/// <summary>
		/// 判定正确
		/// </summary>
		public void JudgeAccept() {
			AcStory.Stop();
			WaStory.Stop();
			AcStory.Begin();

			nowacnum++;
			if (JIfNeedStop()) {
				EndTest();
			}
		}
		/// <summary>
		/// 判定错误
		/// </summary>
		public void JudgeError() {
			//waques.Add(nowques);
			waques.Add(nowques);
			AcStory.Stop();
			WaStory.Stop();
			WaStory.Begin();

			nowwanum++;
			if (JIfNeedStop()) {
				EndTest();
			}
		}

		/// <summary>
		/// 判定是否需要停止
		/// </summary>
		/// <returns></returns>
		public bool JIfNeedStop() {
			if (paddingInfo.questionNum != -1 &&
				nownum >= paddingInfo.questionNum) return true;
			if (paddingInfo.maxAcNum != -1 &&
				nowacnum >= paddingInfo.maxAcNum) return true;
			if (paddingInfo.maxWaNum != -1 &&
				nowwanum >= paddingInfo.maxWaNum) return true;
			return false;
		}
		#endregion

		private void DoTestInit() {
			running = true;
			nowacnum = 0;
			nowwanum = 0;
			nownum = 0;
			nowtime = 0;
			tmaxtime = paddingInfo.maxTime;

			ResaultPanel.Visibility = Visibility.Collapsed;

			AcStory = Resources["ACStory"] as Storyboard;
			WaStory = Resources["WAStory"] as Storyboard;


			waques = new List<IQuestionAble>();
			nowques = paddingInfo.creater.Creater.NextQuestion();
			labQuestion.Text = nowques.Description;
			txtAnswer.Text = null;
			labTestName.Text = paddingInfo.testName;

			FreshFlags();
			Timekeep();
		}

		//public bool DoTest(Info info, EndTestAction action) {
		//	if (info == null || action == null || info.creater == null) return false;
		//	etAction = action;
		//	paddingInfo = info;
		//	waques = new List<IQuestionAble>();

		//	DoTestInit();

		//	//ParentWindow.NavgateToPage(this);
		//	txtAnswer.Focus(FocusState.Pointer);
		//	return true;
		//}
		protected override void OnNavigatedTo(NavigationEventArgs e) {
			Info info = e.Parameter as Info;
			if (info != null) {
				EndTestAction action = info.action;
				etAction = action;
				paddingInfo = info;
				waques = new List<IQuestionAble>();

				DoTestInit();

				//ParentWindow.NavgateToPage(this);
				txtAnswer.Focus(FocusState.Pointer);
			}
		}


		private void btnSNext_Click(object sender, RoutedEventArgs e) {
			Judge();
		}

		#region Message
		public delegate void MessageFunctionType();
		public MessageFunctionType btnMsgOked;
		public MessageFunctionType btnMsgCaned;
		public MessageFunctionType btnMsgEnded;

		public void ShowMessage(string message) {
			labMsg.Text = message;
			MsgShow.Visibility = Visibility.Visible;
		}

		private void btnMsgCan(object sender, RoutedEventArgs e) {
			if (btnMsgCaned != null) {
				btnMsgCaned.Invoke();
			}
			if (btnMsgEnded != null) {
				btnMsgEnded.Invoke();
			}
			MsgShow.Visibility = Visibility.Collapsed;
		}

		private void btnMsgOK(object sender, RoutedEventArgs e) {
			if (btnMsgOked != null) {
				btnMsgOked.Invoke();
			}
			if (btnMsgEnded != null) {
				btnMsgEnded.Invoke();
			}
			MsgShow.Visibility = Visibility.Collapsed;
		}
		#endregion
	}
}
