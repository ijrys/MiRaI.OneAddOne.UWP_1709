using MiRaI.OneAddOne.Creaters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace MiRaI.OneAddOne {
	/// <summary>
	/// 可用于自身或导航至 Frame 内部的空白页。
	/// </summary>
	public sealed partial class SelecterPage : Page {

		public Type FromPage;
		public User _user;

		public delegate void EndTestFun(History res);

		public class Info {
			public Type FromPage;
			public User User;
			/// <summary>
			/// 跳转到此页时显示的信息
			/// </summary>
			public string Message;
		}


		#region 构造方法与展示方法
		public SelecterPage() {
			this.InitializeComponent();
			this.SizeChanged += SelecterPage_SizeChanged;
		}

		private void SelecterPage_SizeChanged(object sender, SizeChangedEventArgs e) {
			double w = e.NewSize.Width;
			w = w / 5.0 * 3.0 - 16;
			StyleWidth.Width = w;
		}

		/// <summary>
		/// 开始选择答题
		/// </summary>
		/// <param name="parentWindow"></param>
		/// <param name="fromPage"></param>
		/// <param name="user"></param>
		//private void Select(Type fromPage, User user) {
		//	if (_user != null) {
		//		_user.LevelChanged -= UserLevelChangedFun;
		//	}
		//	FromPage = fromPage;
		//	_user = user;

		//	user.LevelChanged += UserLevelChangedFun;

		//	SelList.ItemsSource = StoreRoom.GetCreaters(user);
		//	labUserName.Text = user.NickName;

		//}

		protected override void OnNavigatedTo(NavigationEventArgs e) {
			if (_user != null) {
				_user.LevelChanged -= UserLevelChangedFun;
			}

			Info info = e.Parameter as Info;
			if (info == null) {
				ShowMsg("错误的启动信息！");

			}
			else {
				FromPage = info.FromPage;
				_user = info.User;
				_user.LevelChanged += UserLevelChangedFun;

				SelList.ItemsSource = StoreRoom.GetCreaters(_user);
				labUserName.Text = _user.NickName;

				if (info.Message != null) {
					ShowMsg(info.Message);
				}
			}

			//开始动画
			foreach (var item in this.Resources.Values) {
				Storyboard story = item as Storyboard;
				if (story != null) {
					story.Begin();
				}
			}
		}

		/// <summary>
		/// 用户等级改变时方法
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void UserLevelChangedFun(User sender, object[] args) {
			SelList.ItemsSource = StoreRoom.GetCreaters(_user);
		}

		#endregion

		

		/// <summary>
		/// 返回
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Back_Click(object sender, RoutedEventArgs e) {
			Frame rootFrame = Window.Current.Content as Frame;
			if (rootFrame != null && rootFrame.CanGoBack) rootFrame.GoBack();
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			//QuestionPage qpage = new QuestionPage(_contentWindow);
			//_contentWindow.NavgateToPage(qpage);
		}

		private void SelList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			ICreaterUi cui = SelList.SelectedItem as ICreaterUi;
			if (cui == null) return;

			_nowcreaterUi = cui;
			if (!string.IsNullOrEmpty(cui.ShowMwssage)) {
				ShowMsg(cui.ShowMwssage, true);
			}
			else if (cui.CanSelNum) {
				SelNum();
			}
			SelList.SelectedItem = null;
			//cui.Creater;
		}



		private ICreaterUi _nowcreaterUi;

		/// <summary>
		/// 进行数量选择
		/// </summary>
		private void SelNum() {
			//try {
			//	MsgConter.Child = NumSel;

			//}
			//catch (Exception ex) {
			//	Debug.WriteLine(ex);
			//}

			NumSel.Visibility = Visibility.Visible;
			MsgShow.Visibility = Visibility.Collapsed;

			MsgBorder.Visibility = Visibility.Visible;
		}
		private void btnMsgCan(object sender, RoutedEventArgs e) {
			MsgBorder.Visibility = Visibility.Collapsed;
		}

		private void btnSelOK(object sender, RoutedEventArgs e) {
			ICreaterUi cui = _nowcreaterUi;
			if (cui == null) return;

			MsgBorder.Visibility = Visibility.Collapsed;
			StartTest();
		}

		/// <summary>
		/// 信息显示完成后是否要显示数量选择
		/// </summary>
		private bool _dotest;
		/// <summary>
		/// 显示信息
		/// </summary>
		/// <param name="msg"></param>
		private void ShowMsg(string msg, bool selnum) {
			//MsgConter.Child = MsgShow;
			labMsg.Text = msg;

			_dotest = selnum;

			NumSel.Visibility = Visibility.Collapsed;
			MsgShow.Visibility = Visibility.Visible;

			MsgBorder.Visibility = Visibility.Visible;
		}
		public void ShowMsg(string msg) {
			ShowMsg(msg, false);
		}

		private void btnMsgOK(object sender, RoutedEventArgs e) {
			if (_dotest && _nowcreaterUi != null && _nowcreaterUi.CanSelNum) {
				SelNum();
			}
			else {
				MsgBorder.Visibility = Visibility.Collapsed;
				if (_dotest) StartTest();
			}
		}

		private void StartTest() {
			//QuestionPage qpage = new QuestionPage(_contentWindow);
			QuestionPage.Info info = _nowcreaterUi.DefaultStartInfo;
			if (info == null) {
				info = new QuestionPage.Info(_nowcreaterUi);
				info.maxTime = 3600;
			}
			if (_nowcreaterUi.CanSelNum) info.questionNum = (int)(sliNum.Value);

			info.testName = _nowcreaterUi.Name;
			info.action = (History res, QuestionPage qpage) => {
				//_contentWindow.NavgateToPage(this);
				//if (_nowcreaterUi.EndTestFun != null) {
				//	_nowcreaterUi.EndTestFun.Invoke(res, this);
				//}
				_user.AddHistory(res);
			};

			Frame rootFrame = Window.Current.Content as Frame;
			if (rootFrame != null) {
				rootFrame.Navigate(typeof(QuestionPage), info);
			}
		}
	}
}
