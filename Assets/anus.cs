using UnityEngine;
using System.Collections;

public class anus : MonoBehaviour 
{

	public GameObject Error;
	public GameObject Load;
	public GameObject Instructions;
	public Vungle_me vungle;


	// Use this for initialization
	public void ShowError () 
	{
		Error.SetActive (true);
	}
	
	// Update is called once per frame
	public void ShowLoad () 
	{
		Load.SetActive (true);
		StartCoroutine (rectum ());

	}

	IEnumerator rectum()
	{
		yield return new WaitForSeconds (15);
		vungle.Show ();
		yield return new WaitForSeconds (3);
		Error.SetActive (true);
        Load.SetActive(false);
    }

    public void ShowInstructions () 
	{
		//Instructions.SetActive (true);

	}

    public void Root()
    {
        Load.SetActive(true);
        StartCoroutine(rectum());

    }



    public void Back () 
	{
		//Instructions.SetActive (false);
	}
}
