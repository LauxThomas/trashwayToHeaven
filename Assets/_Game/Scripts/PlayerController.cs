using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float freeDistance, freeVelocityY, freeVelocityX;
    public LayerMask BuildLayers, BuildLayers2;
    //Managers
    public GameManager gameManager;
    public ManageSounds soundSource;


    [SerializeField] private float JumpForce;
    [SerializeField] private float Velocity = 2f;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private float moveToCenterSpeed;

    private float originalJumpforce, originalSpeed;
    private float JumpForceTmp;
    private string blockUnderPlayer;

    private bool isJump = false;
    private bool blockBuild = false;

    private float previewDummyFloat = 0;

    private Rigidbody2D rdb;


    // Use this for initialization
    void Start()
    {
        rdb = GetComponent<Rigidbody2D>();
        originalJumpforce = JumpForce;
        originalSpeed = gameManager.BackgroundVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        freeVelocityY = rdb.velocity.y;
        freeVelocityX = rdb.velocity.x;
        RaycastHit2D jumpRayfree = Physics2D.Raycast(new Vector2(transform.position.x + 1.28f, GameManager.getPlayerBottomPositionY()), Vector2.down, 500f, BuildLayers2);
        //freeDistance = GameManager.getPlayerBottomPositionY() - jumpRayfree.transform.position.y - 1.35f;
        freeDistance = jumpRayfree.distance;
        //show Raycasts
        //Debug.DrawRay(new Vector2(transform.position.x + 1.28f, GameManager.getPlayerBottomPositionY()), Vector2.down, Color.red);

        //handle changes of speed and JumpForce when on Trampoline, Gum or Oil
        blockUnderPlayer = jumpRayfree.collider.name.ToLower();
        if (blockUnderPlayer.Contains("trampolin"))
        {
            JumpForce = originalJumpforce * 1.5f;
        }
        else if (blockUnderPlayer.Contains("gum") && freeDistance < 0.3)
        {
            gameManager.BackgroundVelocity = originalSpeed / 2;
        }
        else if (blockUnderPlayer.Contains("oil") && freeDistance < 0.3)
        {
            gameManager.BackgroundVelocity = originalSpeed * 2;
        }
        else
        {
            JumpForce = originalJumpforce;
            gameManager.BackgroundVelocity = originalSpeed;
        }
        // THOMA
        RaycastHit2D craneRay = Physics2D.Raycast(
            new Vector2(transform.position.x + 0.64f, GameManager.getPlayerBottomPositionY()),
            Vector2.down, 500f);

        bool isOnCrane = false;
        if (craneRay.collider.name.ToLower().Contains("kran"))
        {
            isOnCrane = true;
        }
        if (isOnCrane)
        {
            GameObject.Find("Kran").layer = 9;
        }
        else
        {
            GameObject.Find("Kran").layer = 0;
        }


        // //move player back to center of screen
        // if (transform.position.x < 0 )
        // {
            
        //     transform.position = new Vector3(transform.position.x + moveToCenterSpeed, transform.position.y, transform.position.z);
        //     //rdb.AddForce(Vector2.right * Velocity, ForceMode2D.Force);
        //     //blockBuild = true;
        //     //DummyBlockPreview.GetComponent<SpriteRenderer>().enabled = false;
        // }
        // else if (transform.position.x > 0)
        // {
        //     transform.position = new Vector3(0, transform.position.y, transform.position.z);
        //     //blockBuild = false;
        //     //DummyBlockPreview.GetComponent<SpriteRenderer>().enabled = true;
        // }

        if(transform.position.x >=-0.01f  && transform.position.x < 1.0f){
            transform.position = new Vector3(0.0f, transform.position.y, transform.position.z);
        } else if(transform.position.x < -0.01f){
             transform.position = new Vector3(transform.position.x + moveToCenterSpeed, transform.position.y, transform.position.z);
        }
      

        if (Input.GetButton("Jump"))
        {
            if (JumpForceTmp == JumpForce)
            {
                soundSource.PlayJumpSound();
            }

            rdb.velocity = new Vector2(rdb.velocity.x, rdb.velocity.y + JumpForceTmp);
            if (rdb.velocity.y >= 0)
                JumpForceTmp = JumpForceTmp > 0 ? JumpForceTmp / 2f : 0;
            
            //maybe change FOV while jumping to see more of the ground/where the player is building
            //change FOV in relation to yelocity
            /*Debug.Log("yVelocity: " + gameObject.GetComponent<Rigidbody2D>().velocity.y);
            if (gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0.01f && gameObject.GetComponent<Rigidbody2D>().velocity.y>=-0.01f)
                isJump = true;*/
        }

        if (rdb.velocity.y <= 0.01f && rdb.velocity.y >= -0.01f)
        {
            JumpForceTmp = JumpForce;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Implement more stuff that happens after death here
        if (collider.gameObject.CompareTag("DeathWall"))
        {
            endPanel.SetActive(true);
            Destroy(gameObject);
        }
    }
}