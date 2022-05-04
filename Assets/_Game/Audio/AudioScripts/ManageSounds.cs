using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using File = System.IO.File;

public class ManageSounds : MonoBehaviour
{

	public AudioSource soundManager;
	public AudioClip jumpSound;
	public AudioClip waterSplash;
	public AudioClip blockRotation;
	public AudioClip blockPlacement;
	public AudioClip buttonClick;
	public AudioClip triumph;
	public AudioClip sadTrombone;
	public AudioClip trampolineSound;
	public AudioClip stickyGumSound;
	public AudioClip slidyOilSound;
	
	private float masterVolume;
	
	// Use this for initialization
	void Start () {
		
		if (File.Exists(Application.persistentDataPath + "/volumeInfo.dat"))
		{
			Load();
			soundManager.volume = masterVolume;
		}
		else
		{
			masterVolume = 0.5f;
			soundManager.volume = masterVolume;
		}
		
	}
	
	public void Load()
	{
		if (File.Exists(Application.persistentDataPath + "/volumeInfo.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/volumeInfo.dat",FileMode.Open);
			VolumeData data = (VolumeData)bf.Deserialize(file);
			file.Close();
			masterVolume = data.mVolume;
		}
	}
	// Update is called once per frame
	void Update () {
		
	}

	// sound methoods
	public void PlayJumpSound()
	{
		soundManager.PlayOneShot(jumpSound);
	}

	public void PlayWaterSplash()
	{
		soundManager.PlayOneShot(waterSplash);
	}

	public void PlayClickButtonSound()
	{
		soundManager.PlayOneShot(buttonClick);
	}

	public void PlayPlaceBlockSound()
	{
		soundManager.PlayOneShot(blockPlacement);
	}

	public void PlayRotateBlockSound()
	{
		soundManager.PlayOneShot(blockRotation);
	}
	public void PlayTriumphSound()
	{
		soundManager.PlayOneShot(triumph);
	}

	public void PlaySadTrombone()
	{
		soundManager.PlayOneShot(sadTrombone);
	}
	public void PlayTrampolineSound()
	{
		soundManager.PlayOneShot(trampolineSound);
	}
}
