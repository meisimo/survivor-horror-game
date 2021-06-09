using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehavior : MonoBehaviour
{
    public int munition = 10;
    public GameObject tapa;

    private bool full;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        full = true;
        source = GetComponent<AudioSource>();
    }

    private void Open()
    {
        tapa.transform.Rotate(0f, 0f, -40f);
    }

    private void OnTriggerEnter(Collider other) {
        if(full && other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<MainCharacterController>().LoadMunition(munition);
            Open();
            full = false;
            source.Play();
        }
    }
}
