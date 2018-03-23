public interface IBaseStruct {
	void Init(string _str, char _tag = ';');
}

#pragma warning disable 0414
public struct ItemInfoStruct : IBaseStruct {
	uint type;
	uint ID;
	uint count;

	public void Init(string _str, char _tag = ';') {
		string[] _strArray = _str.Split(_tag);
		type = uint.Parse(_strArray[0]);
		ID = uint.Parse(_strArray[1]);
		count = uint.Parse(_strArray[2]);
	}
}

#pragma warning disable 0414
public struct HeroInfoStruct : IBaseStruct {
	uint UID;
	uint ID;
	uint attack;
	uint def;

	public void Init(string _str, char _tag = ';') {
		string[] _strArray = _str.Split(_tag);
		UID = uint.Parse(_strArray[0]);
		ID = uint.Parse(_strArray[1]);
		attack = uint.Parse(_strArray[2]);
		def = uint.Parse(_strArray[3]);
	}
}

