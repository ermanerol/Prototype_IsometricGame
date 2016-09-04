using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupBuildingDetail : MonoBehaviour {

	[SerializeField]
	private Image buildingImage;
	[SerializeField]
	private Image colorBox;

	private Building building;

	public void SetPopup (Building building, BuildingData buildingData) {
		this.building = building;
		buildingImage.sprite = buildingData.sprite;
		colorBox.color = buildingData.buildingColor;
	}

	public void DeleteBuilding () {
		building.DestroyBuilding();
		ClosePopup ();
	}

	public void ClosePopup () {
		GameStateManager.SetGameState (GameStates.Playing);
		Destroy (gameObject);
	}
}
