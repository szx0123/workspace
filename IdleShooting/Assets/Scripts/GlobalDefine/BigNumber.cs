using System.Collections.Generic;
using UnityEngine;
using System;

public struct BigNumberType
{
    public uint min;
    public uint max;
    public string suffix;

    public BigNumberType(uint _min, uint _max, string _suffix)
    {
        min = _min;
        max = _max;
        suffix = _suffix;
    }
}

public struct BigNumber : IBaseStruct
{
    float number;
    uint power;
    string type;
    uint typeStage;

    public float Number
    {
        get { return number; }
    }

    public BigNumber(float _number = 0.0f, uint _power = 0)
    {
        number = _number;
        power = _power;
        type = "";
        typeStage = 0;
    }

    public void Init(string _str, char _tag = ';')
    {
        string[] _strArray = _str.Split(_tag);
        number = float.Parse(_strArray[0]);
        power = uint.Parse(_strArray[1]);
    }

    public void Set(float _number, uint _power)
    {
        number = _number;
        power = _power;

        for (uint i = 0; i < GlobalDefine.Instance.bigNumberDic.Count; ++i)
        {
            if (power < GlobalDefine.Instance.bigNumberDic[i].max)
            {
                number = number * (float)Math.Pow(10, power - GlobalDefine.Instance.bigNumberDic[i].min);
                power = GlobalDefine.Instance.bigNumberDic[i].min;
                typeStage = i;
                type = GlobalDefine.Instance.bigNumberDic[i].suffix;
                return;
            }
        }
    }

    public void AssignParam(BigNumber _data)
    {
        number = _data.number;
        power = _data.power;
        typeStage = _data.typeStage;
        type = _data.type;
    }

    public void Print(string _begin = "", string _end = "")
    {
        Debug.Log(_begin + number + " * 10 ^ " + power + _end);
    }

    public bool IsZero()
    {
        return number == 0f && power == 0;
    }

    public void Reset()
    {
        number = 0;
        power = 0;
        typeStage = 0;
        type = "";
    }

    public void KeepNumberInStage()
    {
        if (IsZero())
        {
            return;
        }

        if (number < 0f)
        {
            Reset();
            return;
        }
        
        if (number >= GlobalDefine.Instance.bigNumberIntervel)
        {
            number /= GlobalDefine.Instance.bigNumberIntervel;
            ++typeStage;
            power = GlobalDefine.Instance.bigNumberDic[typeStage].min;
            type = GlobalDefine.Instance.bigNumberDic[typeStage].suffix;
        }
        else if (number < 1f)
        {
            if (power == 0)
            {
                if (number < 0.001f)
                {
                    number = 0;
                }
                return;
            }

            number *= GlobalDefine.Instance.bigNumberIntervel;
            --typeStage;
            power = GlobalDefine.Instance.bigNumberDic[typeStage].min;
            type = GlobalDefine.Instance.bigNumberDic[typeStage].suffix;
        }        
    }

    public uint ChangeDeltaToPower(uint _delta)
    {
        if (_delta > 8)
        {
            return 0;
        }
        else
        {
            uint res = 1;
            for (uint i = 0; i < _delta; ++i)
            {
                res = res * 10;
            }
            return res;
        }
    }

    public static BigNumber operator + (BigNumber _data1, BigNumber _data2)
    {
        BigNumber _res = new BigNumber();
        int _delta = (int)_data1.power - (int)_data2.power;
        
        if (_delta < 0)
        {
            _res.AssignParam(_data2);
            if (_delta > -GlobalDefine.Instance.bigNumberIntervelIgnoreBit)
            {
                _res.number += _data1.number / _res.ChangeDeltaToPower((uint)-_delta);
            }            
        }
        else if (_delta > 0)
        {
            _res.AssignParam(_data1);
            if (_delta < GlobalDefine.Instance.bigNumberIntervelIgnoreBit)
            {
                _res.number = _data1.number + _data2.number / _res.ChangeDeltaToPower((uint)_delta);
            }
            
        }
        else
        {
            _res.AssignParam(_data1);
            _res.number = _data1.number + _data2.number;
        }

        _res.KeepNumberInStage();
        return _res;
    }

