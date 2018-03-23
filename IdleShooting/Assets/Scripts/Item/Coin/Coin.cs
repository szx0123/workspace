using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public float pickUpFlySpeed = 5.0f;
    public float autoPickUpTime = 3.0f;

    private bool b_pickUpFlag = false;
    private Vector3 m_flyToPosition;
    private BigNumber m_number;
    
    public BigNumber Number
    {
        get { return m_number; }
        set { m_number = value; }
    }

	// Use this for initialization
	void Start () {
        Invoke("TimeOutAutoReceive", autoPickUpTime);
    }

    public void PickUpCoin(Transform _tran)
    {
        CancelInvoke();
        FlyToTarget(_tran.position);
    }

    private void TimeOutAutoReceive()
    {
        FlyToTarget(GameObject.Find("Player1").transform.position);
    }

    private void FlyToTarget(Vector3 _pos)
    {
        b_pickUpFlag = true;
        m_flyToPosition = _pos;
    }

    // Update is called once per frame
    void Update () {
		if (b_pickUpFlag)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_flyToPosition, pickUpFlySpeed * Time.deltaTime);
            if (transform.position.Equals(m_flyToPosition))     //拾取都身上
            {
                Player.Instance.AddGold(m_number);
                GameManager.Instance.RefreashGoldText();
                Destroy(gameObject);
            }
        }
	}
}
