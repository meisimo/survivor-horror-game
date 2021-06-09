using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Munitions : MonoBehaviour
{
    private Text munitionText;
    private int points;

    public void RefreshMunitionText(int points)
    {
        if (munitionText == null)
            munitionText = GetComponent<Text>();
        Debug.Log("REFRESH MUN " + points);
        munitionText.text = "Munition: " + points;
    }

}
