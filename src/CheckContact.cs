using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckContact : MonoBehaviour
{

    GameObject speaker;
    ScoreAggregator scoreAggregator;

    void Awake()
    {
        speaker = GameObject.Find("speaker");
        scoreAggregator = GameObject.Find("agg").GetComponent<ScoreAggregator>();
        lock (scoreAggregator.myLock)
        {
            scoreAggregator.numAudience += 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        float score = ScoreContact();

        print(score);
        Loop();
    }

    // Update is called once per frame
    void Update()
    {

    }

    float ScoreContact()
    {
        // print(gameObject.transform.position);
        // print(speaker.transform.position);
        Vector3 dirFromSpeakerToThis = (gameObject.transform.position - speaker.transform.position).normalized;
        float dotProd = Vector3.Dot(dirFromSpeakerToThis, speaker.transform.forward);

        // print(dotProd);

        if (dotProd > 0.9)
        {
            // ObjA is looking mostly towards ObjB
        }

        return dotProd;
    }

    IEnumerator Loop()
    {
        /**
        while (true)
        {
            yield return new WaitForSeconds(1);
            updateUpdateCountPerSecond = updateCount;
            updateFixedUpdateCountPerSecond = fixedUpdateCount;

            updateCount = 0;
            fixedUpdateCount = 0;
        }
    */
        yield return new WaitForSeconds(1);
    }
}
