using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRaI.OoeAddOne.BasicType {
	public sealed class SimpleNumberQuestionType: IQuestionAble {
		#region 属性和私有字段
		private string _description;
		private string _answer;
		private long _hashcode;

		public string Description { get { return _description; } }
		public string Answer { get { return _answer; } }
		public long HashCodeLong { get { return _hashcode; } }
        public int HashCode { get { return (int)((_hashcode >> 32) ^ (_hashcode)); } }
		#endregion

		public CheckResaults Check(string answer) {
			if (answer == Answer) {
				return new CheckResaults() { resault = CheckResaultEnum.Accept, desc = "正确" };
			} else {
				return new CheckResaults() { resault = CheckResaultEnum.WrongAnswer, desc = "错误" };
			}
		}

		public sealed override int GetHashCode() {
			return HashCode;
		}

		public sealed override bool Equals(object obj) {
			SimpleNumberQuestionType t = null;
			try {
				t = (SimpleNumberQuestionType) obj;
			} catch (Exception ex) {
				return false;
			}
			return this.HashCode == t.HashCode;
		}

		#region 构造方法
		public SimpleNumberQuestionType(SimpleNumberQuestionType que) {
			this._description = que._description;
			this._answer = que._answer;
			this._hashcode = que._hashcode;
		}

		public SimpleNumberQuestionType(string description, string answer, long hashcode) {
			this._description = description;
			this._answer = answer;
			this._hashcode = hashcode;
		} 
		#endregion
	}
}
