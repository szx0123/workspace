using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Vector3 direction;
    public float projectileVelocity;
    public float lifeTimer; //这颗子弹没有击中任何物体最多存活多久

    private BigNumber m_damage;
    private Rigidbody2D m_rb;
    private Vector3 m_targetPos;

    public void Init(BigNumber _heroAttack, Vector3 _pos)
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_damage = _heroAttack;
        m_targetPos = _pos;

        Vector3 _vec = m_targetPos - Camera.main.WorldToScreenPoint(transform.position);
        _vec.z = 0;
        float _angle = Mathf.Atan2(_vec.x, _vec.y) * Mathf.Rad2Deg - 90;
        transform.Rotate(0, 0, _angle);
        _vec.Normalize();
        m_rb.velocity = _vec * 10f;
    }
	
	void Update ()
    {
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject _enemy = collision.gameObject;
            EnemyController _controller = _enemy.GetComponent<EnemyController>();
            _controller.OnHurt(m_damage);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {

        }
    }
}
