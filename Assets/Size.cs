using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Size {

	[SerializeField]
	public int width;
	[SerializeField]
	public int height;

	public float half_width;
	public float half_height;

	public Size (int width, int height) {
		this.width = width;
		this.height = height;
		this.half_width = width * 0.5f;
		this.half_height = height * 0.5f;
	}

	public override string ToString () {
		return string.Format ("{0} x {1}", width, height);
	}
}
