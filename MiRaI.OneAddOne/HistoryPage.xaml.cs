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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace MiRaI.OneAddOne {
	/// <summary>
	/// 可用于自身或导航至 Frame 内部的空白页。
	/// </summary>
	public sealed partial class HistoryPage : Page {
		public HistoryPage() {
			this.InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e) {
			base.OnNavigatedTo(e);
			User user = e.Parameter as User;
			if (user == null) return;
			if (user.IsParents) {
				//界面调整
				listChild.Visibility = Visibility.Visible;
				gridShowPanel.SetValue(Grid.ColumnProperty, 2);

				//显示的用户列表
				User[] chs = user.Children;
				List<User> childs = new List<User>();
				childs.Add(user);
				if (chs != null) childs.AddRange(chs);

				listChild.SelectionChanged += listChild_SelectionChanged;
				listChild.ItemsSource = childs;
				listChild.SelectedItem = null;
				listShow.ItemsSource = null;

			}
			else {
				//界面调整
				listChild.Visibility = Visibility.Collapsed;
				gridShowPanel.SetValue(Grid.ColumnProperty, 1);

				listShow.ItemsSource = user.History;
			}
		}

		private void listChild_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			ListBox lbx = sender as ListBox;
			if (lbx == null) return;

			User nuser = lbx.SelectedItem as User;
			if (nuser == null) return;
			listShow.ItemsSource = nuser.History;
		}
		private void Back_Click(object sender, RoutedEventArgs e) {
			Frame rootFrame = Window.Current.Content as Frame;
			if (rootFrame != null && rootFrame.CanGoBack) rootFrame.GoBack();
		}
	}
}
