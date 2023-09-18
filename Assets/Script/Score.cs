using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Score : MonoBehaviour
{

    public static Score instance;
    private int score;
    Player player;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        Player.instance.OnScoreChanged += ModifyScore;
    }

    private void OnDisable()
    {
        Player.instance.OnScoreChanged -= ModifyScore;
    }

    public int GetScore() { return score; }

    public void ModifyScore(int value) { score += value; }


}
