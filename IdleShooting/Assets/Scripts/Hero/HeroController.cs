using UnityEngine;

public class HeroController : MonoBehaviour
{
    public float speed; //控制角色移动速度
    public Transform muzzleFlashPoint; //开枪火花的触发坐标
    public AudioClip bulletSound;

    private Rigidbody2D rb;
    private Animator animator;
    private float nextFire;

    private HeroData m_data;

    public HeroData Data
    {
        get { return m_data; }
    }

    public void Init()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        m_data = gameObject.AddComponent<HeroData>();
        m_data.Init();
        nextFire = 0;
    }

    private void Update()
    {
        Movement();
    }

    /// <summary>
    /// 控制角色的射击
    /// </summary>
    public void Shoot(Vector3 _pos)
    {
        //用左键射击，判断射击频率
        if (Time.time > nextFire)
        {
            nextFire = Time.time + m_data.AttackSpeed;
            Instantiate(Resources.Load("Prefabs/MuzzleFlash"), muzzleFlashPoint.position, muzzleFlashPoint.rotation);
            Bullet _bullet = (Instantiate(Resources.Load("Prefabs/Bullet"), muzzleFlashPoint.position, muzzleFlashPoint.rotation) as GameObject).GetComponent<Bullet>();
            _bullet.Init(m_data.Damage, _pos);
            animator.SetTrigger("playerShoot");

            SoundManager.instance.PlaySingle(bulletSound);
        }
    }

    private void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement;

        movement = new Vector2(moveHorizontal, moveVertical);

        rb.velocity = movement * speed;

        if (Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            animator.SetBool("playerRun", true);
        }
        else
        {
            animator.SetBool("playerRun", false);
        }
    }

    public void HeroLevelUp()
    {
        if (!Player.Instance.JudgeLevelUpGold(m_data.LevelUpNeedGold))
        {
            return;         //金币不够
        }
        HeroConfig _config = HeroConfigManager.Instance.data[++m_data.Level];
        if (_config == null)
        {
            Debug.LogError("Hero level is max!");
            return;
        }

        Player.Instance.MinusGold(m_data.LevelUpNeedGold);
        m_data.HeroDataCopy(_config);
    }

    public uint Level
    {
        get { return m_data.Level; }
    }
}
