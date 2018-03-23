using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CommonData
{
    private BigNumber m_gold;

    private EnemyBase()
    {
        m_gold.Set(1,2);
    }

    public void OnHurt(BigNumber _damage)
    {
        if (Hp > _damage)
        {
            Hp = Hp - _damage;
        }
        else
        {
            SetHp(0, 0);
        }
    }

    public BigNumber Gold
    {
        get { return m_gold; }
    }
}
