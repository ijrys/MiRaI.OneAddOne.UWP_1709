using System;
using System.Collections.Generic;
using System.Text;

namespace MiRaI.OoeAddOne.BasicType {
	public sealed class Sub10Creater : ICreateQuestionAble {
		int _seed;
		Random _r;

		public int Seed { get { return _seed; } }

        public string Type { get { return "Sub10"; } }

		public IQuestionAble NextQuestion() {
			int id = _r.Next(0, 36) + 1;
			int a = id;
			int b = 1;
			for (; a > b; b++) a -= b;
			b = 9 - b;

			int re = b + a;

			return new SimpleNumberQuestionType(string.Format("{0} - {1} = ?", re, a), b.ToString(), id);
		}

		public Sub10Creater() {
			int seed = (int)DateTime.Now.Ticks;
			_seed = seed;
			_r = new Random(seed);
		}
		public Sub10Creater(int seed) {
			_seed = seed;
			_r = new Random(seed);
		}
	}
}
