using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynapseBar : MonoBehaviour
{
    public Slider passBar;
    public GameObject NT;

    public float maxPoint = 100;
    public float curPoint = 0;

    public float decreaseRate = 0.1f;

    public GameObject velocityBar;

    public bool GameOver = false;
    float time = 0;

    private void Start()
    {
        passBar.value = 0;
    }

    private void Update()
    {
        if (GameManager.instance.player.onSynapse)
        {
            time += Time.deltaTime;
            if (!GameManager.instance.changeScene.isTutorial)
            {
                velocityBar.SetActive(false);
            }
            curPoint -= decreaseRate * Time.deltaTime;

            // NT 이미지 활성화
            NT.SetActive(true);

            if(curPoint < 100 && time >= 20f)
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
            NT.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
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

        HandlePoint();
    }

    private void HandlePoint()
    {
        passBar.value = (float)curPoint / (float)maxPoint;
    }
}
