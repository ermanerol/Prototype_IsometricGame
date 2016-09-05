using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollBarStateHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public void OnPointerDown (PointerEventData data) {
		GameStateManager.SetGameState (GameStates.Scrolling);
	}

	public void OnPointerUp (PointerEventData data) {
		GameStateManager.SetGameState (GameStates.Playing);
	}
}
