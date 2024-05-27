using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public List<GameObject> talkTexts;
    public List<GameObject> infoTexts;
    public List<GameObject> respawnPos;
    public List<GameObject> turnTexts;
    public List<GameObject> infoObj;
    public GameObject talkPanel;
    public GameObject turnPanel;
    public GameObject blackPanel;
    public GameObject SpeedBar;

    int talkIndex = 0;
    int infoIndex = 0;
    bool isStop = false;
    bool isTurn = false;

    // 0: 서론, 1: 액션포텐셜, 2: Axon멘트, 3: Axon, 4: 출발 멘트, 5: 방향 + 위쪽 멘트, 6: 위방향전환
    // 7: 오른쪽 멘트, 8: 우방향전환, 9: 아래 멘트, 10: 아래방향전환, 11: 복잡한 방향 전환 멘트, 12: 복잡한 방향 전환
    // 13. 채널+소듐멘트, 14: 소듐, 15: a키, 16: 소듐 실행, 17: 포타슘멘트, 18: 포타슘 19: s키, 20: 포타슘 실행
    // 21: 속도바, 22: 시냅스 멘트, 23: 시냅스, 24: 마지막 멘트, 25: 시냅스 실행
    int order = 0;

    Coroutine runningCoroutine; // 현재 실행 중인 코루틴을 저장하기 위한 변수

    private void Start()
    {
        runningCoroutine = StartCoroutine(ShowTexts()); // 코루틴 시작 후 참조 저장
        
        foreach(GameObject obj in infoObj)
        {
            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        // Wall Collide
        if (GameManager.instance.player.wall)
        {
            GameManager.instance.player.wall = false;
            GameManager.instance.player.playerSpeed = 0;
            if (order == 6)
            {
                GameManager.instance.player.transform.position = respawnPos[0].transform.position;
            }
            else if (order == 8)
            {
                GameManager.instance.player.transform.position = respawnPos[1].transform.position;
            }
            else if (order == 10)
            {
                GameManager.instance.player.transform.position = respawnPos[2].transform.position;
            }
            else if (order == 12)
            {
                GameManager.instance.player.transform.position = respawnPos[3].transform.position;
            }
            if (runningCoroutine != null)
            {
                StopCoroutine(runningCoroutine);
            }
            runningCoroutine = StartCoroutine(TurnMistakeCoroutine());
        }

        // Finish
        if (GameManager.instance.player.finish)
        {
            if (order == 12)
            {
                isTurn = false;
            }

            if (order == 16)
            {
                SpeedBar.SetActive(true);

                if (!GameManager.instance.player.Na)
                {
                    GameManager.instance.player.transform.position = respawnPos[4].transform.position;
                    GameManager.instance.player.playerSpeed = 0f;
                    if (runningCoroutine != null)
                    {
                        StopCoroutine(runningCoroutine);
                    }
                    runningCoroutine = StartCoroutine(TurnMistakeCoroutine());
                    GameManager.instance.player.finish = false;
                }
                else
                {
                    order++;
                    if (runningCoroutine != null)
                    {
                        StopCoroutine(runningCoroutine);
                    }
                    isTurn = false;
                    Debug.Log("Order: " + order);
                    GameManager.instance.player.finish = false;
                }
            }
            else if (order == 20)
            {
                if (!GameManager.instance.player.K)
                {
                    GameManager.instance.player.transform.position = respawnPos[5].transform.position;
                    GameManager.instance.player.playerSpeed = 0f;
                    if (runningCoroutine != null)
                    {
                        StopCoroutine(runningCoroutine);
                    }
                    runningCoroutine = StartCoroutine(TurnMistakeCoroutine());
                    GameManager.instance.player.finish = false;
                }
                else
                {
                    order++;
                    if (runningCoroutine != null)
                    {
                        StopCoroutine(runningCoroutine);
                    }
                    isTurn = false;
                    Debug.Log("Order: " + order);
                    GameManager.instance.player.finish = false;
                }
            }
            else
            {
                order++;
                Debug.Log("Order: " + order);
                GameManager.instance.player.finish = false;
                if (runningCoroutine != null)
                {
                    StopCoroutine(runningCoroutine);
                    runningCoroutine = null;
                }
            }
        }

        // infoPanel
        if (isStop && !isTurn && runningCoroutine == null)
        {
            StopCoroutine(ShowTexts());
            GameManager.instance.player.playerSpeed = 0;
            runningCoroutine = StartCoroutine(IntroduceObjects());
        }
        // textPanel
        else if (!isStop && !isTurn && runningCoroutine == null)
        {
            StopCoroutine(IntroduceObjects());
            if (order > 3)
            {
                GameManager.instance.player.playerSpeed = 5f;
            }
            if (order == 15 || order == 19 || order == 24)
            {
                GameManager.instance.player.playerSpeed = 0;
            }
            runningCoroutine = StartCoroutine(ShowTexts());
        }
        else if (!isStop && isTurn && runningCoroutine == null)
        {
            if (order == 6)
            {
                StopCoroutine(ShowTexts());
                runningCoroutine = StartCoroutine(TurnPanel(0));
            }
            else if (order == 8)
            {
                StopCoroutine(ShowTexts());
                runningCoroutine = StartCoroutine(TurnPanel(1));
            }
            else if (order == 10)
            {
                StopCoroutine(ShowTexts());
                runningCoroutine = StartCoroutine(TurnPanel(2));
            }
            else if (order == 16 || order == 20 || order == 25)
            {
                GameManager.instance.player.playerSpeed = 5f;
            }
        }
    }

    IEnumerator TurnPanel(int i)
    {
        turnPanel.SetActive(true);
        turnTexts[i].SetActive(true);
        yield return new WaitForSeconds(1.5f);
        turnPanel.SetActive(false);
        turnTexts[i].SetActive(false);
        isTurn = false;
        yield return null;
    }

    IEnumerator TurnMistakeCoroutine()
    {
        turnPanel.SetActive(true);
        turnTexts[3].SetActive(true);
        yield return new WaitForSeconds(1.5f);
        turnPanel.SetActive(false);
        turnTexts[3].SetActive(false);
        GameManager.instance.player.playerSpeed = 5f;
    }

    IEnumerator ShowTexts()
    {
        if (order == 0)
        {
            GameManager.instance.player.playerSpeed = 0;
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
                isTurn = true;
                yield return null;
            }
        }
        else if (order == 7 || order == 9)
        {
            talkPanel.SetActive(true);
            talkTexts[9].SetActive(true);
            yield return new WaitForSeconds(2f);
            talkTexts[9].SetActive(false);
            talkTexts[talkIndex].SetActive(true);
            yield return new WaitForSeconds(3f);
            talkTexts[talkIndex].SetActive(false);
            talkIndex++;
            isTurn = true;
            yield return null;
        }
        else if (order == 11)
        {
            talkIndex = 10;
            talkPanel.SetActive(true);
            talkTexts[9].SetActive(true);
            yield return new WaitForSeconds(2f);
            talkTexts[9].SetActive(false);
            talkTexts[talkIndex].SetActive(true);
            yield return new WaitForSeconds(3f);
            talkPanel.SetActive(false);
            talkTexts[talkIndex].SetActive(false);
            talkIndex++;
            isTurn = true;
        }
        else if (order == 13)
        {
            talkPanel.SetActive(true);
            talkTexts[9].SetActive(true);
            yield return new WaitForSeconds(2f);
            talkTexts[9].SetActive(false);
            while (talkIndex < 13)
            {
                talkTexts[talkIndex].SetActive(true);
                yield return new WaitForSeconds(3f);
                talkTexts[talkIndex].SetActive(false);
                talkIndex++;
            }
            isStop = true;
        }
        else if (order == 15 || order == 19)
        {
            talkPanel.SetActive(true);
            talkTexts[talkIndex].SetActive(true);
            yield return new WaitForSeconds(3f);
            talkTexts[talkIndex].SetActive(false);
            talkIndex++;
            isTurn = true;
        }
        else if (order == 17)
        {
            talkPanel.SetActive(true);
            talkTexts[9].SetActive(true);
            yield return new WaitForSeconds(2f);
            talkTexts[9].SetActive(false);
            talkTexts[talkIndex].SetActive(true);
            yield return new WaitForSeconds(3f);
            talkTexts[talkIndex].SetActive(false);
            talkIndex++;
            isStop = true;

        }
        else if (order == 21)
        {
            SpeedBar.SetActive(true);
            talkPanel.SetActive(true);
            while (talkIndex < 18)
            {
                talkTexts[talkIndex].SetActive(true);
                yield return new WaitForSeconds(3f);
                talkTexts[talkIndex].SetActive(false);
                talkIndex++;
            }
        }
        else if (order == 22)
        {
            talkPanel.SetActive (true);
            talkTexts[talkIndex].SetActive(true);
            yield return new WaitForSeconds(3f);
            talkTexts[talkIndex].SetActive(false);
            talkIndex++;
            isStop = true;
        }
        else if (order == 24)
        {
            talkPanel.SetActive(true);
            while (talkIndex < 22)
            {
                talkTexts[talkIndex].SetActive(true);
                yield return new WaitForSeconds(3f);
                talkTexts[talkIndex].SetActive(false);
                talkIndex++;
            }
            isTurn = true;
        }
        talkPanel.SetActive(false);
        order++;
        Debug.Log("Order: " + order);
        runningCoroutine = null;
    }

    IEnumerator IntroduceObjects()
    {
        isStop = false;
        blackPanel.SetActive(true);
        infoTexts[infoIndex].SetActive(true);
        /*
        SpriteRenderer spriteRenderer = infoObj[infoIndex].GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = 5;
        }
        else
        {
            Debug.LogError(infoObj[infoIndex].name + "에서 SpriteRenderer를 찾을 수 없습니다. 이 오브젝트는 SpriteRenderer가 있어야 합니다.");
        }*/

        yield return new WaitForSeconds(5f);
        blackPanel.SetActive(false);
        infoTexts[infoIndex].SetActive(false);
        //spriteRenderer.sortingOrder = 3;
        infoIndex++;
        order++;
        runningCoroutine = null;
        yield return null;
    }
}