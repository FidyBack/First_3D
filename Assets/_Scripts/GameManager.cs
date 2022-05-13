using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager {

    private static GameManager _instance;
    public int _startTime = 120;
    public int _runningTime = 0;
    private int _objective = 0;
    
    public int StartTime {
        get => _startTime;
        set => _startTime = value;
    }
    public int Objectives {
        get => _objective;
        set => _objective = value;
    }


    private GameManager() {
        _objective = 0;
    }

    public static GameManager GetInstance() {
        if (_instance == null) {
            _instance = new GameManager();
        }
        return _instance;
    }
}
