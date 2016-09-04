using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Int2 {

	public int x, y;

	public Int2 (int x, int y) {
		this.x = x;
		this.y = y;
	}

	public override string ToString () {
		return string.Format ("x: {0} y: {1}", x, y);
	}

	public static bool operator == (Int2 i1, Int2 i2) {
		return i1.x == i2.x && i1.y == i2.y;
	}

	public static bool operator != (Int2 i1, Int2 i2) {
		return i1.x != i2.x || i1.y != i2.y;
	}
}