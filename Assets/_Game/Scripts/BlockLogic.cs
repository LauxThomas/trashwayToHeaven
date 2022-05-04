using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockLogic : MonoBehaviour
{
    //public GameObject tStone, sStone, zStone, oStone, iStone, lStone, jStone, trampolinStone, oilStone, gumStone;
    public GameObject player;

    public LayerMask OnlyThisLayer;

    //Arrays of Blocks
    public GameObject[] Trampolin_Rotations;
    public GameObject[] Z_Rotations;
    public GameObject[] S_Rotations;
    public GameObject[] Oil_Rotations;
    public GameObject[] O_Rotations;
    public GameObject[] L_Rotations;
    public GameObject[] J_Rotations;
    public GameObject[] Gum_Rotations;
    public GameObject[] I_Rotations;
    public GameObject[] T_Rotations;

    //Others
    public Inventory inventory;
    public float DistanceFromPreviewToPlayer;
    public float buildingCD;
    public Vector3 blockStartPosition;
    public ManageSounds soundSource;

    private float buildingDCHelper;
    private GameObject[][] trashminoes;

    //next Block in Line
    private GameObject stoneReference;
    //Save stoneRef in Temp
    private GameObject tmp;

    private float gridSize;
    private int lastYpos;
    private float lastXPos;
    private Boolean trashminoeStopped;
    private int rotationIndex = 0;
    private float previewBlockFloat;
    private int currentBlock;

    private Boolean blockPlaced = false;
    //change to false, check in MoveBlockUpAgain()
    private bool hasItems = true;

    void Start()
    {
        trashminoeStopped = false;
        trashminoes = new GameObject[][]
        {
            J_Rotations, L_Rotations, O_Rotations, Z_Rotations, I_Rotations, T_Rotations, S_Rotations,
            Trampolin_Rotations, Oil_Rotations, Gum_Rotations
        };
        gridSize = GetComponent<GameManager>().GridSize;
        blockStartPosition = new Vector3(-5 * gridSize, 10 * gridSize + ((int) player.transform.position.y * gridSize), player.transform.position.z);
        getNewStoneReference();

    }

    void Update()
    {
        if (stoneReference == null)
        {
            getNewStoneReference();
        }

        if (stoneReference != null)
        {
            //Rotate Block
            RotateBlock();
   
            MoveBlockUpAgain();

            //change xpos
            previewBlockFloat = (previewBlockFloat + GameManager.getInz().BackgroundVelocity) %
                                GameManager.getInz().GridSize;
            SetCorrectXPos();


            Transform[] transforms = stoneReference.GetComponentsInChildren<Transform>();
            //preview block placement in y
            while (!trashminoeStopped)
            {
                if (trashminoeStopped)
                    break;

                stoneReference.transform.Translate(new Vector3(0, -gridSize, 0));

                RaycastHit2D[] rayHits = new RaycastHit2D[transforms.Length];

                for (int i = 2; i < transforms.Length; ++i)
                {
                    rayHits[i] = Physics2D.Raycast(transforms[i].transform.position, Vector2.down, 50f, OnlyThisLayer);

                    //Debug.DrawRay(transforms[i].transform.position,Vector2.down, Color.red);
                }

                for (int k = 2; k < rayHits.Length; ++k)
                {
                    RaycastHit2D hit = rayHits[k];
                    if (hit.distance < gridSize)
                    {
                        //Debug.Log("looop :" +" dist " + hit.distance + "object: "+hit.collider.gameObject.name);
                        trashminoeStopped = true;
                    }
                }
            }

            buildingDCHelper -= Time.deltaTime;
            
            SetBlock();

        }
    }

    private void SetBlock()
    {
        //Click to Build new Block
        if (Input.GetButtonDown("BuildBlock") && buildingDCHelper <= 0)
        {
            //Renderstuff
            blockPlaced = true;
            SetColor();
            UpdateParticleSystemPos();
            //particleSystem.Play();
            this.GetComponentInChildren<ParticleSystem>().Play();

            //Sound    
            soundSource.PlayPlaceBlockSound();

            //Instanciate and Update
            GameManager.MyInstance.buildBlock(stoneReference.transform.position, stoneReference);
                    
            getNewStoneReference(); // holt fuer rechts nen neuen block
            inventory.MoveAllItemsToLeft();
            buildingDCHelper = buildingCD;
        }
    }

    public void RotateBlock()
    {
       if (Input.GetButtonDown("RotateBlock"))
       {
            blockPlaced = false;
            Destroy(stoneReference);
            soundSource.PlayRotateBlockSound();
            stoneReference = (GameObject) Instantiate(trashminoes[currentBlock][++rotationIndex % 4], blockStartPosition, Quaternion.identity);
            SetColor();
            trashminoeStopped = false;
        }

    }



    private void UpdateParticleSystemPos()
    {
        //try to make offset +x 
        transform.position = player.transform.position;
    }

    //Run if Particle hits otherCollider
    // public void OnParticleCollision(GameObject other)
    // {
    //     Debug.Log("Collide");
    //     tmp.GetComponentInChildren<SpriteRenderer>().color = new Color(1,1,1,1);

    // }

    //Set Color after blocks is Instanziated
    private void SetColor()
    {
    
        if(!blockPlaced)
            stoneReference.GetComponentInChildren<SpriteRenderer>().color = new Color(0.3f, 0.8f, 0.3f, 0.5f);

    }

    private void SetCorrectXPos()
    {
        if (player != null)
        {
            float offsetplayer = (float) ((int) (-player.transform.position.x / 1.28f) * 1.28);
            stoneReference.transform.position =
                new Vector3(-(previewBlockFloat + DistanceFromPreviewToPlayer) - offsetplayer,
                    stoneReference.transform.position.y, stoneReference.transform.position.z);
        }
    }

    private void MoveBlockUpAgain()
    {
        trashminoeStopped = false;
        if (hasItems)
        {
            stoneReference.transform.position = new Vector3(stoneReference.transform.position.x,
                stoneReference.transform.position.y + 10 * gridSize, stoneReference.transform.position.z);
        }
    }

    private void getNewStoneReference()
    {

        if (stoneReference != null)
        {
            Destroy(stoneReference);
        }

        currentBlock = player.GetComponent<Inventory>().getCurrentBrick();
        if (currentBlock < 10 && currentBlock >= 0)
        {
            stoneReference = (GameObject) Instantiate(trashminoes[currentBlock][0], blockStartPosition, Quaternion.identity);
            blockPlaced = false;
            SetColor();
            trashminoeStopped = false;
        }

    }

    public void stopTrashminoe()
    {
        stoneReference.transform.position = new Vector3(stoneReference.transform.position.x, lastYpos, stoneReference.transform.position.z);
        trashminoeStopped = true;
    }
}