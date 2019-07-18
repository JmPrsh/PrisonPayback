//using UnityEngine;
//using System.Collections;
//using UnityEditor;
//using System.Linq;
//using System.Collections.Generic;
//using Microsoft.Win32;

//public class PPEditor : EditorWindow
//{
//	string m_NewKey = "";
//	FieldType m_FieldType = FieldType.Float;
//	string m_NewvalueCurrent= "";
	
//	enum FieldType
//	{
//		Float,
//		Int,
//		String
//	}
	
//	[MenuItem("Jamie's Plugin/Show PlayerPrefs %_2")]
//	public static void ShowWindow()
//	{
//		EditorWindow.GetWindow(typeof(PPEditor));
//	}
	
//	void OnGUI()
//	{
//		GUILayout.Box("Player prefs key-valueCurrentpairs: ");
		
//		// see unity docks for ref of why this is there 
//		string[] keys = new string[0];
//		try
//		{
//			RegistryKey ppKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey(PlayerSettings.companyName).OpenSubKey(PlayerSettings.productName);
//			keys = ppKey.GetValueNames().Select(x => x.Remove(x.LastIndexOf("_"))).ToArray();
//			GUILayout.BeginVertical(GUI.skin.box);
//			foreach (string key in keys)
//			{
//				DrawKeyEditor(key);
//			}
//			GUILayout.EndVertical();
			
//			GUILayout.Space(25);
//		}
//		catch (System.NullReferenceException e) { }
		
		
//		GUILayout.BeginVertical(GUI.skin.box);
		
//		GUILayout.BeginHorizontal();
//		GUILayout.Label("Key: ", GUILayout.MinWidth(50));
		
//		if (keys.Contains(m_NewKey))
//		{
//			GUI.color = Color.red;
//		}
		
//		m_NewKey = GUILayout.TextField(m_NewKey, GUILayout.MinWidth(50), GUILayout.MinHeight(18));
//		GUI.color = Color.white;
//		GUILayout.Label("Value: ", GUILayout.MinWidth(50));
//		m_NewvalueCurrent= GUILayout.TextField(m_NewValue, GUILayout.MinWidth(50), GUILayout.MinHeight(18));
		
//		GUILayout.FlexibleSpace();
//		List<string> fieldTypes = System.Enum.GetNames(typeof(FieldType)).ToList();
//		m_FieldType = (FieldType)GUILayout.SelectionGrid(fieldTypes.IndexOf("" + m_FieldType), fieldTypes.ToArray(), 3);
//		GUILayout.EndHorizontal();
//		GUILayout.BeginHorizontal();
		
//		GUILayout.FlexibleSpace();
//		if (GUILayout.Button("Add"))
//		{
//			switch (m_FieldType)
//			{
//			case FieldType.Float:
//			{
//				float parse = 0;
//				float.TryParse(m_NewValue, out parse);
//				PlayerPrefs.SetFloat(m_NewKey, parse);
//				break;
//			}
//			case FieldType.Int:
//			{
//				int parse = 0;
//				int.TryParse(m_NewValue, out parse);
//				PlayerPrefs.SetInt(m_NewKey, parse);
//				break;
//			}
//			case FieldType.String:
//			{
//				Debug.Log("settings string");
//				PlayerPrefs.SetString(m_NewKey, m_NewValue);
//				break;
//			}
//			}
//		}
//		GUILayout.FlexibleSpace();
		
//		GUILayout.EndHorizontal();
//		GUILayout.EndVertical();
//		GUILayout.Space(25);
//		if (GUILayout.Button("Delete all"))
//		{
//			PlayerPrefs.DeleteAll();
//		}
//	}
	
//	void DrawKeyEditor(string key)
//	{
//		GUILayout.BeginHorizontal(GUI.skin.box);
//		GUILayout.Label(key);
//		GUILayout.FlexibleSpace();
//		if (PlayerPrefs.GetFloat(key, -1) != -1)
//		{
//			float defaultV = PlayerPrefs.GetFloat(key);
//			GUILayout.Label("float");
//			string edited = GUILayout.TextField("" + defaultV, GUILayout.MinWidth(35), GUILayout.MinHeight(18));
//			float result = 0;
//			if (float.TryParse(edited, out result))
//			{
//				if (defaultV != result)
//					PlayerPrefs.SetFloat(key, result);
//			}
//		}
//		else if (PlayerPrefs.GetString(key, "-1") != "-1")
//		{
//			GUILayout.Label("string");
//			string defaultV = PlayerPrefs.GetString(key);
//			string edited = GUILayout.TextField("" + defaultV, GUILayout.MinWidth(35), GUILayout.MinHeight(18));
//			if (edited != defaultV)
//				PlayerPrefs.SetString(key, edited);
//		}
//		else
//		{
//			GUILayout.Label("int");
//			int defaultV = PlayerPrefs.GetInt(key);
//			string edited = GUILayout.TextField("" + defaultV, GUILayout.MinWidth(35), GUILayout.MinHeight(18));
//			int result = 0;
//			if (int.TryParse(edited, out result))
//			{
//				if (defaultV != result)
//					PlayerPrefs.SetInt(key, result);
//			}
//		}
		
//		if (GUILayout.Button("x"))
//		{
//			PlayerPrefs.DeleteKey(key);
//		}
//		GUILayout.EndHorizontal();
//	}
//}
