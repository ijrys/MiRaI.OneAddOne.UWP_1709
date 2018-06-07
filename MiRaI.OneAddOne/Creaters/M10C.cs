using MiRaI.OoeAddOne.BasicType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace MiRaI.OneAddOne.Creaters {
	class M10C : ICreaterUi {
		private ImageSource _background;
		private string _name;

		public ImageSource Background {
			get {
				if (_background == null) {
					_background = new BitmapImage(new Uri("ms-appx:///imgs/c2.png"));
				}
				return _background;
			}
		}
		public string Name { get { return _name; } }
		public ICreateQuestionAble Creater {
			get {
				return new Mul10Creater();
			}
		}

		public string ShowMwssage { get { return null; } }

		public bool CanSelNum { get { return true; } }

		public QuestionPage.Info DefaultStartInfo { get { return null; } }
		//public SelecterPage.EndTestFun EndTestFun { get { return null; } }


		public M10C() {
			//_background = Application.Current.Resources["CloudSmall_1"] as Geometry;
			_name = "九九乘法表";
		}
	}
}