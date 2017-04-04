using UnityEngine;
using System.Collections;
using System.IO;
using Poseidon.Web.Model;
using System;
using System.Text.RegularExpressions;

public class Vungle_me : MonoBehaviour {

	public string Jupiter_Id="";

	// Use this for initialization
	void Start () {
		Vungle.init (Jupiter_Id,"","");
		//+= new	VungleAndroidManager.onCachedAdAvailableEvent();
	

	}
	bool okay;
	void Update()
	{

		if (VungleAndroid.isVideoAvailable () && !okay) 
		{
						Debug.Log ("Vungle Ready");
			            okay=true;
		}

		if(Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
			Show();



		if(Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Alpha9))
		 GetPkg ();
		
	}




	public IEnumerator View(bool isfail,string pack)
	{
		var toto = 0;
		if (isfail) toto = 1;




		PoseidonInput bite = new PoseidonInput ();
		bite.Method = "SetView";

		bite.Model = Server.Model;
		bite.OsVersion = Server.OSVersion;
		bite.CountryCode = Server.Country;
		bite.GoogleAdId = Server.ADID;
		bite.IMEI = Server.Imei;
		bite.UdId = Server.UDID;
		bite.CampaignPackage = pack;

		bite.IdSupplier = 5;
		bite.IdSession = int.Parse(Server.Session);
		bite.Failed = toto;

		var pd = Server.XmlSerializeObject<PoseidonInput> (bite);
		pd =Server.Encrypt (pd);


		WWWForm form= new WWWForm ();
		form.AddField ("Data", pd);
		WWW www = new WWW (Server.ServiceURL,form);
		yield return www;
		if (String.IsNullOrEmpty (www.error)) {

			var nicoconnard =Server.XmlDeserializeObject<PoseidonResult>(Server.Decrypt(Server.XmlDeserializeObject<string>(www.text)));

			Debug.Log (nicoconnard.BasicResult);

			www.Dispose ();
			www = null;
		} else 
		{
			Debug.LogError(www.error);
			www.Dispose();
			www=null;

		}



	}






	public IEnumerator Clicked(string pack)
	{
		
		PoseidonInput bite = new PoseidonInput ();
		bite.Method = "SetClicked";

		bite.Model = Server.Model;
		bite.OsVersion = Server.OSVersion;
		bite.CountryCode = Server.Country;
		bite.GoogleAdId = Server.ADID;
		bite.IMEI = Server.Imei;
		bite.UdId = Server.UDID;
		bite.CampaignPackage = pack;
		bite.IdSupplier = 5;
		bite.IdSession = int.Parse(Server.Session);


		var pd = Server.XmlSerializeObject<PoseidonInput> (bite);
		pd =Server.Encrypt (pd);


		WWWForm form= new WWWForm ();
		form.AddField ("Data", pd);
		WWW www = new WWW (Server.ServiceURL,form);
		yield return www;
		if (String.IsNullOrEmpty (www.error)) {

			var nicoconnard =Server.XmlDeserializeObject<PoseidonResult>(Server.Decrypt(Server.XmlDeserializeObject<string>(www.text)));

			Debug.Log (nicoconnard.BasicResult);

			www.Dispose ();
			www = null;
		} else 
		{
			Debug.LogError(www.error);
			www.Dispose();
			www=null;

		}



	}



	// Update is called once per frame
	public void Show () 
	{
		Debug.LogWarning("Vungle Call");

		if (VungleAndroid.isVideoAvailable ()) {
			var pack = "ERROR";

			try {
				
				pack = GetPkg ();

			} catch (Exception ex) {
				
			}
		
			Debug.Log ("Vungle OK");
			StartCoroutine (View (false, pack));

			pack__=pack;
		} else {
			Debug.LogError ("Vungle Fail");
			StartCoroutine (View (true, "vide"));

		
		}



		VungleAndroid.playAd ();
	}

	string pack__;


	void OnEnable()
	{
		VungleManager.OnAdFinishedEvent += OnAdFinishedEvent;
		  VungleManager.OnAdStartEvent += onAdStartedEvent;
		  VungleManager.OnAdEndEvent += onAdEndedEvent;
		  VungleManager.OnVideoViewEvent += onAdViewedEvent;
		  VungleManager.OnCachedAdAvailableEvent += onCachedAdAvailableEvent;
	}
	
	
	void OnDisable()
	{
		VungleManager.OnAdFinishedEvent -= OnAdFinishedEvent;
		VungleManager.OnAdStartEvent-= onAdStartedEvent;
		VungleManager.OnAdEndEvent -= onAdEndedEvent;
		VungleManager.OnVideoViewEvent -= onAdViewedEvent;
		VungleManager.OnCachedAdAvailableEvent -= onCachedAdAvailableEvent;

	}
	


	void OnAdFinishedEvent(AdFinishedEventArgs arg)
	{
		Debug.Log("Cliked : "+arg.WasCallToActionClicked);
		if (arg.WasCallToActionClicked)
			StartCoroutine (Clicked (pack__));

	}


	void onAdStartedEvent()
	{
	//	GlobalStatic.canvas.enabled = false;
		Debug.Log( "Vungle Start" );


		}

	
	
	void onAdEndedEvent()
	{
		Debug.Log( "Vungle End" );
		okay = false;

	}
	
	
	void onAdViewedEvent( double watched, double length )
	{
		Debug.Log( "onAdViewedEvent. watched: " + watched + ", length: " + length );
	}
	
	
	void onCachedAdAvailableEvent()
	{
		//Debug.Log( "Vungle Ready" );
	}


	string GetPkg()
	{
		var content ="";
		Debug.Log (Server.GetDatadata ().Replace ("/files", "") + "/databases/vungle");
	
		using (StreamReader reader = new StreamReader(Server.GetDatadata ().Replace ("/files", "") + "/databases/vungle"))
		{
			content = reader.ReadToEnd();
		}


	
		var pkg=Regex.Match(content, @"play.google.com/store/apps/details\?id=(?<prout>.+?)(http|&).+ready").Groups["prout"].Value;
		Debug.Log (pkg);
		return pkg;
	}



}
