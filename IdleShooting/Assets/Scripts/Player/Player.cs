using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingleInstance<Player> {

    private BigNumber m_gold;
    public BigNumber Gold
    {
        get { return m_gold; }
        set { m_gold = value; }
    }


    public Player()
    {
        m_gold.Set(100, 0);
    }

    public void AddGold(BigNumber _number)
    {
        m_gold = m_gold + _number;
        GameManager.Instance.RefreashGoldText();
    }

    public void MinusGold(BigNumber _number)
    {
        m_gold = m_gold - _number;
        GameManager.Instance.RefreashGoldText();
    }

    public bool JudgeLevelUpGold(BigNumber _needGold)
    {
        if (m_gold >= _needGold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
