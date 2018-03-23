using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFunction : SingleInstance<GlobalFunction> {

    public int[] ChangeStringToIntArray(string _str, char _tag = ';')
    {
        string[] _strArray = _str.Split(_tag);
        int[] _array = new int[_str.Length];

        for (uint i = 0; i < _strArray.Length; ++i)
        {
            _array[i] = int.Parse(_strArray[i]);
        }

        return _array;
    }

    public int ChangeStringToInt(string _str)
    {
        if (_str == "")
        {
            return 0;
        }
        else
        {
            return int.Parse(_str);
        }
    }

    public float ChangeStringToFloat(string _str)
    {
        if (_str == "")
        {
            return 0.0f;
        }
        else
        {
            return float.Parse(_str);
        }
    }

    public uint ChangeStringToUint(string _str)
    {
        if (_str == "")
        {
            return 0;
        }
        else
        {
            return uint.Parse(_str);
        }
    }

    public float[] ChangeStringToFloatArray(string _str, char _tag = ';')
    {
        string[] _strArray = _str.Split(_tag);
        float[] _array = new float[_str.Length];

        for (uint i = 0; i < _strArray.Length; ++i)
        {
            _array[i] = float.Parse(_strArray[i]);
        }

        return _array;
    }

    public uint[] ChangeStringToUintArray(string _str, char _tag = ';')
    {
        string[] _strArray = _str.Split(_tag);
        uint[] _array = new uint[_str.Length];

        for (uint i = 0; i < _strArray.Length; ++i)
        {
            _array[i] = uint.Parse(_strArray[i]);
        }

        return _array;
    }

    public T[] ChangeStringToStructArray<T>(string _str, char _tag = ';') where T : IBaseStruct
    {
        string[] _strArray = _str.Split(_tag);
        T[] _array = new T[_str.Length];

        for (uint i = 0; i < _strArray.Length; ++i)
        {
            _array[i].Init(_strArray[i], '-');
        }

        return _array;
    }
}