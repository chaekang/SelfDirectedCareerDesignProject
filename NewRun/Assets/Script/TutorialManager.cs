using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public List<GameObject> texts;
    public GameObject talkPanel;

    public GameObject blackPanel;

    int currentIndex = 0;
    bool isStop = false;

    private void Start()
    {
        StartCoroutine(ShowTexts());
    }

    IEnumerator ShowTexts()
    {
        while (currentIndex<texts.Count)
        {
            texts[currentIndex].SetActive(true);
            yield return new WaitForSeconds(3f);
            texts[currentIndex].SetActive(false);
            currentIndex++;
            yield return null;
        }
        talkPanel.SetActive(false);
        isStop = true;
    }

    private void Update()
    {
        if (isStop)
        {
            StartCoroutine(IntroduceObjects());
        }
    }

    IEnumerator IntroduceObjects()
    {

        blackPanel.SetActive(true);
        yield return new WaitForSeconds(5f);
        blackPanel.SetActive(false);
        yield return null;

    }
    

}
