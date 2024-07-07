

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBar : MonoBehaviour
{
    public float curSpeed;
    public float maxSpeed;
    public float slowSpeedRate;

    public Slider speedBar;

    public float decressSpeed;
    public float increaseSpeed;

    private _Player playerScript;
    private ChangeScene changeScene;


    void Start()
    {
        playerScript = FindObjectOfType<_Player>();
        changeScene = FindObjectOfType<ChangeScene>();

        maxSpeed = playerScript.playerSpeed;
        speedBar.maxValue = maxSpeed;
        SetSpeedBar(maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpeed();

    }

    void SetSpeedBar(float num)
    {
        if (!GameManager.instance.changeScene.isTutorial)
        {
            maxSpeed = num;
            curSpeed = maxSpeed;
        }
    }

    void CheckSpeed()
    {
        if (!GameManager.instance.changeScene.isTutorial)
        {
            curSpeed -= slowSpeedRate * Time.deltaTime;
            if (curSpeed < 0f)
            {
                curSpeed = 0f;
                changeScene.DeadPalayer("Vel");
            }
            else if (curSpeed > maxSpeed)
            {
                curSpeed = maxSpeed;
            }
            speedBar.value = curSpeed;
            playerScript.playerSpeed = curSpeed;
        }
    }

    public void DecreaseSpeedByPoison()
    {
        if (!GameManager.instance.changeScene.isTutorial)
        {
            curSpeed -= decressSpeed;
        }
    }

    public void IncreaseSpeedByIon()
    {
        if (!GameManager.instance.changeScene.isTutorial)
        {
            curSpeed += increaseSpeed;
        }
    }

    public void OutAxon()
    {
        if (!GameManager.instance.changeScene.isTutorial)
        {
            changeScene.DeadPalayer("Axon");
        }
    }

    public void PushWrong()
    {
        if (!GameManager.instance.changeScene.isTutorial)
        {
            Debug.Log("Push Wrong Key");
            curSpeed -= 0.03f;
        }
    }
}
