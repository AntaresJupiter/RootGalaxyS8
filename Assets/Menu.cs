using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {


public	GameObject panel1;

public	GameObject panel2;

public	GameObject panel3;

	public AudioSource AS;

	public AudioClip ac;


	public Text txt_Zic;
	bool ZIC=true;
	bool sound=true;
	public Text txt_sound;


	public void ZIC_()
	{

		if (ZIC) 
		{
			AS.Stop();
			AS.PlayOneShot (ac);
			txt_Zic.text = "Music <color=Grey>OFF</color>";
			ZIC=false;
		} else 
		{
			AS.PlayOneShot (ac);
			AS.Play();
			ZIC=true;
			txt_Zic.text = "Music <color=green>ON</color>";
		}


	}




	public void Sound_()
	{
		AS.PlayOneShot (ac);
		if (sound) 
		{

			txt_sound.text = "Sound <color=Grey>OFF</color>";
			sound=false;
		} else 
		{

			sound=true;
			txt_sound.text = "Sound <color=green>ON</color>";
		}
		
		
	}






public void play()
	{
		AS.PlayOneShot (ac);
		panel1.SetActive (false);
		panel2.SetActive (true);
	}


	public void setting()
	{
		AS.PlayOneShot (ac);
		panel1.SetActive (false);
		panel3.SetActive (true);
	}


	public void back()
	{
		AS.PlayOneShot (ac);
		panel1.SetActive (true);
		panel3.SetActive (false);
	}


}