    public static BigNumber operator - (BigNumber _data1, BigNumber _data2)
    {
        BigNumber _res = new BigNumber { power = _data1.power };
        int _delta = (int)_data1.power - (int)_data2.power;
        if (_delta < 0)
        {
            _res.AssignParam(_data2);
            if (_delta > -GlobalDefine.Instance.bigNumberIntervelIgnoreBit)
            {
                _res.number = _data2.number - _data1.number / _res.ChangeDeltaToPower((uint)-_delta);
            }
        }
        else if (_delta > 0)
        {
            _res.AssignParam(_data1);
            if (_delta < GlobalDefine.Instance.bigNumberIntervelIgnoreBit)
            {
                _res.number = _data1.number - _data2.number / _res.ChangeDeltaToPower((uint)_delta);
            }
        }
        else
        {
            _res.AssignParam(_data1);
            _res.number = _data1.number - _data2.number;
        }

        _res.KeepNumberInStage();
        return _res;
    }

    public static BigNumber operator * (BigNumber _data1, BigNumber _data2)
    {
        BigNumber _res = new BigNumber
        {
            number = _data1.number * _data2.number,
            power = _data1.power + _data2.power
        };

        _res.KeepNumberInStage();
        return _res;
    }

    public static BigNumber operator / (BigNumber _data1, BigNumber _data2)
    {
        BigNumber _res = new BigNumber();

        if (_data1.power < _data2.power)
        {
            if (_data2.power - _data1.power < GlobalDefine.Instance.bigNumberIntervelIgnoreBit)
            {
                _res.AssignParam(_data2);
                _res.number = (_data1.number / GlobalDefine.Instance.bigNumberIntervel) / _data2.number;
            }
            else
            {
                _res.Reset();
            }
        }
        else
        {
            _res.AssignParam(_data1);
            _res.number = _data1.number / _data2.number;
            _res.power = _data1.power - _data2.power;
        }     

        _res.KeepNumberInStage();
        return _res;
    }

    public static bool operator > (BigNumber _data1, BigNumber _data2)
    {
        if (_data1.power > _data2.power)
        {
            return true;
        }
        else if (_data1.power < _data2.power)
        {
            return false;
        }
        else
        {
            if (_data1.number > _data2.number)
            {
                return true;
            }                
            else
            {
                return false;
            }                
        }
    }

    public static bool operator >= (BigNumber _data1, BigNumber _data2)
    {
        if (_data1.power > _data2.power)
        {
            return true;
        }
        else if (_data1.power < _data2.power)
        {
            return false;
        }
        else
        {
            if (_data1.number >= _data2.number)
            {
                return true;
            }                
            else
            {
                return false;
            }                
        }
    }

    public static bool operator < (BigNumber _data1, BigNumber _data2)
    {
        if (_data1.power > _data2.power)
        {
            return false;
        }
        else if (_data1.power < _data2.power)
        {
            return true;
        }
        else
        {
            if (_data1.number < _data2.number)
            {
                return true;
            }                
            else
            {
                return false;
            }                
        }
    }

    public static bool operator <= (BigNumber _data1, BigNumber _data2)
    {
        if (_data1.power > _data2.power)
        {
            return false;
        }
        else if (_data1.power < _data2.power)
        {
            return true;
        }
        else
        {
            if (_data1.number <= _data2.number)
            {
                return true;
            }                
            else
            {
                return false;
            }                
        }
    }

    public static bool operator == (BigNumber _data1, BigNumber _data2)
    {
        if (_data1.power == _data2.power && _data1.number == _data2.number)
        {
            return true;
        }            
        else
        {
            return false;
        }
            
    }

    public static bool operator != (BigNumber _data1, BigNumber _data2)
    {
        if (_data1.power == _data2.power && _data1.number == _data2.number)
            return false;
        else
            return true;
    }

    public override bool Equals(object _obj)
    {
        BigNumber _data = (BigNumber)_obj;
        if (power == _data.power && number == _data.number)
            return true;
        else
            return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public string GetValue()
    {
        return number.ToString() + type;
    }
}