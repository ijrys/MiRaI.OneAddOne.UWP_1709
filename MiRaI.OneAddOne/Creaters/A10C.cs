﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using MiRaI.OoeAddOne.BasicType;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace MiRaI.OneAddOne.Creaters {
	class A10C : ICreaterUi {
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
				return new Add10Creater();
			}
		}

		public string ShowMwssage { get { return null; } }

		public bool CanSelNum { get { return true; } }

		public QuestionPage.Info DefaultStartInfo { get { return null; } }
		public QuestionPage.EndTestAction EndTestFun { get { return null; } }


		public A10C() {
			//_background = Application.Current.Resources["CloudSmall_1"] as Geometry;
			_name = "10以内的加法";
		}
	}
}
