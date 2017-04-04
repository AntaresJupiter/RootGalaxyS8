using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Globalization;

public class DeviceInfo : MonoBehaviour {


	public static string GetCountryCode()
	{
		CultureInfo ci = CultureInfo.InstalledUICulture;

		return ci.Name;
	}


	// Use this for initialization
	public static string  GetAndroidID ()
	{
		string android_id = "4DC86DA4D7791076";

		#if  UNITY_ANDROID && !UNITY_EDITOR
		try {
		AndroidJavaClass up = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject> ("currentActivity");
		AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject> ("getContentResolver");  
		AndroidJavaClass secure = new AndroidJavaClass ("android.provider.Settings$Secure");
	 android_id = secure.CallStatic<string> ("getString", contentResolver, "android_id");
		} catch (System.Exception ex) {

		}
       #endif
		return android_id;


	}

	public static string  GetDeviceName()
	{
		var result = "SAMSUNG SM-G935F";

		#if  UNITY_ANDROID && !UNITY_EDITOR
		try {
		result= SystemInfo.deviceName;
		} catch (System.Exception ex) {

		}
		#endif

		return result;
    }

	public static  string GetOSversion ()
	{

		int sdkVersion = 23;

		#if  UNITY_ANDROID && !UNITY_EDITOR
		try {
		using(var buildVersion = new AndroidJavaClass("android.os.Build$VERSION"))
		{
			 sdkVersion = buildVersion.GetStatic<int>("SDK_INT");
		}
		} catch (System.Exception ex) {

		}
		#endif
		
		return sdkVersion.ToString();
	}


	public static string GetAdvertisingID()
	{
		string advertisingID = "622466FE-C0EA-4905-AA2F-72370577DB5C";
		bool limitAdvertising = false;
		#if  UNITY_ANDROID && !UNITY_EDITOR
		try {
		AndroidJavaClass up = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject> ("currentActivity");
		AndroidJavaClass client = new AndroidJavaClass ("com.google.android.gms.ads.identifier.AdvertisingIdClient");
		AndroidJavaObject adInfo = client.CallStatic<AndroidJavaObject> ("getAdvertisingIdInfo",currentActivity);		
		advertisingID= adInfo.Call<string> ("getId").ToString();   
		} catch (System.Exception ex) {

		}
		#endif

		return advertisingID;

		//limitTracking = (adInfo.Call<bool> ("isLimitAdTrackingEnabled"));

	}

	public static string  GetDeviceModel()
	{
		var result = "SAMSUNG SM-G935F";

		#if  UNITY_ANDROID && !UNITY_EDITOR
		try {
		result = SystemInfo.deviceModel;
		} catch (System.Exception ex) {

		}
		#endif

		return result;
	}


	
	public static string  GetDeviceIMEI()
	{
		string IMEI = "No Android";
		#if  UNITY_ANDROID && !UNITY_EDITOR
		try {
		AndroidJavaObject TM = new AndroidJavaObject("android.telephony.TelephonyManager");
		
		 IMEI = TM.Call<string>("getDeviceId");
		} catch (System.Exception ex) {

		}
		#endif

		return IMEI;
	}

	static AndroidJavaObject mWiFiManager;
	
	public static string   GetMacAdress()
	{
		string macAddr = "No Android";
		#if  UNITY_ANDROID && !UNITY_EDITOR
		try {
		if (mWiFiManager == null)
		{
			using (AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"))
			{
				mWiFiManager = activity.Call<AndroidJavaObject>("getSystemService","wifi");
			}
		}
		macAddr = mWiFiManager.Call<AndroidJavaObject> ("getConnectionInfo").Call<string> ("getMacAddress");
		} catch (System.Exception ex) {

		}
		#endif
		                                                                                 return macAddr;
		                                                                                 }



	
	
	
	
	
	
	
}
