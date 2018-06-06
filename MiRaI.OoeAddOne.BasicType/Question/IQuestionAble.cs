using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRaI.OoeAddOne.BasicType {
	public interface IQuestionAble {
		string Description { get; }
		string Answer { get; }

		CheckResaults Check(string answer);

		long HashCodeLong { get; }

		int HashCode { get; }
	}
}
