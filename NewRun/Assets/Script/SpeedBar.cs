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
    // Start is called before the first frame update
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
        maxSpeed = num;
        curSpeed = maxSpeed;
    }

    void CheckSpeed()
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

    public void DecreaseSpeedByPoison()
    {
        curSpeed -= decressSpeed;
    }

    public void IncreaseSpeedByIon()
    {
        curSpeed += increaseSpeed;
    }

    public void OutAxon()
    {
        changeScene.DeadPalayer("Axon");
        curSpeed = 0f;   
    }
}
