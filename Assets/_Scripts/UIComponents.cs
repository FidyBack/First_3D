using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIComponents : MonoBehaviour {

    public TextMeshProUGUI moneyText, timeText;
    private GameManager gm;

    void Start() {
        gm = GameManager.GetInstance();
    }

    void Update() {
        gm._runningTime = (int) (gm._startTime - Time.time);

        moneyText.text = "Money: " + gm.Money.ToString();
        timeText.text = "Time: " + gm._runningTime.ToString();    
    }
}
