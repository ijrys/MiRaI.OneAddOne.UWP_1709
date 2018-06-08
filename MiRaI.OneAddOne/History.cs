using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRaI.OneAddOne {
	public class History {
		/// <summary>
		/// 正确数
		/// </summary>
		private int acNum = 0;
		/// <summary>
		/// 错误数
		/// </summary>
		private int waNum = 0;
		/// <summary>
		/// 用时
		/// </summary>
		private int useTime = 0;
		/// <summary>
		/// 最大连续正确数
		/// </summary>
		private int maxStreaks = 0;
		/// <summary>
		/// 测试名
		/// </summary>
		private string testName;
		/// <summary>
		/// 测试日期
		/// </summary>
		private DateTime testDate;

		Guid _creatMachineId;
		Guid _historyID;

		/// <summary>
		/// 正确数
		/// </summary>
		public int AcNum { get => acNum; set => acNum = value; }
		/// <summary>
		/// 正确数的字符串
		/// </summary>
		public string AcNumStr { get { return AcNum.ToString(); } }
		/// <summary>
		/// 错误数
		/// </summary>
		public int WaNum { get => waNum; set => waNum = value; }
		/// <summary>
		/// 错误数的字符串
		/// </summary>
		public string WaNumStr { get { return WaNum.ToString(); } }
		/// <summary>
		/// 总题目数的字符串
		/// </summary>
		public string SumStr {
			get { return (AcNum + WaNum).ToString(); }
		}
		/// <summary>
		/// 使用时间
		/// </summary>
		public int UseTime { get { return useTime; } set { useTime = value; } }
		/// <summary>
		/// 最大连续正确数
		/// </summary>
		public int MaxStreaks { get => maxStreaks; set => maxStreaks = value; }
		/// <summary>
		/// 最大正确数的字符串
		/// </summary>
		public string MaxStreaksStr { get { return MaxStreaks.ToString(); } }
		/// <summary>
		/// 测试名
		/// </summary>
		public string TestName { get { return testName; } set { testName = value; } }
		/// <summary>
		/// 测试日期
		/// </summary>
		public DateTime TestDate { get { return testDate; } set { testDate = value; } }
		/// <summary>
		/// 正确率的字符串
		/// </summary>
		public string AcProportionsStr {
			get {
				int sum = AcNum + WaNum;
				if (sum == 0) return "N/A";
				Debug.WriteLine(sum);
				Debug.WriteLine(AcNum);
				Debug.WriteLine(AcNum * 100.0 / sum);
				return (AcNum * 100.0 / sum).ToString("00.00") + "%";
			}
		}
		/// <summary>
		/// 使用时间的字符串
		/// </summary>
		public string UseTimeStr {
			get {
				return string.Format("{0} : {1:00}", UseTime / 60, UseTime % 60);
			}
		}
		/// <summary>
		/// 历史记录创建设备ID
		/// </summary>
		public Guid CreatMachineId { get => _creatMachineId; }
		/// <summary>
		/// 历史记录ID
		/// </summary>
		public Guid HistoryID { get => _historyID; }

		#region 构造函数
		public History() : this(StoreRoom.MachineGuid(), new Guid()) {

		}
		public History(Guid historyID) : this(StoreRoom.MachineGuid(), historyID) {

		}
		public History(Guid machineId, Guid historyId) {
			_creatMachineId = machineId;
			_historyID = historyId;
		} 
		#endregion
	}
}
