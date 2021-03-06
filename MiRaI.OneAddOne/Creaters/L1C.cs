﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MiRaI.OoeAddOne.BasicType;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace MiRaI.OneAddOne.Creaters {
	class L1C : ICreaterUi {
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
				return new Add100Creater();
			}
		}

		public string ShowMwssage { get { return "过关条件：1分组内答对10道即可解锁新的关卡"; } }

		public bool CanSelNum { get { return false; } }
		public QuestionPage.EndTestAction EndTestFun {
			get {
				return (History res, QuestionPage cont) => {
					if (res.AcNum >= 1) {
						cont.ShowMessage("恭喜过关");
						StoreRoom.NowLoginedUser.ChangeLevel(StoreRoom.NowLoginedUser.Level + 1);
						//cont._user.ChangeLevel(cont._user.Level + 1);
					}
					else {
						cont.ShowMessage("真遗憾，下次再战吧");
					}
				};
			}
		}

		public QuestionPage.Info DefaultStartInfo {
			get {
				QuestionPage.Info re = new QuestionPage.Info(this);
				re.maxTime = 20;
				re.maxAcNum = 1;
				return re;
			}
		}

		public L1C() {
			//_background = Application.Current.Resources["CloudSmall_1"] as Geometry;
			_name = "100以内的加法进阶关";
		}
	}
}
