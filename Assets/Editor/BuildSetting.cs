using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;

public class BuildSetting : EditorWindow {

    Vungle_me Vungle;
    Server Server;

    [MenuItem("Super Arnaque/Setting")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(BuildSetting));
	}

    string ID_Vungle;

	void OnGUI()
	{
		try {
			
		


		GUILayout.Label ("------------------------------------------------------------------------------------------------------------------------------------");

            if (Vungle == null)
            {
                Vungle = GameObject.Find("Canvas").GetComponent<Vungle_me>();
              //  ID_Vungle = Vungle.Jupiter_Id;

            }

            if(Server==null)
            {
                Server = GameObject.Find("Canvas").GetComponent<Server>();


            }

            Vungle.Jupiter_Id = EditorGUILayout.TextField ("Vungle Id", Vungle.Jupiter_Id);
            if (GUILayout.Button("StartAppId"))
            {
                Process.Start("notepad.exe", "Assets/Resources/StartAppData.txt");

            }
                GUILayout.Label ("------------------------------------------------------------------------------------------------------------------------------------");

		PlayerSettings.applicationIdentifier = EditorGUILayout.TextField ("Package", PlayerSettings.applicationIdentifier);
            Server.Package = PlayerSettings.applicationIdentifier;

        PlayerSettings.companyName = EditorGUILayout.TextField ("Company Name", PlayerSettings.companyName);
		PlayerSettings.productName = EditorGUILayout.TextField ("Name", PlayerSettings.productName);
         PlayerSettings.bundleVersion = EditorGUILayout.TextField("Version", PlayerSettings.bundleVersion);
          PlayerSettings.Android.bundleVersionCode = EditorGUILayout.IntField("bundle Version", PlayerSettings.Android.bundleVersionCode);

            GUILayout.Label("------------------------------------------------------------------------------------------------------------------------------------");
        }
        catch (System.Exception ex)
        {

        }



        /*	GUILayout.Label ("------------------------------------------------------------------------------------------------------------------------------------");
            if (GUILayout.Button ("Build")) 
            {
                if(!Directory.Exists(Application.dataPath+"/Apk_Games"))
                    Directory.CreateDirectory(Application.dataPath+"/Apk_Games");

                string[] levels = new string[] {"Assets/AppodealDemo/AppodealDemo1.unity"};

                BuildPipeline.BuildPlayer(levels, Application.dataPath+"/Apk_Games/"+PlayerSettings.bundleIdentifier+".apk", BuildTarget.Android, BuildOptions.None);

                    EditorUtility.RevealInFinder (Application.dataPath + "/Apk_Games/"+PlayerSettings.bundleIdentifier+".apk");

            }

              */
    }



}
