using System;
using System.Collections.Generic;
using System.Text;

namespace MiRaI.OoeAddOne.BasicType {
	public struct CheckResaults {
		public CheckResaultEnum resault;
		public string desc;
	}

	public enum CheckResaultEnum {
		WrongAnswer,
		Accept,
	}
}
