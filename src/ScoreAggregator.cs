using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAggregator : MonoBehaviour
{
    public int numAudience = 0;
    public readonly Object myLock = new Object();
    private List<int>[] eyeContactScores;
    public float contactScoreLoopTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        // numAudience = GameObject.FindGameObjectsWithTag("audience").Length;
        print(numAudience);

        eyeContactScores = new List<int>[numAudience];

        // Initialize array of scores
        for (int i = 0; i < numAudience; ++i)
        {
            eyeContactScores[i] = new List<int>();
        }

        StartCoroutine("TestAverage");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {

    }

    public void WriteEyeContactScore(int audinceID, int score)
    {
        lock(myLock)
        {
            eyeContactScores[audinceID].Add(score);
        }
    }

    IEnumerator TestAverage()
    {
        while (true)
        {
            yield return new WaitForSeconds(contactScoreLoopTime);
            print(GetRecentEyeScores());
        }
    }

    float GetRecentEyeScores()
    {
        float total = 0;
        float count = 0;
        for (int i = 0; i < eyeContactScores.Length; ++i)
        {
            if (eyeContactScores[i].Count > 0)
            {
                total += (float) eyeContactScores[i][eyeContactScores[i].Count - 1];
                count += 1;
            }
        }

        if (count > 0)
        {
            return total / count;
        }
        return 0;
    }
}
