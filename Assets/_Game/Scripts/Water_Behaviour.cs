using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Behaviour : MonoBehaviour {
    [SerializeField]
    private GameObject water;
    [SerializeField]
    private float risingSpeed;
    private float startTime;
    private SpriteRenderer waterSprite;
    private BoxCollider2D waterCollider;
	public GameObject endscreen;
    private bool rise;

	public ManageSounds soundSource;

	// Use this for initialization
	void Start () {
        rise = false;
        waterSprite = water.GetComponent<SpriteRenderer>();
        waterCollider = water.GetComponent<BoxCollider2D>();
        startTime = 60.0f;
	}
	
	// Update is called once per frame
	void Update () {
        startTime -= Time.deltaTime;
//        Debug.Log(Time.deltaTime);
        if(startTime < 0.1f){
            Vector3 oldtrans = waterSprite.transform.position;
            Vector3 newtrans = oldtrans + new Vector3(0, risingSpeed * 0.001f, 0);
            waterSprite.transform.position = newtrans;
        }   
	}

    void OnTriggerEnter2D(Collider2D col)
    {
	    
        if (col.CompareTag("Player")) 
        {
            /*
             ToDo:
             Play dieanimation and sound
             start die logic from player
             */
            GameObject tmp = col.gameObject;
            //Destroy(tmp);
	        tmp.GetComponent<SpriteRenderer>().enabled = false;

	        if (GameObject.FindGameObjectWithTag("Player") != null)
	        {
		        soundSource.PlayWaterSplash();
	        }
	        
	        //TODO: Show EndScreen
	        endscreen.SetActive(true);


        }
    }
}

