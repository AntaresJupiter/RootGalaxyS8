using UnityEngine;
using System.Collections;

public class InitLoad : MonoBehaviour {


	public GameObject panel;
	public GameObject load;

	void Start () 
	{
		//panel.SetActive (false);
		//load.SetActive (true);
		//StartCoroutine (wait ());
	}
	

	IEnumerator wait () 
	{
		Debug.Log ("Init wait : 9.5");
		yield return new WaitForSeconds (4.5f);
		panel.SetActive (true);
		Debug.Log ("Init wait : "+Server.initwait);
		yield return new WaitForSeconds (Server.initwait);
		load.SetActive (false);
	//	Destroy (load);

	}
}
