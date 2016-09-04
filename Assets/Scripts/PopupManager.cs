using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour {

	public static PopupManager instance;

	void Awake () {
		instance = this;
	}

	public static void ShowBuildingDetailPopup (Building building, BuildingData buildingData) {
		var popup = Instantiate (Resources.Load(Prefab.Popup_BuildingDetail)) as GameObject;
		var rect = popup.GetComponent<RectTransform>();
		rect.SetParent (instance.transform);
		rect.localPosition = Vector2.zero;
		rect.sizeDelta = Vector2.zero;

		popup.GetComponent<PopupBuildingDetail>().SetPopup (building, buildingData);

		GameStateManager.SetGameState (GameStates.Paused);
	}
}
