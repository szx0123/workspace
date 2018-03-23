using System.Collections.Generic;

public class AcitveCopyMapConfig {
	public readonly int m_Id;
	public readonly int m_activeIndex;
	public readonly int m_triggerType;
	public readonly int m_activeType;
	public readonly int m_TimeLimit;
	public readonly int m_JoinNumber;
	public readonly int[] m_OpenTime;
	public readonly int[] m_CloseTime;
	public readonly int[] m_RewardOpenTime;
	public readonly int[] m_RewardCloseTime;
	public readonly string m_activename;
	public readonly string m_timetips;
	public readonly int m_joinlevel;
	public readonly string m_activetips;
	public readonly string[] m_showreward;
	public readonly string m_icon;
	public readonly string m_remindtext;
	public readonly int m_MinNum;
	public readonly int m_MaxNum;
	public readonly int m_CountDown;
	public readonly int m_MapId;
	public readonly int m_ActiveMeleeId;

	public AcitveCopyMapConfig(string[] _str)
	{
		uint i = 0;
		m_Id = GlobalFunction.Instance.ChangeStringToInt(_str[i++]);
		m_activeIndex = GlobalFunction.Instance.ChangeStringToInt(_str[i++]);
		m_triggerType = GlobalFunction.Instance.ChangeStringToInt(_str[i++]);
		m_activeType = GlobalFunction.Instance.ChangeStringToInt(_str[i++]);
		m_TimeLimit = GlobalFunction.Instance.ChangeStringToInt(_str[i++]);
		m_JoinNumber = GlobalFunction.Instance.ChangeStringToInt(_str[i++]);
		m_OpenTime = GlobalFunction.Instance.ChangeStringToIntArray(_str[i++]);
		m_CloseTime = GlobalFunction.Instance.ChangeStringToIntArray(_str[i++]);
		m_RewardOpenTime = GlobalFunction.Instance.ChangeStringToIntArray(_str[i++]);
		m_RewardCloseTime = GlobalFunction.Instance.ChangeStringToIntArray(_str[i++]);
		m_activename = _str[i++];
		m_timetips = _str[i++];
		m_joinlevel = GlobalFunction.Instance.ChangeStringToInt(_str[i++]);
		m_activetips = _str[i++];
		m_showreward = _str[i++].Split(';');
		m_icon = _str[i++];
		m_remindtext = _str[i++];
		m_MinNum = GlobalFunction.Instance.ChangeStringToInt(_str[i++]);
		m_MaxNum = GlobalFunction.Instance.ChangeStringToInt(_str[i++]);
		m_CountDown = GlobalFunction.Instance.ChangeStringToInt(_str[i++]);
		m_MapId = GlobalFunction.Instance.ChangeStringToInt(_str[i++]);
		m_ActiveMeleeId = GlobalFunction.Instance.ChangeStringToInt(_str[i++]);
	}
}

public class AcitveCopyMapConfigManager : SingleInstance<AcitveCopyMapConfigManager>{

	public readonly Dictionary<uint, AcitveCopyMapConfig> data;

	public AcitveCopyMapConfigManager () {
		data = new Dictionary<uint, AcitveCopyMapConfig>();
	}

	public void Init(string _name) {
		CsvReader.Instance.LoadCsv(_name);
		for (int i = 3; i < CsvReader.Instance.ArrrayData.Count; ++i) {
			if (CsvReader.Instance.ArrrayData[i][0] == "")	return;
			AcitveCopyMapConfig _config = new AcitveCopyMapConfig(CsvReader.Instance.ArrrayData[i]);
			data.Add(uint.Parse(CsvReader.Instance.ArrrayData[i][0]), _config);
		}
	}
}