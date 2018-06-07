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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace MiRaI.OneAddOne {
	/// <summary>
	/// 可用于自身或导航至 Frame 内部的空白页。
	/// </summary>
	public sealed partial class LoginPage : Page {
		public class NavgateInfo {
			/// <summary>
			/// 登陆成功后方法【为null则无动作】
			/// </summary>
			public LoginSuccessFun Succfun;
			/// <summary>
			/// 登陆成功后跳转的页面【为null表示返回上一页】
			/// </summary>
			public Type SuccToPage;
		}

		public delegate void LoginSuccessFun(User.GetUserRes res);
		LoginSuccessFun _refun;

		public LoginPage() {
			this.InitializeComponent();
		}

		private Type _fromPageType;
		Storyboard msgshowStory = null;
		private void ShowMsg(string msg) {
			labMsg.Text = msg;
			if (msgshowStory == null) {
				msgshowStory = Resources["MsgKirakira"] as Storyboard;
			}
			msgshowStory.Begin();
		}


		protected override void OnNavigatedTo(NavigationEventArgs e) {
			base.OnNavigatedTo(e);
			cbxUser.ItemsSource = User.LocalUserList();
			txtPwd.Password = "";
			labMsg.Text = "";

			NavgateInfo info = e.Parameter as NavgateInfo;
			if (info != null) {
				_refun = info.Succfun;
				_fromPageType = info.SuccToPage;
			} else {
				_refun = null;
				_fromPageType = null;
			}

		}

		/// <summary>
		/// 返回特定页面
		/// </summary>
		private void BackPage() {
			Frame rootFrame = Window.Current.Content as Frame;
			if (rootFrame == null || !rootFrame.CanGoBack) return;
			rootFrame.GoBack();
		}
		private void Login_Click(object sender, RoutedEventArgs e) {
			string uname = cbxUser.SelectionBoxItem as string;
			string pwd = txtPwd.Password;
			User.GetUserRes res = User.GetUser(uname, pwd);
			if (res.state == User.GetUserEnum.ok) {
				_refun?.Invoke(res);
				StoreRoom.NowLoginedUser = res.user;
				BackPage();
			}
			else {
				ShowMsg(res.stateDesc);
			}
		}

		private void btnBack_Click(object sender, RoutedEventArgs e) {
			BackPage();
		}

		private void btnReg_Click(object sender, RoutedEventArgs e) {
			//RegisterPage rpage = new RegisterPage();
			//rpage.RegSuccess += rpage_RegSuccess;

			//rpage.Register(ParentWindow, this);
		}

		void rpage_RegSuccess(object sender, object[] args) {
			cbxUser.ItemsSource = User.LocalUserList();
		}
	}
}
