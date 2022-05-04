using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class waterSliderController : MonoBehaviour
{
    public Slider waterSlider, playerSlider;

    public GameObject water, player, moon;

    // Use this for initialization
    void Start()
    {
        waterSlider.minValue = playerSlider.minValue = 0;
        waterSlider.maxValue = waterSlider.maxValue = moon.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            waterSlider.value = water.transform.position.y + 25;
            playerSlider.value = player.transform.position.y-15;
        }

    }
}