using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiRaI.OneAddOne.Creaters;

namespace MiRaI.OneAddOne {
	static class StoreRoom {
		private static List<ICreaterUi> AllCreaters = new List<ICreaterUi> () {
			new A10C (),
			new A100C(),
			new M10C(),
		};
		private static List<ICreaterUi> CL1 = new List<ICreaterUi> () {
			new A10C (),
			new A100C(),
		};
		private static List<ICreaterUi> CL2 = new List<ICreaterUi> () {
			new M10C (),
		};
		public static List<ICreaterUi> GetCreaters (User user) {
			if (user == null) return null;
			if (user.IsParents) return AllCreaters;
			List<ICreaterUi> res = new List<ICreaterUi> ();
			if (user.Level > 0) {
				res.AddRange (CL1);
			} else {
				return res;
			}
			if (user.Level > 1) {
				res.AddRange (CL2);
			} else {
				res.Add (new L1C ());
				return res;
			}
			if (user.Level > 2) {
				
			} else {
				res.Add (new L2C ());
				return res;
			}
			return res;
		}


		private static HomePage _homePage = null;
		private static QuestionPage _qPage = null;
		//public static HomePage GetHomePage (MainWindow parentWindow) {
		//	if (_homePage == null) {
		//		_homePage = new HomePage (parentWindow);
		//	}
		//	return _homePage;
		//}
		//public static QuestionPage GetQuestionPage (MainWindow parentWindow) {
		//	if (_qPage == null) {
		//		_qPage = new QuestionPage (parentWindow);
		//	}
		//	return _qPage;
		//}

		public static User NowLoginedUser;


		public static Guid MachineGuid () {
			return new Guid();
		}
	}
}
