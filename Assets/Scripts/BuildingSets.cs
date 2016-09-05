using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingsData")]
public class BuildingSets : ScriptableObject {
	
	public BuildingData[] buildingDatas;

	public BuildingData GetBuildingData (int index) {
		return buildingDatas[index];
	}
}

[System.Serializable]
public class BuildingData {
	[SerializeField]
	public int buildingIndex;

	[SerializeField]
	public Sprite sprite;
	[SerializeField]
	public Size size;

	[SerializeField]
	public Color buildingColor;

	[SerializeField]
	public int maxAmount = 1;

	[System.NonSerialized]
	private int builtAmount = 0;

	public bool canBuild {
		get { return (maxAmount == -1) || builtAmount < maxAmount; }
	}

	public void BuildingBuilt () {
		builtAmount++;
	}

	public void BuildingDestroyed () {
		builtAmount--;
	}
}