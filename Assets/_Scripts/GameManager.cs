using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager {

    private static GameManager _instance;
    
    public enum GameState {MENU, PLAY, ENDGAME};
    public GameState _gameState;

    public int _startTime { get; private set; }
    public int _objective;
    public int _runningTime;

    public delegate void ChangeStateDelegate();
    public static ChangeStateDelegate changeStateDelegate;
    

    private GameManager() {
        _gameState = GameState.MENU;
        _startTime = 120;
        _objective = 0;
    }

    public static GameManager GetInstance() {
        if (_instance == null) {
            _instance = new GameManager();
        }
        return _instance;
    }

    public void Reset() {
        _objective = 0;
        _runningTime = 0;
    }
}
