using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class animatorScript : MonoBehaviour
{
    private Animator animator;
    public GameObject player;
    private float freeDistance;
    public float freeVelocityY, freeVelocityX;
    private float oldX, newX;

    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        freeDistance = player.GetComponent<PlayerController>().freeDistance;
        freeVelocityY = player.GetComponent<PlayerController>().freeVelocityY;
        freeVelocityX = player.GetComponent<PlayerController>().freeVelocityX;
        newX = player.transform.position.x;
    }


    // Update is called once per frame
    void Update()
    {
        freeDistance = player.GetComponent<PlayerController>().freeDistance;
        freeVelocityY = player.GetComponent<PlayerController>().freeVelocityY;
        freeVelocityX = player.GetComponent<PlayerController>().freeVelocityX;

        animator.SetFloat("jumpHeight", freeDistance);
        animator.SetFloat("velocityY", freeVelocityY);
        animator.SetFloat("velocityX", freeVelocityX);

        //        newX = player.transform.position.x;
        //        if (newX > oldX)
        //        {
        //            animator.SetBool("isRunning",true);
        //        }
        //        else
        //        {
        //            animator.SetBool("isRunning",false);
        //        }

        newX = player.transform.position.x - oldX;
        oldX = player.transform.position.x;
        //float xValue = player.GetComponent<Rigidbody2D>().velocity.x;
        //Debug.Log(xValue);
        if (newX > 0.05)
        {
            animator.SetBool("run", true);
            animator.SetBool("block", false);
        }
        else if (newX < -0.05)
        {
            animator.SetBool("block", true);
            animator.SetBool("run", false);
        }
        else
        {
            animator.SetBool("run", false);
            animator.SetBool("block", false);
        }


        //animator.Update(Time.deltaTime);
        if (Input.GetButtonDown("BuildBlock"))
        {
            animator.SetTrigger("throwTrigger");
        }

        if (player.transform.position.x != 0 && !(newX > oldX))
        {
            animator.SetBool("isOnWall", true);
        }
        else
        {
            animator.SetBool("isOnWall", false);
        }

        oldX = player.transform.position.x;
    }
}