using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Punctuation : MonoBehaviour
{
    private Text pointsText;
    private int points;

    private void Awake()
    {
        pointsText = GetComponent<Text>();
        InitPoints();
    }

    private void RefreshPointsText()
    {
        pointsText.text = "Puntos: " + this.points;
    }

    public void InitPoints()
    {
        points = 0;
        RefreshPointsText();
    }

    public void IncreasePoints(int points)
    {
        this.points += points;
        RefreshPointsText();
    }

}
