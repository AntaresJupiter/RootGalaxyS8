using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LOADING2 : MonoBehaviour {



	public Vungle_me vungle;
	public Startapp_me pub2;
	public Text txt;
	public Image img;
	public GameObject error;
void Start () 
	{
		StartCoroutine (bb ());

	}

	IEnumerator bb()
	{
		float waittime = 2.1f;

		txt.text = "GenerATing World .";
		img.fillAmount = 0;
		yield return new WaitForSeconds (waittime);
		txt.text = "GenerATing World ..";
		img.fillAmount = 0.05f;
		yield return new WaitForSeconds (waittime);
		txt.text = "GenerATing World ...";
		img.fillAmount = 0.1f;
	//	waittime = 0.3f;
		yield return new WaitForSeconds (waittime);
		txt.text = "LoAd Texture PAck  .";
		img.fillAmount = 0.13f;
		yield return new WaitForSeconds (waittime);
		txt.text = "LoAd Texture PAck  ..";
		img.fillAmount = 0.16f;
		yield return new WaitForSeconds (waittime);
		txt.text = "LoAd Texture PAck  ...";
		img.fillAmount = 0.18f;
		yield return new WaitForSeconds (waittime);
		txt.text = "LoAd Assets PAck 1";
		img.fillAmount = 0.22f;
		yield return new WaitForSeconds (waittime);
		txt.text = "LoAd Assets PAck 2";
		img.fillAmount = 0.25f;
	//	 waittime = 0.8f;
		yield return new WaitForSeconds (waittime);
		txt.text = "LoAd Assets PAck 3";
		img.fillAmount = 0.32f;
	//	waittime = 0.4f;
		yield return new WaitForSeconds (waittime);
		txt.text = "LoAd Assets PAck 4";
		img.fillAmount = 0.38f;
		yield return new WaitForSeconds (waittime);
		txt.text = "GenerATing Dungeons .";
		img.fillAmount = 0.42f;
		yield return new WaitForSeconds (waittime);
		txt.text = "GenerATing Dungeons ..";
		img.fillAmount = 0.45f;
		yield return new WaitForSeconds (waittime);
		txt.text = "GenerATing Dungeons ...";

		vungle.Show ();

		img.fillAmount = 0.5f;
	//	waittime = 0.7f;
		yield return new WaitForSeconds (waittime);
		txt.text = "LoAD AI .";
		img.fillAmount = 0.55f;
		yield return new WaitForSeconds (waittime);
		txt.text = "LoAD AI ..";
		img.fillAmount = 0.60f;
		yield return new WaitForSeconds (waittime);
		txt.text = "LoAD AI ...";
		img.fillAmount = 0.65f;
		yield return new WaitForSeconds (waittime);
		txt.text = "LoAD CrAft .";
		img.fillAmount = 0.70f;
		yield return new WaitForSeconds (waittime);
		txt.text = "LoAD CrAft ..";
		img.fillAmount = 0.75f;
		yield return new WaitForSeconds (waittime);
		txt.text = "LoAD CrAft ...";
		img.fillAmount = 0.80f;
		yield return new WaitForSeconds (waittime);

		if (Server.IsCrach==1 && PlayerPrefs.GetInt("a")==0) 
		{
			PlayerPrefs.SetInt ("a", 1);
			error.SetActive (true);
		}
			else
		SceneManager.LoadScene ("Main 1");
		

	}

}
