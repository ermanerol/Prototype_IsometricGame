using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildingMenuManager : MonoBehaviour {

	public BuildingSets buildings;

	public GameObject prefabBuildingMenuItem;

	/// <summary>
	/// Clones all buildings on menu scroll
	/// </summary>
	void Awake () {
		for (int i = 0; i < buildings.buildingDatas.Length; i++) {
			var g = Instantiate (prefabBuildingMenuItem) as GameObject;
			g.transform.SetParent (transform);
			g.GetComponent<BuildingMenuItem> ().SetData(buildings.buildingDatas[i]);
		}
	}
}
