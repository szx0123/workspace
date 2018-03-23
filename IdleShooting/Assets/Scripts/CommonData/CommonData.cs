using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData : MonoBehaviour{

    private uint level;//角色等级
    private BigNumber hp;//角色的生命值
    private string characterName;//角色名称
    private BigNumber damage;//角色伤害
    private float attackSpeed;//角色攻击频率
    private float characterAttackRange;//角色攻击距离    
    private bool isDamaged;//角色是否被攻击

    public CommonData()
    {
        this.level = 1;
        this.characterName = "";
        this.hp.Set(100, 0);
        this.isDamaged = false;
    }

    public void CommonDataCopyHeroData(HeroConfig _config)
    {
        this.Level = _config.m_Level;
        this.Hp = _config.m_HP;
        this.Damage = _config.m_Attack_Damage;
        this.AttackSpeed = _config.m_Attack_Speed;
    }

    //封装角色等级
    public uint Level
    {
         get { return level; }
         set { level = value; }
    }
    
    //封装角色生命值
    public BigNumber Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    public void SetHp(float _number = 0f, uint _power = 0)
    {
        hp.Set(_number, _power);
    }

    //封装角色姓名
    public string CharacterName
    {
        get { return characterName; }
        set { characterName = value; }
    }
     //封装角色伤害
    public BigNumber Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    //封装角色攻击速度
    public float AttackSpeed
    {
        get { return attackSpeed; }
        set { attackSpeed = value; }
    }

    //封装角色攻击距离
    public float CharacterAttackRange
    {
        get { return characterAttackRange; }
        set { characterAttackRange = value; }
    }

    //封装角色受伤
    public bool IsDamaged
    {
        get { return isDamaged; }
        set { isDamaged = value; }
    }

    public bool IsDead
    {
        get { return Hp.IsZero(); }
    }
}
