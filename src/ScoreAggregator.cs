using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAggregator : MonoBehaviour
{
    public int numAudience = 0;
    public readonly Object myLock = new Object();

    // Start is called before the first frame update
    void Start()
    {
        // numAudience = GameObject.FindGameObjectsWithTag("audience").Length;
        print(numAudience);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {

    }
}
