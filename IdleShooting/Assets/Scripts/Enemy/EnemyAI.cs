using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public Vector3 playerLastPos;
    public bool moving = true;
    public float speed;
    
    private GameObject player;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLastPos = player.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        movement();
	}

    void movement()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

}
