using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour 
{

	// Update is called once per frame
	void Update ()
	{
		transform.position -= new Vector3(GameManager.getInz().BackgroundVelocity,0,0);
		//GetComponent<Rigidbody2D>().velocity = new Vector3(GameManager.getInz().Velocity * -1,0,0);
		if (transform.position.x < GameManager.getInz().getSolidGroundStart())
		{
            /*transform.position = new Vector3(
				transform.position.x + (GameManager.getInz().getSolidGroundTextureSize().x * GameManager.getInz().SolidGroundObject.transform.localScale.x)+ GameManager.getInz().placementOffset,
				transform.position.y,
				transform.position.z
				);*/

            transform.position = new Vector3(
                    transform.position.x + GameManager.getInz().placementOffset,
                    transform.position.y,
                    transform.position.z
                    );
        }	
	}
}
