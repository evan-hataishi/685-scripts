using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckContact : MonoBehaviour
{

    GameObject speaker;
    ScoreAggregator scoreAggregator;
    private int audienceNumber;
    private float loopTime;

    void Awake()
    {
        speaker = GameObject.Find("speaker");
        scoreAggregator = GameObject.Find("agg").GetComponent<ScoreAggregator>();

        lock (scoreAggregator.myLock)
        {
            audienceNumber = scoreAggregator.numAudience;
            scoreAggregator.numAudience += 1;
        }
        loopTime = scoreAggregator.contactScoreLoopTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Loop());
    }

    // Update is called once per frame
    void Update()
    {

    }

    int ScoreContact()
    {
        // print(gameObject.transform.position);
        // print(speaker.transform.position);
        Vector3 dirFromSpeakerToThis = (gameObject.transform.position - speaker.transform.position).normalized;
        float dotProd = Vector3.Dot(dirFromSpeakerToThis, speaker.transform.forward);

        // print(dotProd);

        if (dotProd > 0.9)
        {
            return 1;
        }

        return 0;
    }

    IEnumerator Loop()
    {
        while (true)
        {
            scoreAggregator.WriteEyeContactScore(audienceNumber, ScoreContact());
            yield return new WaitForSeconds(loopTime);
        }
    }
}
