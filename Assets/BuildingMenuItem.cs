using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenuItem : MonoBehaviour {

	private BuildingData building;

	public void SetData (BuildingData building) {
		this.building = building;
		GetComponent<Image> ().sprite = building.sprite;
		transform.GetChild(0).GetComponent<Text> ().text = building.size.ToString ();
	}

	public void OnClick () {
		var prefab = Resources.Load(Prefab.Tile_Building) as GameObject;
		var clone = Instantiate (prefab);
		clone.GetComponent<Building> ().SetBuilding (building);
		TouchController.SetBuilding (clone.GetComponent<Building> ());
	}
}