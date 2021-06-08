using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePoints : MonoBehaviour
{
    private Text LifeText;
    private int points;

    private void Awake()
    {
        LifeText = GetComponent<Text>();
    }

    private void RefreshLifeText()
    {
        LifeText.text = "Salud: " + this.points;
    }

    public void InitPoints(int points)
    {
        this.points = points;
        RefreshLifeText();
    }

    public void IncreaseLifePointsText(int points)
    {
        this.points += points;
        RefreshLifeText();
    }

    public void DecreaseLifePointsText(int points)
    {
        this.points -= points;
        RefreshLifeText();
    }
}
