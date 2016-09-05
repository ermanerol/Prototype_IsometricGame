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

	/// <summary>
	/// Set building detail to be displayed on popup
	/// </summary>
	public void SetPopup (Building building, BuildingData buildingData) {
		this.building = building;
		buildingImage.sprite = buildingData.sprite;
		colorBox.color = buildingData.buildingColor;
	}

	/// <summary>
	/// Deletes building and closes popup
	/// </summary>
	public void DeleteBuilding () {
		building.DestroySelf();
		ClosePopup ();
	}

	/// <summary>
	/// Closes building detail popup
	/// </summary>
	public void ClosePopup () {
		GameStateManager.SetGameState (GameStates.Playing);
		Destroy (gameObject);
	}
}
