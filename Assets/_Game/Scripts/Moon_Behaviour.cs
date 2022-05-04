using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon_Behaviour : MonoBehaviour {

	public GameObject endScreen;

	void OnTriggerEnter2D(Collider2D col){
		 if (col.CompareTag("Player")) {
			 endScreen.SetActive(true);
		 }
	}
}
