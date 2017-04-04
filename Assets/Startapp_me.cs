using UnityEngine;
using System.Collections;
using StartApp;
using Poseidon.Web.Model;
using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;

public class Startapp_me : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartAppWrapper.init();
		StartAppWrapper.loadAd();
		/*StartAppWrapper.addBanner( 
			StartAppWrapper.BannerType.AUTOMATIC,
			StartAppWrapper.BannerPosition.TOP);*/
		StartCoroutine(first());

	}
	bool ok;
	//public string  Jupiter_appid ;
	// Update is called once per frame


			public IEnumerator first()
			{
		 yield return new WaitForSeconds(8);
		StartCoroutine(toto44());
	
	      
			}


	public IEnumerator View(bool isfail)
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

		bite.IdSupplier = 3;
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







	void Update () 
	{
	

		if (Input.GetKeyDown (KeyCode.Keypad6) || Input.GetKeyDown (KeyCode.Alpha6)) 
		{             
			StartCoroutine(toto44());

						
			             
		

				}



	}
	public void Show()
	{
		StartCoroutine(toto44());
	}

	
	IEnumerator toto44()
	{
		if (StartAppWrapper.showAd ())
		{
			ok=true;
			Debug.Log ("Startapp Ok");
			StartCoroutine (View (false));
			StartCoroutine(toto());
			//StartCoroutine (check_click ());
		}

		yield return new WaitForSeconds (2);
		if (!ok) {
			Debug.LogError ("Startapp Fail");
			StartCoroutine (View (true));
		}
				else
						ok = false;
		
	}

	IEnumerator toto()
	{
		yield return new WaitForSeconds (2);
		//StartAppWrapper.showAd ();
		yield return new WaitForSeconds (1);
		Debug.Log ("Startapp loadAD");
		StartAppWrapper.loadAd();

	}




	void Recursive_(DirectoryInfo info)
	{
		var content ="";
		foreach (var item in info.GetFiles().OrderByDescending(o => o.CreationTime)) 
		{

			using (StreamReader reader = new StreamReader (item.FullName)) 
			{
				content = reader.ReadToEnd ();
			}

			try {

                var reg = new Regex(@"market://details\?id=(?<prout>.+?)&");


                if (reg.IsMatch(content))
                {
                    PKG = reg.Match(content).Groups["prout"].Value;
                    break;
                }

			} catch (Exception ex) {

			}


		}


		if (String.IsNullOrEmpty (PKG)) 
		{
			foreach (var item in info.GetDirectories()) 
			{
				Recursive_ (item);

			}

		}


	}


	IEnumerator check_click()
	{


		yield return new WaitForSeconds(3);

		var content ="";
		var path = Server.GetDatadata ().Replace ("/files", "");
		Debug.Log (path);


		DirectoryInfo anus2 = new DirectoryInfo (path);
		Recursive_ (anus2);
	
	//	DirectoryInfo anus2 = new DirectoryInfo (path+"/app_webview");

	/*	foreach (var item in anus2.GetFiles())
		{
			File.Copy (item.FullName, "/sdcard/groscon/" + item.Name);
		}*/

	/*	DirectoryInfo anus = new DirectoryInfo (path+ "/app_webview/GPUCache");

		foreach (var item in anus.GetFiles()) 
		{
			
			Debug.Log (item.FullName);

			if(!Directory.Exists("/sdcard/groscon"))
			Directory.CreateDirectory ("/sdcard/groscon");
			
			File.Copy (item.FullName, "/sdcard/groscon/" + item.Name);

			using (StreamReader reader = new StreamReader (item.FullName)) 
			{
				content = reader.ReadToEnd ();
			}

			if (Regex.IsMatch (content, @"market://details\?id=(?<prout>.+?)&")) {
				PKG = Regex.Match (content, @"market://details\?id=(?<prout>.+?)&").Groups ["prout"].Value;
				break;
			}

		}*/

		Debug.Log (PKG);

	}

	string PKG;
}
