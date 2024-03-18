using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynapseBar : MonoBehaviour
{
    public Slider passBar;

    public Player player;

    public float maxPoint = 100;
    public float curPoint = 0;

    public float decreaseRate = 0.1f;

    private void Start()
    {
        passBar.value = 0;
    }

    private void Update()
    {
        if (player.isSynapse)
        {
            curPoint -= decreaseRate * Time.deltaTime;
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
                player.isSynapse = false;
            }
        }

        HandlePoint();
    }

    private void HandlePoint()
    {
        passBar.value = (float)curPoint / (float)maxPoint;
    }
}
