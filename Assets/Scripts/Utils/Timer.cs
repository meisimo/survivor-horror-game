using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

  private Text reloj;
  private int minutos;
  private int segundos;
  private float tiempo;
  private string tiempoTexto;

  private void Awake()
  {
    reloj = GetComponent<Text>();
  }

  void Update()
  {
    tiempo += Time.deltaTime;
    minutos     = Mathf.FloorToInt(tiempo / 60);
    segundos    = Mathf.FloorToInt(tiempo - minutos * 60);
    tiempoTexto = string.Format("{0:00}:{1:00}", minutos, segundos);
    reloj.text  = tiempoTexto;
  }
}
