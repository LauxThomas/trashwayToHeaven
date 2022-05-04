using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;



public class BGMManager : MonoBehaviour
{

    public AudioSource musicManager;
    //music
    public AudioClip earthMusic;

    //public ManageSounds soundManager;
    //private Transform volumeSlider;
    private GameObject volumeSlider;
    private float masterVolume;
    private static bool created = false;
    // Use this for initialization
    void Start()
    {
        volumeSlider = GameObject.Find("Slider");
        //volumeSlider = transform.Find("SliderV");
        //Debug.Log(volumeSlider);

        if (!created)
        {
            DontDestroyOnLoad(gameObject);

            if (File.Exists(Application.persistentDataPath + "/volumeInfo.dat"))
            {
                Load();
                if (volumeSlider != null)
                {
                    SetSlider();
                }

                //masterVolume = 0.5f;
            }
            else
            {
                masterVolume = 0.5f;
                if (volumeSlider != null)
                {
                    SetSlider();
                }
            }

            musicManager.volume = masterVolume / 4;
            musicManager.clip = earthMusic;
            musicManager.Play();
            created = true;

        }




    }


    public void AdjustVolume(Slider x)
    {
        masterVolume = x.value;
        musicManager.volume = masterVolume / 4;
        Save();
    }

    private void SetSlider()
    {
        //volumeSlider.GetComponent<Slider>().value = masterVolume;
        volumeSlider.GetComponent<Slider>().value = masterVolume;
        Save();

    }

    // Persistent volume code
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/volumeInfo.dat");
        VolumeData data = new VolumeData();
        data.mVolume = masterVolume;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/volumeInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/volumeInfo.dat", FileMode.Open);
            VolumeData data = (VolumeData)bf.Deserialize(file);
            file.Close();
            masterVolume = data.mVolume;
        }
    }

    //Update is called once per frame
    void Update()
    {
    }
}

[Serializable]
class VolumeData
{
    public float mVolume;
}