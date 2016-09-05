using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public enum GameStates {
	Initializing,
	Playing,
	Building,
	Paused,
	Scrolling
};

public static class GameStateManager {

	public static GameStates state = GameStates.Initializing;

	public static void SetGameState (GameStates newState) {
		state = newState;
	}
}
