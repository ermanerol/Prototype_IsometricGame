using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMenuManager : MonoBehaviour {

	public BuildingSets buildings;

	public GameObject prefabBuildingMenuItem;

	void Awake () {
		for (int i = 0; i < buildings.buildingSets.Length; i++) {
			var g = Instantiate (prefabBuildingMenuItem) as GameObject;
			g.transform.SetParent (transform);
			g.GetComponent<BuildingMenuItem> ().SetData(buildings.buildingSets[i]);
		}
	}
}
