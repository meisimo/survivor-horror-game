using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePoints : MonoBehaviour
{
    private Text lifeText;
    private int points;

    private void Awake()
    {
        if (lifeText == null)
            lifeText = GetComponent<Text>();
    }

    private void RefreshLifeText()
    {
        lifeText.text = "Salud: " + this.points;
    }

    public void InitPoints(int points)
    {
        lifeText = GetComponent<Text>();
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
