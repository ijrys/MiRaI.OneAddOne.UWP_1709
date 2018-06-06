using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MiRaI.OneAddOne {
	public class User {
		public delegate void ProChangeFun (User sender, object[] args);
		public event ProChangeFun LevelChanged;
		public event ProChangeFun HistoryChanged;

		private static string connstr = "Data Source=./Data/db.db;Version=3";

		public enum GetUserEnum {
			ok,
			error,
			pwderror,
		}
		public struct GetUserRes {
			public User user;
			public GetUserEnum state;
			public string stateDesc;
		}

		#region 属性和私有字段
		int _id = -1;
		int _level = -1;
		string _nickName;
		bool _isParents = false;
		User[] _children = null;
		List<QuestionPage.Resault> _history = null;

		/// <summary>
		/// 获取家长的孩子
		/// </summary>
		public User[] Children {
			get {
				if (!IsParents) return null;
				if (_children == null) {
					_children = LocalChildList ();
				}
				return _children;
			}
		}

		/// <summary>
		/// 获取用户历史
		/// </summary>
		public IEnumerable<QuestionPage.Resault> History {
			get {
				if (_history == null) {
					_history = new List<QuestionPage.Resault> ();
					_history.AddRange (GetHistorys (_id));
				}
				return _history.ToArray ();
			}
		}

		/// <summary>
		/// 昵称
		/// </summary>
		public string NickName { get { return _nickName; } }
		/// <summary>
		/// 指示是否是家长
		/// </summary>
		public bool IsParents { get { return _isParents; } }
		/// <summary>
		/// 用户id
		/// </summary>
		public int Id { get { return _id; } }
		/// <summary>
		/// 用户等级
		/// </summary>
		public int Level { get { return _level; } }
		#endregion

		/// <summary>
		/// 登记发生改变
		/// </summary>
		/// <param name="level"></param>
		public void ChangeLevel (int level) {
			_level = level;
			//using (SQLiteConnection conn = new SQLiteConnection (connstr)) {
			//	using (SQLiteCommand cmd = conn.CreateCommand ()) {
			//		cmd.CommandText = "update UserSet set level = @newlevel where id = @usid";
			//		cmd.Parameters.AddWithValue ("@usid", _id);
			//		cmd.Parameters.AddWithValue ("@newlevel", level);

			//		conn.Open ();

			//		cmd.ExecuteNonQuery ();
			//	}
			//}
			if (LevelChanged != null) LevelChanged (this, new object[] { level });
		}

		/// <summary>
		/// 添加一条历史记录
		/// </summary>
		/// <param name="res"></param>
		public void AddHistory (QuestionPage.Resault res) {
			_history = null;
			SaveHistory (_id, res);
			if (HistoryChanged != null) HistoryChanged (this, new object[] { res });
		}

		#region Static Funs
		/// <summary>
		/// 获取一个用户，用于用户登录
		/// </summary>
		/// <param name="account">账号</param>
		/// <param name="pwd">密码</param>
		/// <returns></returns>
		public static GetUserRes GetUser (string account, string pwd) {
			GetUserRes res = new GetUserRes ();

			//using (SQLiteConnection conn = new SQLiteConnection (connstr)) {
			//	using (SQLiteCommand cmd = conn.CreateCommand ()) {
			//		cmd.CommandText = "select * from UserSet where Account = @acc and Password = @pwd and delflag == 1";
			//		cmd.Parameters.AddWithValue ("@acc", account);
			//		cmd.Parameters.AddWithValue ("@pwd", pwd);

			//		conn.Open ();

			//		using (SQLiteDataReader reader = cmd.ExecuteReader ()) {
			//			if (reader.Read ()) {
			//				User user = new User ();
			//				user._id = int.Parse (reader["ID"].ToString ());
			//				user._nickName = reader["Nickname"].ToString ();
			//				user._isParents = bool.Parse (reader["IsParents"].ToString ());
			//				user._level = int.Parse (reader["level"].ToString ());

			//				if (reader.Read ()) { // error
			//					res.state = GetUserEnum.error;
			//					res.stateDesc = "未知错误";
			//					res.user = null;
			//				} else { // ok
			//					res.state = GetUserEnum.ok;
			//					res.stateDesc = "";
			//					res.user = user;
			//				}
			//			} else { // 用户名或密码错误
			//				res.state = GetUserEnum.pwderror;
			//				res.user = null;
			//				res.stateDesc = "用户名或密码错误";
			//			}
			//		}

			//	}
			//}
			return res;
		}

		/// <summary>
		/// 本地用户列表
		/// </summary>
		/// <returns></returns>
		public static string[] LocalUserList () {
			//return new string[] { "c1", "p1" };
			List<string> re = new List<string> ();
			//using (SQLiteConnection conn = new SQLiteConnection (connstr)) {
			//	using (SQLiteCommand cmd = conn.CreateCommand ()) {
			//		cmd.CommandText = "select Account from UserSet where delflag == 1";

			//		conn.Open ();
			//		using (SQLiteDataReader reader = cmd.ExecuteReader ()) {
			//			while (reader.Read ()) {
			//				string nm = reader["Account"].ToString ();
			//				re.Add (nm);
			//			}
			//		}
			//	}
			//}
			return re.ToArray ();
		}

		/// <summary>
		/// 获取本地所有child
		/// </summary>
		/// <returns></returns>
		public static User[] LocalChildList () {
			//return new string[] { "c1", "p1" };
			List<User> re = new List<User> ();
			//using (SQLiteConnection conn = new SQLiteConnection (connstr)) {
			//	using (SQLiteCommand cmd = conn.CreateCommand ()) {
			//		cmd.CommandText = "select * from [UserSet] where IsParents = 0 and delflag = 1";

			//		conn.Open ();
			//		using (SQLiteDataReader reader = cmd.ExecuteReader ()) {
			//			while (reader.Read ()) {
			//				User user = new User ();
			//				user._id = int.Parse (reader["ID"].ToString ());
			//				user._nickName = reader["Nickname"].ToString ();
			//				user._isParents = bool.Parse (reader["IsParents"].ToString ());
			//				user._level = int.Parse (reader["level"].ToString ());

			//				re.Add (user);
			//			}
			//		}
			//	}
			//}
			return re.ToArray ();
		}
		/// <summary>
		/// 获取一个用户的历史记录
		/// </summary>
		/// <param name="id">用户的id</param>
		/// <returns></returns>
		private static QuestionPage.Resault[] GetHistorys (int id) {
			List<QuestionPage.Resault> res = new List<QuestionPage.Resault> ();
			//using (SQLiteConnection conn = new SQLiteConnection (connstr)) {
			//	using (SQLiteCommand cmd = conn.CreateCommand ()) {
			//		cmd.CommandText = "select * from HistorySet where uid=@usid";
			//		cmd.Parameters.AddWithValue ("@usid", id);

			//		conn.Open ();
			//		using (SQLiteDataReader reader = cmd.ExecuteReader ()) {
			//			while (reader.Read ()) {
			//				var nowhis = new QuestionPage.Resault ();
			//				nowhis.testDate = DateTime.Parse (reader["Datetime"].ToString ());
			//				nowhis.testName = reader["Testname"].ToString ();
			//				nowhis.acNum = int.Parse (reader["Acnum"].ToString ());
			//				nowhis.waNum = int.Parse (reader["Wanum"].ToString ());
			//				nowhis.maxStreaks = int.Parse (reader["MaxStreaks"].ToString ());
			//				nowhis.useTime = int.Parse (reader["Usetime"].ToString ());

			//				res.Add (nowhis);
			//			}
			//		}
			//	}
			//}

			return res.ToArray ();
		}

		/// <summary>
		/// 向用户的历史记录列表中追加一条记录
		/// </summary>
		/// <param name="id">用户id</param>
		/// <param name="res">记录</param>
		private static void SaveHistory (int id, QuestionPage.Resault res) {
			//using (SQLiteConnection conn = new SQLiteConnection (connstr)) {
			//	using (SQLiteCommand cmd = conn.CreateCommand ()) {
			//		cmd.CommandText = "insert into HistorySet(UID, Datetime, Testname, Acnum, Wanum, MaxStreaks, Usetime) values" +
			//			"(@usid,@datetime,@textname,@acnum,@wanum,@maxsnum,@utime)";
			//		cmd.Parameters.AddWithValue ("@usid", id);
			//		cmd.Parameters.AddWithValue ("@datetime", res.testDate);
			//		cmd.Parameters.AddWithValue ("@textname", res.testName);
			//		cmd.Parameters.AddWithValue ("@acnum", res.acNum);
			//		cmd.Parameters.AddWithValue ("@wanum", res.waNum);
			//		cmd.Parameters.AddWithValue ("@maxsnum", res.maxStreaks);
			//		cmd.Parameters.AddWithValue ("@utime", res.useTime);

			//		conn.Open ();

			//		cmd.ExecuteNonQuery ();

			//	}
			//}
		}


		#endregion


		#region 用户注册相关

		/// <summary>
		/// 检查是否可以注册这个账号
		/// </summary>
		/// <param name="account">检查的账号</param>
		/// <returns></returns>
		public static bool CanNewAccount (string account) {
			bool re = false;
			//using (SQLiteConnection conn = new SQLiteConnection (connstr)) {
			//	using (SQLiteCommand cmd = conn.CreateCommand ()) {
			//		cmd.CommandText = "select count(*) from UserSet where Account = @account";
			//		cmd.Parameters.AddWithValue ("@account", account);

			//		conn.Open ();

			//		int num = int.Parse (cmd.ExecuteScalar ().ToString ());

			//		re = (num == 0);
			//	}
			//}
			return re;
		}

		/// <summary>
		/// 创建一个新用户
		/// </summary>
		/// <param name="account"></param>
		/// <param name="pwd"></param>
		/// <param name="nickname"></param>
		/// <param name="isp"></param>
		/// <returns></returns>
		public static bool CreateUser (string account, string pwd, string nickname, bool isp) {
			//using (SQLiteConnection conn = new SQLiteConnection (connstr)) {
			//	using (SQLiteCommand cmd = conn.CreateCommand ()) {
			//		cmd.CommandText = "insert into UserSet (Account, Password, Nickname, IsParents, level, delflag) values (@account, @pwd, @nickname, @ispar, @level, 1)";

			//		cmd.Parameters.AddWithValue ("@account", account);
			//		cmd.Parameters.AddWithValue ("@pwd", pwd);
			//		cmd.Parameters.AddWithValue ("@nickname", nickname);
			//		cmd.Parameters.AddWithValue ("@ispar", isp);
			//		if (isp) {
			//			cmd.Parameters.AddWithValue ("@level", -1);
			//		} else {
			//			cmd.Parameters.AddWithValue ("@level", 1);
			//		}

			//		conn.Open ();

			//		int inr = cmd.ExecuteNonQuery ();
			//		return (inr == 1);

			//	}
			//}
			return false;
		} 
		#endregion
	}
}
