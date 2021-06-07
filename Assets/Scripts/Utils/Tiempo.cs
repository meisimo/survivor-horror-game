using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tiempo : MonoBehaviour
{
    private int minutos;
    private int segundos;
    private float tiempo;
    private string tiempoTexto;
    public Text reloj;

    public Text puntaje;
    private int contador=0;
    // Start is called before the first frame update
    void Start()
    {
      puntaje.text = "Puntos: " + contador; 
    }

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;
        minutos = Mathf.FloorToInt(tiempo / 60);
        segundos = Mathf.FloorToInt(tiempo - minutos * 60);
        tiempoTexto = string.Format("{0:00}:{1:00}", minutos, segundos);
        reloj.text = tiempoTexto;
    }
}
