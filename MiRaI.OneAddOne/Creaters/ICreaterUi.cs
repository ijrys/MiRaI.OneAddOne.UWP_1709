using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiRaI.OoeAddOne.BasicType;
using Windows.UI.Xaml.Media;

namespace MiRaI.OneAddOne.Creaters {
	interface ICreaterUi {
		ImageSource Background { get; }
		string Name { get; }
		ICreateQuestionAble Creater { get; }
		string ShowMwssage { get; }
		bool CanSelNum { get; }
		//QuestionPage.Info DefaultStartInfo { get; }

		//SelecterPage.EndTestFun EndTestFun { get; }
	}
}
