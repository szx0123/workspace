using System.Collections.Generic;
using UnityEngine;

public class HeroManager : SingleMonoBehaviour<HeroManager> {

    public GameObject controlUI;

    private List<HeroController> m_heroController;
    private List<HeroUI> m_heroUIs;
    private Transform[] m_refreshPoint;

    private int m_index;       //当前购买到了几号角色,几号角色需要顺序购买,即:若4号角色未购买,则无法购买5号角色

    public HeroController MainHero 
    {
        get { return m_heroController[0]; }
    }
    public List<HeroController> Heros
    {
        get { return m_heroController; }
    }

    public override void Create()
    {
        m_index = 0;
        m_heroController = new List<HeroController>();
        m_heroUIs = new List<HeroUI>();
        m_refreshPoint = GameObject.Find("PlayerPoint").GetComponentsInChildren<Transform>();
        CreateHeroUI(m_index);
        BuyHero();
    }

    private void CreateHero(int _index)
    {
        GameObject _heroObj = Instantiate(Resources.Load("Prefabs/Player"), m_refreshPoint[_index + 1]) as GameObject;
        if (_heroObj == null)
        {
            Debug.LogError("Instantiate error!HeroManager::Start");
            return;
        }        
        _heroObj.name = "Player" + (_index + 1).ToString();
        HeroController _ctrl = _heroObj.GetComponent<HeroController>();
        _ctrl.Init();
        m_heroController.Add(_ctrl);
    }

    private void CreateHeroUI(int _index)
    {
        GameObject _controlUI = Instantiate(controlUI, GameObject.Find("Canvas/HeroUIPosition/Viewport/Content").GetComponent<Transform>());
        HeroUI _heroUI = _controlUI.GetComponent<HeroUI>();
        if (_heroUI == null)
        {
            Debug.LogError("_controlUI.GetComponent<HeroUI>() == null");
        }
        _heroUI.Init(_index);
        _heroUI.BuyBtn.gameObject.SetActive(true);
        _heroUI.LevelUpBtn.gameObject.SetActive(false);
        _heroUI.LevelText.gameObject.SetActive(false);
        _heroUI.SkillBtn[0].gameObject.SetActive(true);
        for (int i = 1; i < _heroUI.SkillBtn.Count; ++i)
        {
            _heroUI.SkillBtn[i].gameObject.SetActive(false);
        }
        _heroUI.FightPowerText.gameObject.SetActive(false);
        m_heroUIs.Add(_heroUI);
    }

    public void HeroLevelUp(int _index)
    {
        m_heroController[_index].HeroLevelUp();
        m_heroUIs[_index].SetCurLevel(m_heroController[_index].Data.Level);
        m_heroUIs[_index].SetCurFightPower(m_heroController[_index].Data.Damage);
        GameManager.Instance.RefreashFightPowerText();
    }

    public void BuyHero()
    {
        CreateHero(m_index);
        m_heroUIs[m_index].BuyBtn.gameObject.SetActive(false);
        m_heroUIs[m_index].LevelUpBtn.gameObject.SetActive(true);
        m_heroUIs[m_index].LevelText.gameObject.SetActive(true);
        m_heroUIs[m_index].SetCurLevel(m_heroController[m_index].Data.Level);
        m_heroUIs[m_index].FightPowerText.gameObject.SetActive(true);
        m_heroUIs[m_index].SetCurFightPower(m_heroController[m_index].Data.Damage);
        CreateHeroUI(++m_index);
        GameManager.Instance.RefreashFightPowerText();
    }

    public BigNumber CountAllHeroFightPower()
    {
        BigNumber _value = new BigNumber();
        for (int i = 0; i < m_index; ++i)
        {
            _value = _value + m_heroController[i].Data.Damage;
        }
        return _value;
    }
}
