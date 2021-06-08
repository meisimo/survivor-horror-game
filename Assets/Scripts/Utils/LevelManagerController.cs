using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerController : MonoBehaviour
{
    static LevelManagerController sharedInstance;

    public Timer timer;
    public Punctuation punctuation;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    static public void IncreasePoints(int points)
    {
        sharedInstance.punctuation.IncreasePoints(points);
    }
}
