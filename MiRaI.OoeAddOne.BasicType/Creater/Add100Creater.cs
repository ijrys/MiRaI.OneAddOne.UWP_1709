using System;
using System.Collections.Generic;
using System.Text;

namespace MiRaI.OoeAddOne.BasicType {
	public sealed class Add100Creater : ICreateQuestionAble {
		int _seed;
		Random _r;

		public int Seed { get { return _seed; } }

		public string Type { get { return  "Add100"; } }

		public IQuestionAble NextQuestion() {
			int id = _r.Next(0, 5050) + 1;
			int a = id;
			int b = 1;
			for (; a > b; b++) a -= b;
			b = 99 - b;

			int re = b + a;

			return new SimpleNumberQuestionType(string.Format("{0} + {1} = ?", a, b), re.ToString(), id);
		}

		public Add100Creater() {
			int seed = (int)DateTime.Now.Ticks;
			_seed = seed;
			_r = new Random(seed);
		}
		public Add100Creater(int seed) {
			_seed = seed;
			_r = new Random(seed);
		}
	}
}
