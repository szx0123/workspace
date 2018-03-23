using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : MonoBehaviour {

    private Button m_levelUpBtn;
    private Button m_buyBtn;    
    private Text m_levelText;
    private List<Button> m_skillBtn;
    private Text m_fightPowerText;

    private int m_index;

    public Button LevelUpBtn
    {
        get { return m_levelUpBtn; }
    }
    public Button BuyBtn
    {
        get { return m_buyBtn; }
    }
    public Text LevelText
    {
        get { return m_levelText; }
    }
    public List<Button> SkillBtn
    {
        get { return m_skillBtn; }
    }
    public Text FightPowerText
    {
        get { return m_fightPowerText; }
    }

    public void Init(int _index)
    {
        m_index = _index;
        m_levelUpBtn = transform.Find("LevelUpButton").GetComponent<Button>();
        m_buyBtn = transform.Find("BuyButton").GetComponent<Button>();
        m_levelText = transform.Find("LevelText").GetComponent<Text>();
        m_skillBtn = new List<Button>();
        Transform _trans = transform.Find("Skills");
        for (uint i = 0; i < _trans.childCount; ++i)
        {
            m_skillBtn.Add(_trans.Find("Skill" + (i + 1).ToString()).GetComponent<Button>());
        }
        m_fightPowerText = transform.Find("FightPowerText").GetComponent<Text>();
        m_levelUpBtn.onClick.AddListener(OnClickLevelUpButton);
        m_buyBtn.onClick.AddListener(OnClickBuyBtn);
    }

    public void SetCurLevel(uint _level)
    {
        m_levelText.text = "当前等级:" + _level.ToString();
    }

    public void SetCurFightPower(BigNumber _value)
    {
        m_fightPowerText.text = "战斗力:" + _value.GetValue();
    }

    private void OnClickLevelUpButton()
    {
        HeroManager.Instance.HeroLevelUp(m_index);
    }

    private void OnClickBuyBtn()
    {
        HeroManager.Instance.BuyHero();
    }
}
