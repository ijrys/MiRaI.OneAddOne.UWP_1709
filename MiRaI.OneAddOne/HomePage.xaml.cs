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
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace MiRaI.OneAddOne {
	/// <summary>
	/// 可用于自身或导航至 Frame 内部的空白页。
	/// </summary>
	public sealed partial class HomePage : Page {

		public HomePage() {
			this.InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e) {
			base.OnNavigatedTo(e);
			if (StoreRoom.NowLoginedUser != null) {
				User user = StoreRoom.NowLoginedUser;
				labUserName.Text = user.NickName;
				btnStart.Visibility = Visibility.Visible;
				btnHistory.Visibility = Visibility.Visible;
			}

		}

		private class CloudInfo {
			public Image image;
			public int keyf;
		}

		private void btnHelp_Click(object sender, RoutedEventArgs e) {

		}


		private void btnLogin_Click(object sender, RoutedEventArgs e) {
			Frame rootFrame = Window.Current.Content as Frame;
			if (rootFrame == null) return;

			rootFrame.Navigate(typeof(LoginPage), new LoginPage.NavgateInfo());
		}

		private void btnHistory_Click(object sender, RoutedEventArgs e) {
			User us = StoreRoom.NowLoginedUser;
			Frame rootFrame = Window.Current.Content as Frame;
			if (rootFrame == null) return;

			rootFrame.Navigate(typeof(HistoryPage), us);
			
		}

		private void btnStart_Click(object sender, RoutedEventArgs e) {
			Frame rootFrame = Window.Current.Content as Frame;
			if (rootFrame == null) return;
			rootFrame.Navigate(typeof(SelecterPage), new SelecterPage.Info() { User = StoreRoom.NowLoginedUser });
		}


	}
}
