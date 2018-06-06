﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MiRaI.OoeAddOne.BasicType;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace MiRaI.OneAddOne.Creaters {
	class L1C : ICreaterUi {
		private Geometry _background;
		private string _name;

		public Geometry Background {
			get {
				if (_background == null) {
					object a = Application.Current.Resources["CloudSmall_1"];
					_background = a as Geometry;
				}
				return _background;
			}
		}
		public string Name { get { return _name; } }
		public ICreateQuestionAble Creater {
			get {
				return new Add100Creater ();
			}
		}

		public string ShowMwssage { get { return "过关条件：1分组内答对10道即可解锁新的关卡"; } }

		public bool CanSelNum { get { return false; } }
		//public SelecterPage.EndTestFun EndTestFun {
		//	get {
		//		return (QuestionPage.Resault res, SelecterPage cont) => {

		//			if (res.acNum >= 1) {
		//				cont.ShowMsg ("恭喜过关");
		//				cont._user.ChangeLevel (cont._user.Level + 1);
		//			} else {
		//				cont.ShowMsg ("真遗憾，下次再战吧");
		//			}
		//		};
		//	}
		//}

		public QuestionPage.Info DefaultStartInfo {
			get {
				QuestionPage.Info re = new QuestionPage.Info (Creater);
				re.maxTime = 20;
				re.maxAcNum = 1;
				return re;
			}
		}

		public L1C () {
			_background = Application.Current.Resources["CloudSmall_1"] as Geometry;
			_name = "100以内的加法进阶关";
		}
	}
}