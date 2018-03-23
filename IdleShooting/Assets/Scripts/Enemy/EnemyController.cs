using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public GameObject tombPrefab;
    public GameObject coinPrefab;

    private Slider m_hpBar;
    private EnemyBase m_data;
    private Animator m_anim;

    private SpriteRenderer sr;
    private int blink = 0;
    private Color cr;
    private float blinkspeed = 5f;
    private bool b_isDead = false;

    void Start () {
        m_hpBar = transform.Find("HPBarPoint/HpBar").GetComponent<Slider>();
        m_data = GetComponent<EnemyBase>();
        m_anim = GetComponent<Animator>();

        sr = this.GetComponent<SpriteRenderer>();
        cr = this.GetComponent<SpriteRenderer>().color;

        Init();
    }

    void Update()
    {
        OnHurtBlinkUpdate();
    }

    private void OnDestroy()
    {
        b_isDead = true;
    }

    public void Init()
    {
        m_hpBar.maxValue = m_data.Hp.Number;
        m_hpBar.value = m_hpBar.maxValue;
    }

    public void OnHurt(BigNumber _damage)
    {
        if (b_isDead)
            return;

        m_data.OnHurt(_damage);
        m_hpBar.value = m_data.Hp.Number;
        if (m_data.IsDead)
        {
            b_isDead = true;
            Dead();
        }
        else
        {
            sr.color = Color.Lerp(Color.white, Color.clear, blinkspeed * Time.deltaTime);
            blink = 2;
        }
    }

    private void Dead()
    {
        GameObject _tomb = Instantiate(tombPrefab, this.transform.position, this.transform.rotation);
        Coin _coin = (Instantiate(coinPrefab, this.transform.position, this.transform.rotation) as GameObject).GetComponent<Coin>();
        _coin.Number = m_data.Gold;

        EnemyManager.Instance.Die(this);
        Destroy(_tomb, 5f);
        Destroy(gameObject);
    }

    private void OnHurtBlinkUpdate()
    {
        if (blink == 2)
        {
            blink--;
        }
        else if (blink == 1)
        {
            sr.color = cr;
            blink--;
        }
    }
}
