using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Size {

	[SerializeField]
	public int width;
	[SerializeField]
	public int height;


	public Size (int width, int height) {
		this.width = width;
		this.height = height;
	}
}
