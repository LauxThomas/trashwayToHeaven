using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    private float time;
    public float addItemEveryXSeconds = 1.0f;
    public Image slot1, slot2, slot3;
    public Item item;

    public void AddItem()
    {
        if (slot1.sprite == item.getImage(10))
        {
            slot1.sprite = item.getImage(Random.Range(0, 9));
        }
        else if (slot2.sprite == item.getImage(10))
        {
            slot2.sprite = item.getImage(Random.Range(0, 9));
        }
        else if (slot3.sprite == item.getImage(10))
        {
            slot3.sprite = item.getImage(Random.Range(0, 9));
        }
        
    }

    private void Start()
    {
        AddItem();
        AddItem();
        AddItem();
    }

    //Methode if Inventory is Empty
    private void Update()
    {
        time += Time.deltaTime;
        getCurrentBrick();
        if (time >= addItemEveryXSeconds)
        {
            if (slot3.sprite == item.getImage(10))
            {
                AddItem();
            }

            time = 0.0f;
        }

    }

    public void MoveAllItemsToLeft()
    {
        slot1.sprite = slot2.sprite;
        slot2.sprite = slot3.sprite;
        slot3.sprite = item.getImage(10);
    }

    public int getCurrentBrick()
    {
        //TODO: kann man ebstimmt mit nem switch case lösen
        if (slot1.sprite == item.getImage(0))
        {
            return 0;
        }
        if (slot1.sprite == item.getImage(1))
        {
            return 1;
        }
        if (slot1.sprite == item.getImage(2))
        {
            return 2;
        }
        if (slot1.sprite == item.getImage(3))
        {
            return 3;
        }
        if (slot1.sprite == item.getImage(4))
        {
            return 4;
        }
        if (slot1.sprite == item.getImage(5))
        {
            return 5;
        }
        if (slot1.sprite == item.getImage(6))
        {
            return 6;
        }
        if (slot1.sprite == item.getImage(7))
        {
            return 7;
        }
        if (slot1.sprite == item.getImage(8))
        {
            return 8;
        }
        if (slot1.sprite == item.getImage(9))
        {
            return 9;
        }

        return -1;
    }
}