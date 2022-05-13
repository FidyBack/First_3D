using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIComponents : MonoBehaviour {

    public TextMeshProUGUI objText, timeText;
    private GameManager gm;

    void Start() {
        gm = GameManager.GetInstance();
    }

    void Update() {
        gm._runningTime = (int) (gm._startTime - Time.time);

        objText.text = "Objectives: " + gm.Objectives.ToString() + "/4";
        timeText.text = "Time: " + gm._runningTime.ToString();
    }
}
