using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour {

	public static PopupManager instance { get; private set; }

	void Awake () {
		instance = this;
	}

	/// <summary>
	/// Clone and set popup for the given building. Sets game state to paused
	/// </summary>
	public static void ShowBuildingDetailPopup (Building building, BuildingData buildingData) {
		var popup = Instantiate (Resources.Load(Prefab.Popup_BuildingDetail)) as GameObject;
		var rect = popup.GetComponent<RectTransform>();
		rect.SetParent (instance.transform); //TODO Look for ways to get canvas reference without singleton...
		rect.localPosition = Vector2.zero;
		rect.sizeDelta = Vector2.zero;

		popup.GetComponent<PopupBuildingDetail>().SetPopup (building, buildingData);

		GameStateManager.SetGameState (GameStates.Paused);
	}
}
