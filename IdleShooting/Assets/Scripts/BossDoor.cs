using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour {

    private Animator anim;


	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        OpenDoor();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OpenDoor ()
    {
        anim.SetBool("openDoor", true);
    }
}
