using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynapseBar : MonoBehaviour
{
    public Slider passBar;
    public GameObject[] NT;

    public float maxPoint = 100;
    public float curPoint = 0;

    public float decreaseRate = 0.1f;

    public GameObject velocityBar;

    public bool GameOver = false;
    float time = 0;

    bool getKeyboard = true;

    private void Start()
    {
        passBar.value = 0;
    }

    private void Update()
    {
        if (GameManager.instance.changeScene.isTutorial)
        {
            if (GameManager.instance.tutorialManager.order == 33)
            {
                getKeyboard = true;
            }
            else
            {
                getKeyboard = false;
            }
        }

        if (!GameManager.instance.changeScene.isTutorial && GameManager.instance.player.onSynapse)
        {
            velocityBar.SetActive(false);

            // NT 이미지 활성화
            for (int i = 0; i < NT.Length; i++)
            {
                NT[i].SetActive(true);
            }
        }

        if (GameManager.instance.spawner.ntFinish || GameManager.instance.player.endStop)
        {
            time += Time.deltaTime;
            curPoint -= decreaseRate * Time.deltaTime;

            if (curPoint < 100 && time >= 20f)
            {
                GameManager.instance.changeScene.DeadPalayer("Syn");
            }
            if(curPoint < -100)
            {
                GameManager.instance.changeScene.DeadPalayer("Syn");
            }
        }

        if(!GameManager.instance.player.onSynapse)
        {
            for (int i = 0; i < NT.Length; i++)
            {
                NT[i].SetActive(false);
            }
            time = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (getKeyboard)
            {
                if (curPoint < maxPoint)
                {
                    curPoint += 5;
                }
                else
                {
                    curPoint = 100;
                    GameManager.instance.player.disappear = true;
                    GameManager.instance.player.onSynapse = false;
                }
            }
        }

        HandlePoint();
    }

    private void HandlePoint()
    {
        passBar.value = (float)curPoint / (float)maxPoint;
    }
}
