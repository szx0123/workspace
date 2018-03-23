using UnityEngine;
using UnityEngine.EventSystems;

public class ClickController : SingleMonoBehaviour<ClickController> {

    public float singleClickInterval = 0.1f;
    private float nextClickTime = 0.0f;

    void Update () {
        if (Time.time < nextClickTime)
            return;

        if (Input.GetKey(KeyCode.Mouse0))
        {
#if IPHONE || ANDROID
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#else
            if (EventSystem.current.IsPointerOverGameObject())
#endif
            {

            }
            else
            {
                if (HeroManager.Instance == null)
                {
                    Debug.Log("HeroManager.Instance == null");
                    return;
                }
                HeroManager.Instance.MainHero.Shoot(Input.mousePosition);
                ClickCoin();
            }
            
            nextClickTime = Time.time + singleClickInterval;
        }
    }

    private void ClickCoin()
    {
        Collider2D[] col = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (col.Length > 0)
        {
            foreach (Collider2D c in col)
            {
                if (c.tag == "Coin")
                {
                    Debug.Log("Click Coin!");
                    c.GetComponent<Coin>().PickUpCoin(HeroManager.Instance.MainHero.gameObject.transform);
                    return;
                }
            }
        }
    }
}
