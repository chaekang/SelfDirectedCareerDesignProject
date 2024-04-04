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

    private Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<Player>();
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
            Debug.Log("==== 게임오버 ====");
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
        curSpeed -= 1f;
    }

    public void IncreaseSpeedByIon()
    {
        curSpeed += 1f;
    }
}
