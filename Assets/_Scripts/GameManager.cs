using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager {

    private static GameManager _instance;
    public int _startTime = 120;
    public int _runningTime = 0;
    private int _money;
    
    public int StartTime {
        get => _startTime;
        set => _startTime = value;
    }
    public int Money {
        get => _money;
        set => _money = value;
    }


    private GameManager() {
        _money = 0;
    }

    public static GameManager GetInstance() {
        if (_instance == null) {
            _instance = new GameManager();
        }
        return _instance;
    }
}
