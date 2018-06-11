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
	public sealed partial class RegisterPage : Page {
		public RegisterPage() {
			this.InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e) {
			MsgBorder.Visibility = Visibility.Collapsed;
			labMsg.Text = "";
		}

		Storyboard msgshowStory = null;
		private void ShowMsg(string msg) {
			labMsg.Text = msg;
			if (msgshowStory == null) {
				msgshowStory = Resources["MsgKirakira"] as Storyboard;
			}
			msgshowStory.Begin();
		}

		private void btnBack_Click(object sender, RoutedEventArgs e) {
			Frame rootFrame = Window.Current.Content as Frame;
			if (rootFrame == null || !rootFrame.CanGoBack) return;
			rootFrame.GoBack();
		}

		private void btnReg_Click(object sender, RoutedEventArgs e) {
			string acc = txtAccount.Text;
			string nn = txtNickname.Text;
			string pwd = txtPWD.Text;

			if (string.IsNullOrWhiteSpace(acc)) {
				ShowMsg("用户名不能为空");
				txtAccount.Focus(FocusState.Pointer);
				return;
			}
			if (string.IsNullOrWhiteSpace(nn)) {
				ShowMsg("昵称不能为空");
				txtNickname.Focus(FocusState.Pointer);
				return;
			}
			if (string.IsNullOrWhiteSpace(pwd)) {
				ShowMsg("密码不能为空");
				txtPWD.Focus(FocusState.Pointer);
				return;
			}

			if ((!rIsP.IsChecked.HasValue && !rIsC.IsChecked.HasValue) ||
				(rIsP.IsChecked.Value == false && rIsC.IsChecked.Value == false)) {
				ShowMsg("请选择用户类型");
				return;
			}

			if (!User.CanNewAccount(acc)) {
				ShowMsg("用户名已占用");
				txtAccount.Focus(FocusState.Pointer);
				return;
			}

			if (User.CreateUser(acc, pwd, nn, rIsP.IsChecked.Value)) {
				MsgBorder.Visibility = Visibility.Visible;

				//if (RegSuccess != null) RegSuccess.Invoke(this, null);
				
			}
			else {
				ShowMsg("发生未知错误");
			}
		}

		private void btnMsgOK(object sender, RoutedEventArgs e) {
			btnBack_Click(this, null);
		}
	}
}
