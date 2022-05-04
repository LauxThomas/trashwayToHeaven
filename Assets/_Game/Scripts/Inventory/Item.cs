using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Sprite[] imageDatabase;

    public Sprite getImage(int i)
    {
        return imageDatabase[i];
    }
}