using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public List<GameObject> talkTexts;
    public List<GameObject> infoTexts;
    public List<GameObject> respawnPos;
    public List<GameObject> turnPanel;
    public GameObject talkPanel;
    public GameObject blackPanel;
    public GameObject infoPanel;

    int talkIndex = 0;
    int infoIndex = 0;
    bool isStop = false;
    // 0: 서론, 1: 액션포텐셜, 2: Axon멘트, 3: Axon, 4: 출발 멘트, 5: 방향 + 위쪽 멘트, 6: 위방향전환
    // 7: 오른쪽 멘트, 8: 우방향전환, 9: 아래 멘트, 10: 아래방향전환
    int order = 0;

    Coroutine runningCoroutine; // 현재 실행 중인 코루틴을 저장하기 위한 변수

    private void Start()
    {
        GameManager.instance.player.playerSpeed = 0;
        runningCoroutine = StartCoroutine(ShowTexts()); // 코루틴 시작 후 참조 저장
    }

    private void Update()
    {
        // infoPanel
        if (isStop && runningCoroutine == null)
        {
            StopCoroutine(ShowTexts());
            GameManager.instance.player.playerSpeed = 0;
            runningCoroutine = StartCoroutine(IntroduceObjects());
        }
        // textPanel
        else if (!isStop && runningCoroutine == null)
        {
            StopCoroutine(IntroduceObjects());
            if (order > 3)
            {
                GameManager.instance.player.playerSpeed = 5f;
            }
            runningCoroutine = StartCoroutine(ShowTexts());
        }

        if (order == 6)
        {
            Debug.Log("6");
            TurnPanel(0);

            if (GameManager.instance.player.wall)
            {
                GameManager.instance.player.wall = false;
                GameManager.instance.player.transform.position = respawnPos[0].transform.position;
                GameManager.instance.player.playerSpeed = 0;
            }
        }
    }

    void TurnPanel(int i)
    {
        float timer = 0;
        turnPanel[i].SetActive(true);
        timer += Time.deltaTime;
        if (timer > 1.5f)        {
            turnPanel[i].SetActive(false);
        }
    }

    IEnumerator ShowTexts()
    {
        if (order == 0)
        {
            while (talkIndex < 3)
            {
                talkPanel.SetActive(true);
                talkTexts[talkIndex].SetActive(true);
                yield return new WaitForSeconds(3f);
                talkTexts[talkIndex].SetActive(false);
                talkIndex++;
                yield return null;
            }
            isStop = true;
        }
        else if (order == 2)
        {
            talkPanel.SetActive(true);
            talkTexts[talkIndex].SetActive(true);
            yield return new WaitForSeconds(3f);
            talkTexts[talkIndex].SetActive(false);
            talkIndex++;
            isStop = true;
            yield return null;
        }
        else if (order == 4)
        {
            talkPanel.SetActive(true);
            talkTexts[talkIndex].SetActive(true);
            yield return new WaitForSeconds(2f);
            talkTexts[talkIndex].SetActive(false);
            talkIndex++;
            yield return null;
        }
        else if (order == 5)
        {
            while (talkIndex < 7)
            {
                talkPanel.SetActive(true);
                talkTexts[talkIndex].SetActive(true);
                yield return new WaitForSeconds(3f);
                talkTexts[talkIndex].SetActive(false);
                talkIndex++;
                yield return null;
            }

        }
        talkPanel.SetActive(false);
        order++;
        runningCoroutine = null; // 코루틴이 종료되면 runningCoroutine을 null로 설정
    }

    IEnumerator IntroduceObjects()
    {
        isStop = false;
        Debug.Log("start");
        infoPanel.SetActive(true);
        blackPanel.SetActive(true);
        infoTexts[infoIndex].SetActive(true);
        yield return new WaitForSeconds(5f);
        blackPanel.SetActive(false);
        infoPanel.SetActive(false);
        infoTexts[infoIndex].SetActive(false);
        infoIndex++;
        order++;
        runningCoroutine = null; // 코루틴이 종료되면 runningCoroutine을 null로 설정
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            if (order == 6)
            {
                Debug.Log("bump");
                GameManager.instance.player.transform.position = respawnPos[0].transform.position;
                GameManager.instance.player.playerSpeed = 0;
                
            }
        }
    }
}
