using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LOADING : MonoBehaviour {


	public Text txt;
void Start () 
	{
		StartCoroutine (bb ());

	}

	IEnumerator bb()
	{
		txt.text = "LOADING";
		yield return new WaitForSeconds (0.16f);
		txt.text = "LOADING .";
		yield return new WaitForSeconds (0.16f);
		txt.text = "LOADING ..";
		yield return new WaitForSeconds (0.16f);
		txt.text = "LOADING ...";
		yield return new WaitForSeconds (0.16f);
		StartCoroutine (bb ());
	}

}
