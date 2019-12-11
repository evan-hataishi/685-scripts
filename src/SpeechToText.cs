using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using UnityEngine.Networking;
using static System.String;

public class SpeechToText : MonoBehaviour
{
    private float loopTime = 5.0f;

    private string resultText;

    private string hypothesisText;

    private DictationRecognizer m_DictationRecognizer;

    private readonly UnityEngine.Object myLock = new Object();

    private string scoreAddr = "localhost:8080/api/score";

    void Start()
    {
        m_DictationRecognizer = new DictationRecognizer();
        m_DictationRecognizer.InitialSilenceTimeoutSeconds = 600;
        m_DictationRecognizer.AutoSilenceTimeoutSeconds = 600;

        //m_Hypotheses = new Text();
        //m_Recognitions = new Text();
        resultText = "";
        hypothesisText = "";

        m_DictationRecognizer.DictationResult += (text, confidence) =>
        {
            //Debug.LogFormat("Dictation result: {0}", text);
            // m_Recognitions.text += text + "\n";
            //print("found a result");
            lock (myLock)
            {
                resultText += text + " ";
            }
        };

        m_DictationRecognizer.DictationHypothesis += (text) =>
        {
            //Debug.LogFormat("Dictation hypothesis: {0}", text);
            // m_Hypotheses.text += text;
            print("here");
            lock (myLock)
            {
                hypothesisText = text;
            }
        };

        m_DictationRecognizer.DictationComplete += (completionCause) =>
        {
            //print("in complete");
            /**
            if (completionCause != DictationCompletionCause.Complete)
                print(completionCause);
            */
            //Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
            m_DictationRecognizer.Start();

        };

        m_DictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        };

        m_DictationRecognizer.Start();

        StartCoroutine("CutClips");
    }

    IEnumerator CutClips()
    {
        while (true)
        {
            yield return new WaitForSeconds(loopTime);
            lock (myLock)
            {
                if (resultText != "")
                {
                    print(resultText);
                }
                else
                {
                    print(hypothesisText);
                }
                resultText = "";
                hypothesisText = "";
            }
            StartCoroutine(CalculateSpeechScore(hypothesisText));
        }
    }

    void Update()
    {
        // m_DictationRecognizer.Start();
    }

    // Make sure text is not null
    IEnumerator CalculateSpeechScore(string text)
    {
        float score = 0.0f;
        string uri = scoreAddr + ToQueryParameter(text);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            //string[] pages = uri.Split('/');
            //int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                //Debug.Log(pages[page] + ": Error: " + webRequest.error);
                print("Got error");
            }
            else
            {
                //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                try
                {
                    score = float.Parse(webRequest.downloadHandler.text);
                }
                catch
                {
                    print("Parse float exception");
                }
            }
            ProcessSpeechScore(score);
        }
    }

    string ToQueryParameter(string text)
    {
        string[] split = text.Split(' ');
        string joined = Join("_", split);
        return "?phrase=" + joined;
    }

    void ProcessSpeechScore(float score)
    {
        print(score);
    }
}
