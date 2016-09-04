using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenuItem : MonoBehaviour {

	public void SetDisplay (BuildingData building) {
		GetComponent<Image> ().sprite = building.sprite;
		transform.GetChild(0).GetComponent<Text> ().text = building.size.ToString ();
	}
}