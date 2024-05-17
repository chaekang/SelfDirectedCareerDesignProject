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

    private void Start()
    {
        passBar.value = 0;
    }

    private void Update()
    {

        if (GameManager.instance.player.onSynapse)
        {
            velocityBar.SetActive(false);
            curPoint -= decreaseRate * Time.deltaTime;

            // NT 이미지 활성화
            NT.SetActive(true);
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
                Debug.Log("스테이지 클리어!");
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
