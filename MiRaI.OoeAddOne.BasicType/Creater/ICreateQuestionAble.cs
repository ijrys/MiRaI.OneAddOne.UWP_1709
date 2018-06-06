using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRaI.OoeAddOne.BasicType {
	public interface ICreateQuestionAble {
		int Seed{ get; }
		string Type { get; }
		IQuestionAble NextQuestion ();

	}
}
