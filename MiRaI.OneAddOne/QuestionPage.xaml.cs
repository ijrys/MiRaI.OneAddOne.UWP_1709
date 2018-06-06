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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace MiRaI.OneAddOne
{
	/// <summary>
	/// 可用于自身或导航至 Frame 内部的空白页。
	/// </summary>
	public sealed partial class QuestionPage : Page
	{
		public class Info
		{
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

			public ICreateQuestionAble creater;

			public Info(ICreateQuestionAble creater)
			{
				maxAcNum = -1;
				maxWaNum = -1;
				maxTime = 65535;
				canBack = false;
				questionNum = -1;
				this.creater = creater;
			}
		}
		public class Resault
		{
			/// <summary>
			/// 正确数
			/// </summary>
			public int acNum = 0;
			/// <summary>
			/// 错误数
			/// </summary>
			public int waNum = 0;
			/// <summary>
			/// 用时
			/// </summary>
			public int useTime = 0;
			/// <summary>
			/// 最大连续正确数
			/// </summary>
			public int maxStreaks = 0;
			/// <summary>
			/// 测试名
			/// </summary>
			public string testName;
			/// <summary>
			/// 测试日期
			/// </summary>
			public DateTime testDate;

			/// <summary>
			/// 正确数的字符串
			/// </summary>
			public string AcNumStr { get { return acNum.ToString(); } }
			/// <summary>
			/// 错误数的字符串
			/// </summary>
			public string WaNumStr { get { return waNum.ToString(); } }
			/// <summary>
			/// 总题目数的字符串
			/// </summary>
			public string SumStr {
				get { return (acNum + waNum).ToString(); }
			}
			/// <summary>
			/// 使用时间
			/// </summary>
			public int UseTime { get { return useTime; } }
			/// <summary>
			/// 最大正确数的字符串
			/// </summary>
			public string MaxStreaksStr { get { return maxStreaks.ToString(); } }
			/// <summary>
			/// 测试名
			/// </summary>
			public string TestName { get { return testName; } }
			/// <summary>
			/// 测试日期
			/// </summary>
			public DateTime TestDate { get { return testDate; } }
			/// <summary>
			/// 正确率的字符串
			/// </summary>
			public string AcProportionsStr {
				get {
					int sum = acNum + waNum;
					if (sum == 0) return "N/A";
					//Console.WriteLine(sum);
					//Console.WriteLine(acNum);
					//Console.WriteLine(acNum * 100.0 / sum);
					return (acNum * 100.0 / sum).ToString("00.00") + "%";
				}
			}
			/// <summary>
			/// 使用时间的字符串
			/// </summary>
			public string UseTimeStr {
				get {
					return string.Format("{0} : {1:00}", useTime / 60, useTime % 60);
				}
			}
		}
		public QuestionPage()
		{
			this.InitializeComponent();
		}
	}
}
