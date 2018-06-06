using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiRaI.OoeAddOne.BasicType {
	public sealed class Mul10Creater: ICreateQuestionAble {
		int _seed;
		Random _r;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id">start with 0</param>
		/// <returns></returns>
		public static IQuestionAble GetQuestionByID (int id) {
			id = id % 45;
			int a = id + 1, b = 1, re;
			for (; a > b; b ++) a -= b;
			re = a * b;

			return new SimpleNumberQuestionType (string.Format ("{0} * {1} = ?", a, b), re.ToString (), id);
		}

		public int Seed { get { return _seed; } }

		public string Type { get { return "Mul10"; } }

		public IQuestionAble NextQuestion() {
			int id = _r.Next(0, 45);
			return GetQuestionByID (id);
		}

		public Mul10Creater() {
			int seed = (int)DateTime.Now.Ticks;
			_seed = seed;
			_r = new Random(seed);
		}
		public Mul10Creater(int seed) {
			_seed = seed;
			_r = new Random(seed);
		}
	}
}
