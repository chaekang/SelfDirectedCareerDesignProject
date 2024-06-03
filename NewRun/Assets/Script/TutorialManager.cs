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
    public GameObject MainPlayer;
    public GameObject initPlayer;
    public GameObject initPlayerCam;

    int talkIndex = 0;
    int infoIndex = 0;
    bool isStop = false;
    bool isTurn = false;

    // 0: ����, 1: �׼����ټ�, 2: Axon��Ʈ, 3: Axon, 4: ��� ��Ʈ, 5: ���� + ���� ��Ʈ, 6: ��������ȯ
    // 7: ������ ��Ʈ, 8: �������ȯ, 9: �Ʒ� ��Ʈ, 10: �Ʒ�������ȯ, 11: ������ ���� ��ȯ ��Ʈ, 12: ������ ���� ��ȯ
    // 13: ä��+�ҵ��Ʈ, 14: �ҵ�, 15: aŰ, 16: �ҵ� ����, 17: ��Ÿ����Ʈ, 18: ��Ÿ�� 19: sŰ, 20: ��Ÿ�� ����
    // 21: �ҵ㵶 ��Ʈ, 22: �ҵ㵶, 23: dŰ, 24: �ҵ㵶 ����, 25: ��Ÿ�� �� ��Ʈ, 26: ��Ÿ����, 27: fŰ, 28: ��Ÿ�� �� ����
    // 29: ���+�ӵ���, 30: �ó��� ��Ʈ, 31: �ó���, 32: ������ ��Ʈ, 33: �ó��� ����
    int order = 0;

    Coroutine runningCoroutine; // ���� ���� ���� �ڷ�ƾ�� �����ϱ� ���� ����

    private void Start()
    {
        runningCoroutine = StartCoroutine(ShowTexts()); // �ڷ�ƾ ���� �� ���� ����

        foreach (GameObject obj in infoObj)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
        }

        MainPlayer.SetActive(false);
        initPlayer.SetActive(true);
        initPlayerCam.SetActive(true);
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
            else if (order == 24)
            {
                if (!GameManager.instance.player.NaPoison)
                {
                    GameManager.instance.player.transform.position = respawnPos[6].transform.position;
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
            else if (order == 28)
            {
                if (!GameManager.instance.player.KPoison)
                {
                    GameManager.instance.player.transform.position = respawnPos[7].transform.position;
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
            if (order == 15 || order == 19 || order == 23 || order == 27 || order == 32)
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
            else if (order == 16 || order == 20 || order == 24 || order == 28 || order == 33)
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
        runningCoroutine = null;
    }

    IEnumerator ShowTexts()
    {
        if (order == 0)
        {
            GameManager.instance.player.playerSpeed = 0;
            while (talkIndex < 5)
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
            initPlayer.SetActive(false);
            initPlayerCam.SetActive(false);
            MainPlayer.SetActive(true);
            talkTexts[talkIndex].SetActive(true);
            yield return new WaitForSeconds(2f);
            talkTexts[talkIndex].SetActive(false);
            talkIndex++;
            yield return null;
        }
        else if (order == 5)
        {
            while (talkIndex < 9)
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
            talkTexts[11].SetActive(true);
            yield return new WaitForSeconds(2f);
            talkTexts[11].SetActive(false);
            talkTexts[talkIndex].SetActive(true);
            yield return new WaitForSeconds(3f);
            talkTexts[talkIndex].SetActive(false);
            talkIndex++;
            isTurn = true;
            yield return null;
        }
        else if (order == 11)
        {
            talkIndex = 12;
            talkPanel.SetActive(true);
            talkTexts[11].SetActive(true);
            yield return new WaitForSeconds(2f);
            talkTexts[11].SetActive(false);
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
            talkTexts[11].SetActive(true);
            yield return new WaitForSeconds(2f);
            talkTexts[11].SetActive(false);
            while (talkIndex < 15)
            {
                talkTexts[talkIndex].SetActive(true);
                yield return new WaitForSeconds(3f);
                talkTexts[talkIndex].SetActive(false);
                talkIndex++;
            }
            isStop = true;
        }
        else if (order == 15 || order == 19 || order == 23 || order == 27)
        {
            talkPanel.SetActive(true);
            talkTexts[talkIndex].SetActive(true);
            yield return new WaitForSeconds(3f);
            talkTexts[talkIndex].SetActive(false);
            talkIndex++;
            isTurn = true;
        }
        else if (order == 17 || order == 21 || order == 25)
        {
            talkPanel.SetActive(true);
            talkTexts[11].SetActive(true);
            yield return new WaitForSeconds(2f);
            talkTexts[11].SetActive(false);
            talkTexts[talkIndex].SetActive(true);
            yield return new WaitForSeconds(3f);
            talkTexts[talkIndex].SetActive(false);
            talkIndex++;
            isStop = true;
        }
        else if (order == 29)
        {
            talkPanel.SetActive(true);
            talkTexts[11].SetActive(true);
            yield return new WaitForSeconds(2f);
            talkTexts[11].SetActive(false);
            talkTexts[talkIndex].SetActive(true);
            yield return new WaitForSeconds(3f);
            talkTexts[talkIndex].SetActive(false);
            talkIndex++;
            SpeedBar.SetActive(true);
            while (talkIndex < 25)
            {
                talkTexts[talkIndex].SetActive(true);
                yield return new WaitForSeconds(3f);
                talkTexts[talkIndex].SetActive(false);
                talkIndex++;
            }
            SpeedBar.SetActive(false);
        }
        else if (order == 30)
        {
            GameManager.instance.player.onSynapse = false;
            talkPanel.SetActive(true);
            talkTexts[talkIndex].SetActive(true);
            yield return new WaitForSeconds(3f);
            talkTexts[talkIndex].SetActive(false);
            talkIndex++;
            isStop = true;
        }
        else if (order == 32)
        {
            talkPanel.SetActive(true);
            while (talkIndex < 31)
            {
                talkTexts[talkIndex].SetActive(true);
                yield return new WaitForSeconds(3f);
                talkTexts[talkIndex].SetActive(false);
                talkIndex++;
            }
            isTurn = true;
            GameManager.instance.player.onSynapse = true;
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

        Renderer renderer = infoObj[infoIndex].GetComponent<Renderer>();
        if (renderer != null)
        {
            if (infoIndex == 6)
            {
                Renderer renderer1 = infoObj[infoIndex + 1].GetComponent<Renderer>();
                Renderer renderer2 = infoObj[infoIndex + 2].GetComponent<Renderer>();
                renderer.sortingOrder = 5;
                renderer1.sortingOrder = 5;
                renderer2.sortingOrder = 5;
            }
            else
                renderer.sortingOrder = 5;
        }
        else
        {
            Debug.LogError(infoObj[infoIndex].name + "���� Renderer�� ã�� �� �����ϴ�. �� ������Ʈ�� Renderer�� �־�� �մϴ�.");
        }

        yield return new WaitForSeconds(5f);
        blackPanel.SetActive(false);
        infoTexts[infoIndex].SetActive(false);
        if (renderer != null && infoIndex != 1)
        {
            renderer.sortingOrder = 3;
        }
        else if (renderer != null && infoIndex == 1)
        {
            renderer.sortingOrder = 1;
        }
        infoIndex++;
        order++;
        runningCoroutine = null;
        yield return null;
    }
}
