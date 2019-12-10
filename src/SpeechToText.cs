using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;


public class SpeechToText : MonoBehaviour
{
    [SerializeField]
    private Text m_Hypotheses;

    [SerializeField]
    private Text m_Recognitions;

    private string currText;

    private DictationRecognizer m_DictationRecognizer;

    private readonly Object myLock = new Object();

    void Start()
    {
        m_DictationRecognizer = new DictationRecognizer();
        m_DictationRecognizer.AutoSilenceTimeoutSeconds = 100;
        m_DictationRecognizer.InitialSilenceTimeoutSeconds = 100;

        //m_Hypotheses = new Text();
        //m_Recognitions = new Text();
        currText = "";

        m_DictationRecognizer.DictationResult += (text, confidence) =>
        {
            //Debug.LogFormat("Dictation result: {0}", text);
            // m_Recognitions.text += text + "\n";
            lock(myLock)
            {
                currText += text + "\n";
            }
        };

        m_DictationRecognizer.DictationHypothesis += (text) =>
        {
            //Debug.LogFormat("Dictation hypothesis: {0}", text);
            // m_Hypotheses.text += text;
        };

        m_DictationRecognizer.DictationComplete += (completionCause) =>
        {
            if (completionCause != DictationCompletionCause.Complete)
                Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
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
            yield return new WaitForSeconds(5.0f);
            lock(myLock)
            {
                string[] splitText = currText.Split(' ');
                print(splitText.Length);
                print(currText);
                currText = "";
            }
        }
    }

    void Update()
    {
        // m_DictationRecognizer.Start();
    }string

    void getMaxSizeClip()
}
