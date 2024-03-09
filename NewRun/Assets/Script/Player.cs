using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {run, turn_up, turn_down, turn_right}; // 플레이어 상태 변수
public class Player : MonoBehaviour
{
    public GameObject PlayerObject;

    public float playerSpeed = 4f; // 플레이어 속도

    PlayerState state; // 플레이어 상태

    public bool poisonS = true;
    public bool poisonF = true;

    public SpeedBar speedBarScript;
    public Ranvier ranvier;

    // Start is called before the first frame update
    void Start()
    {
        speedBarScript = FindObjectOfType<SpeedBar>();
        state = PlayerState.run;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Player State: " + state); 
        PlayerchangeDirection();
        PlayerMove();
    }

    // 플레이어 이동
    void PlayerMove()
    {
        if(state == PlayerState.run || state == PlayerState.turn_right)
        {
            transform.Translate(1 * playerSpeed * Time.deltaTime, 0, 0);
        }
        else if (state == PlayerState.turn_up)
        {
            transform.Translate(0, 1 * playerSpeed * Time.deltaTime, 0);
        }
        else if (state == PlayerState.turn_down)
        {
            transform.Translate(0, -1 * playerSpeed * Time.deltaTime, 0);

        }
    }
    
    // 플레이어 방향 전환 상태 저장
    void PlayerchangeDirection()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            state = PlayerState.turn_right;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            state = PlayerState.turn_up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            state = PlayerState.turn_down;
        }
    }
   

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("Wall 충돌");
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "IonRed")
        {
            Debug.Log("IonRed 공간");
            Transform child = collision.gameObject.transform.Find("IonR");
            if (Input.GetKeyUp(KeyCode.A))
            {
                if(child != null)
                {
                    child.gameObject.SetActive(true);
                    speedBarScript.IncreaseSpeedByIon();
                }
            }
        }
        else if (collision.gameObject.tag == "IonBlue")
        {
            Debug.Log("IonBlue 공간");
            Transform child = collision.gameObject.transform.Find("IonB");
            if (Input.GetKeyUp(KeyCode.S))
            {
                if (child != null)
                {
                    child.gameObject.SetActive(true);
                    speedBarScript.IncreaseSpeedByIon();
                }
            }
        }
        else if (collision.gameObject.tag == "PoisonFish")
        {
            Debug.Log("PoisonFish 공간");
            if (Input.GetKeyUp(KeyCode.D))
            {
                poisonF = false;
                //Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.tag == "PoisonSnake")
        {
            Debug.Log("PoisonSnake 공간");
            if (Input.GetKeyUp(KeyCode.F))
            {
                poisonS = false;
                //Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "IonRed")
        {
        }
        else if (collision.gameObject.tag == "IonBlue")
        {
        }
        else if (collision.gameObject.tag == "PoisonFish")
        {
            if(poisonF) 
            { 
                Debug.Log("PoisonFish 삭제가 되지 않음"); 
            }
            else 
            {
                poisonF = true;
                Debug.Log("PoisonFish 삭제됨");
            }
        }
        else if (collision.gameObject.tag == "PoisonSnake")
        {
            if (poisonS)
            {
                Debug.Log("PoisonFPoisonSnakeish 삭제가 되지 않음");
            }
            else
            {
                poisonS = true;
                Debug.Log("PoisonSnake 삭제됨");
            }

        }
    }
}
