using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class CreateBuildingSetsObject {
	[MenuItem("Assets/Create/My Scriptable Object")]

	public static void Create () {
		BuildingSets asset = ScriptableObject.CreateInstance<BuildingSets>();

		AssetDatabase.CreateAsset(asset, "Assets/NewBuildingSets.asset");
		AssetDatabase.SaveAssets();

		EditorUtility.FocusProjectWindow();

		Selection.activeObject = asset;
	}
}
