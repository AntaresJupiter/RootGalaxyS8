using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using ChartboostSDK;
//using UnityEngine.Advertisements;
using System;
using System.Text;



public class SuperConsole : MonoBehaviour 
{

	public GameObject canvas;

	public static List<string> Debugitem=new List<string>();
	public static bool ActiveCmd=false;
	public bool DebugV=true;
	string command="";
	public GUISkin style;
	public GameObject Fps;
	GameObject Fps2;

	// Use this for initialization
	void HandleLog(string popo35,string pipi42,LogType type)
	{

		var anus = pipi42.Split ('/');
		string anus2 = anus [anus.Length - 1].Replace("\n","");


		if(Debugitem.Count>12)
			Debugitem.RemoveAt(0);

		if(type==LogType.Error)
			Debugitem.Add(anus2+" "+  DateTime.Now.ToString(" [HH:mm:ss] ")+ "<color=red>"+popo35+"</color>");
		else if(type==LogType.Warning)
			Debugitem.Add(anus2+" "+  DateTime.Now.ToString(" [HH:mm:ss] ")+ "<color=orange>"+popo35+"</color>");
			else
			Debugitem.Add(anus2+" "+  DateTime.Now.ToString(" [HH:mm:ss] ")+ "<color=green>"+popo35+"</color>");

	}
	bool show;

	void OnGUI()
	{
		
	//	if (show) {
			if (ActiveCmd && DebugV) {
	
				GUI.skin = style;

				string all = "";
				for (int i = 0; i < Debugitem.Count; i++) {
					all += Debugitem [i] + "\n";
				}
				GUI.Box (new Rect (0, 0, Screen.width / 1, 300), "");
				GUI.Label (new Rect (0, 0, Screen.width,300), all);
		
			}
		//}
	}

	
	public void Start()
	{
		//style=new GUIStyle();
		//style.fontStyle=FontStyle.Bold;
		Fps2=(GameObject)Instantiate(Fps,new Vector3(0.5f,0.1f,0),Quaternion.identity);
	
	}
	
	// Update is called once per frame

	void Awake()
	{
		//if(Input.GetKeyDown(KeyCode.A))
			ActiveCmd=!ActiveCmd;
		//else if(Input.touchCount>0)
				ActiveCmd=true;
//		else if(Input.touches[0].tapCount==4)
	//		ActiveCmd=false;

		if(DebugV && ActiveCmd)
			Application.RegisterLogCallback(HandleLog);
	

	}


	void Update () 
	{


		if ( Input.GetKeyDown (KeyCode.B)) 
		{
			Debug.Log("Check");
		}


		

		if(DebugV && ActiveCmd)
		Application.RegisterLogCallback(HandleLog);
	}
}
