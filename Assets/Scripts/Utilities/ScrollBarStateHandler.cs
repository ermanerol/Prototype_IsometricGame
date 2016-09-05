using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Prevents camera to pan while scrolling buildings scroll list
/// </summary>
public class ScrollBarStateHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public void OnPointerDown (PointerEventData data) {
		GameStateManager.SetGameState (GameStates.Scrolling);
	}

	public void OnPointerUp (PointerEventData data) {
		GameStateManager.SetGameState (GameStates.Playing);
	}
}
