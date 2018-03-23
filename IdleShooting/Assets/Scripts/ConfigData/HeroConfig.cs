using System.Collections.Generic;

public class HeroConfig {
	public readonly uint m_ID;
	public readonly uint m_Level;
	public readonly BigNumber m_HP;
	public readonly BigNumber m_Attack_Damage;
	public readonly float m_Attack_Speed;
	public readonly BigNumber m_Upgrade_Gold;

	public HeroConfig(string[] _str)
	{
		uint i = 0;
		m_ID = GlobalFunction.Instance.ChangeStringToUint(_str[i++]);
		m_Level = GlobalFunction.Instance.ChangeStringToUint(_str[i++]);
		m_HP.Init(_str[i++]);
		m_Attack_Damage.Init(_str[i++]);
		m_Attack_Speed = GlobalFunction.Instance.ChangeStringToFloat(_str[i++]);
		m_Upgrade_Gold.Init(_str[i++]);
	}
}

public class HeroConfigManager : SingleInstance<HeroConfigManager>{

	public readonly Dictionary<uint, HeroConfig> data;

	public HeroConfigManager () {
		data = new Dictionary<uint, HeroConfig>();
	}

	public void Init(string _name) {
		CsvReader.Instance.LoadCsv(_name);
		for (int i = 3; i < CsvReader.Instance.ArrrayData.Count; ++i) {
			if (CsvReader.Instance.ArrrayData[i][0] == "")	return;
			HeroConfig _config = new HeroConfig(CsvReader.Instance.ArrrayData[i]);
			data.Add(uint.Parse(CsvReader.Instance.ArrrayData[i][0]), _config);
		}
	}
}