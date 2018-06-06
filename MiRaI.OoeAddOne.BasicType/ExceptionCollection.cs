using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MiRaI.OoeAddOne.BasicType {
	class ExceptionCollection {
	}
	class ScopeException: Exception {
		public ScopeException():this("设定范围不正确或超出限制") {
		}

		public ScopeException(string message) : base(message) {
		}
	}
	class NoMoreException : Exception {
		public NoMoreException() : this("没有更多符合要求的题目") {
		}

		public NoMoreException(string message) : base(message) {
		}
	}
}
