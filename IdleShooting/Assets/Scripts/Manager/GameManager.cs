using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : SingleMonoBehaviour<GameManager>
{
    public float levelStartDelay = 2f;
    public float turnDelay = .1f;
    public int playerFoodPoints = 100;
    [HideInInspector] public bool playersTurn = true;

    private int level = 1;

    private Text m_goldText;
    private Text m_fightPowerText;
    private BigNumber m_fightPower = new BigNumber();
    private float m_attackTime = 0f;

    private void Start()
    {
        m_goldText = GameObject.Find("Canvas/UIGroup_Gold/Text_CoinNumber").GetComponent<Text>();
        m_fightPowerText = GameObject.Find("Canvas/UIGroup_DPS/Text_DPSNumber").GetComponent<Text>();

        ConfigDataManager.Instance.LoadConfig();
        EnemyManager.Instance.Create();
        HeroManager.Instance.Create();
        Player.Instance.Create();

        InitGame();
    }

    void InitGame()
    {
        RefreashGoldText();
        RefreashFightPowerText();
    }

    private void Update()
    {
        m_attackTime = m_attackTime - Time.deltaTime;
        if (m_attackTime <= 0f)
        {
            for (int i = 0; i < EnemyManager.Instance.Enemys.Count; ++i)
            {
                EnemyManager.Instance.Enemys[i].OnHurt(m_fightPower);
            }
            m_attackTime = GlobalDefine.Instance.autoAttackInterval;
        }
    }

    void HideLevelImage()
    {
        //levelImage.SetActive(false);
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {        
        InitGame();

        ++level;
    }

    void OnEnable()
    {
        //SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        //SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    public void Gameover()
    {
        //levelText.text = "After " + level + " days, you starved.";
        //levelImage.SetActive(true);     

        enabled = false;
    }

    public void RefreashGoldText()
    {
        m_goldText.text = "Gold:" + Player.Instance.Gold.GetValue();
    }

    public void RefreashFightPowerText()
    {
        m_fightPower = HeroManager.Instance.CountAllHeroFightPower();
        m_fightPowerText.text = "DPS:" + m_fightPower.GetValue();
    }
}
