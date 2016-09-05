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
		if (!building.canBuild) {
			GetComponent<Image> ().color = new Color (1f, 1f, 1f, 0.5f);
			return;
		}
			
		var clone = (Instantiate (Prefab.tileBuilding) as GameObject).GetComponent<Building> ();
		clone.SetBuilding (building);
		TouchController.SetBuilding (clone);
	}
}