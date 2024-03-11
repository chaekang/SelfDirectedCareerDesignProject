using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {run, turn_up, turn_down, turn_right}; // 플레이어 상태 변수
public class Player : MonoBehaviour
{
    public GameObject PlayerObject;

    public GameObject prfSynBar;
    public GameObject canvas;
    RectTransform synBar;

    Camera cam;

    public float playerSpeed = 4f; // 플레이어 속도
    public float synBarHeight;

    PlayerState state; // 플레이어 상태

    public bool poisonS = true;
    public bool poisonF = true;
    public bool IonR = false;
    public bool IonB = false;

    public SpeedBar speedBarScript;

    private Transform child;
    private Transform ionItemspace;
    private Transform player;


    private void Awake()
    {
        //cam = Camera.main;
    }

    void Start()
    {
        speedBarScript = FindObjectOfType<SpeedBar>();
        state = PlayerState.run;

        synBar = Instantiate(prfSynBar, canvas.transform).GetComponent<RectTransform>();
    }

    void Update()
    {
        Debug.Log("Player State: " + state); 
        PlayerchangeDirection();
        PlayerMove();

        //Vector3 _synBarPos =
            //cam.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + synBarHeight, 0));

        //synBar.position = _synBarPos;
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
        // 벽 부딪힘
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("Wall 충돌");
        }

        // 시냅스 끝에 도착하면 멈춤
        else if (collision.gameObject.tag == "StopPoint")
        {
            playerSpeed = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "IonRed" || collision.gameObject.tag == "IonBlue")              SetChild(collision);
        else if (collision.gameObject.tag == "PoisonFish" || collision.gameObject.tag == "PoisonSnake") SetChild(collision);

        if (collision.gameObject.tag == "IonR" || collision.gameObject.tag == "IonB")
            {
                Debug.Log("플레이어 이온 흡입");
                Destroy(collision.gameObject);
                speedBarScript.IncreaseSpeedByIon();
            }
    }

    private void SetChild(Collider2D collision)
    {
        if (collision.gameObject.tag == "IonRed" || collision.gameObject.tag == "IonBlue") ionItemspace = collision.transform.GetChild(2);

        child = collision.transform.GetChild(0);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "IonRed")
        {
            if (Input.GetKeyUp(KeyCode.A)) 
            {
                ionItemspace.gameObject.SetActive(false);
                IonR = true; 
            }
            if (IonR) TakeIon();
        }
        else if (collision.gameObject.tag == "IonBlue")
        {
            if (Input.GetKeyUp(KeyCode.S))
            {
                ionItemspace.gameObject.SetActive(false);
                IonB = true;
            }
            if (IonB) TakeIon();
        }
        else if (collision.gameObject.tag == "PoisonFish")
        {
            Debug.Log("PoisonFish 공간");
            if (Input.GetKeyUp(KeyCode.D))
            {
                poisonF = false;
                DeletePoison();
            }
        }
        else if (collision.gameObject.tag == "PoisonSnake")
        {
            Debug.Log("PoisonSnake 공간");
            if (Input.GetKeyUp(KeyCode.F))
            {
                poisonS = false;
                DeletePoison();
            }
        }
    }

    private void TakeIon()
    {
        if(child != null)
        {
            Vector3 direction = player.position - child.position;
            direction.Normalize();
            child.position += direction * 30f * Time.deltaTime;
        }
    }

    private void DeletePoison()
    {
        if(child != null)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "IonRed")
        {
            IonR = false;
        }
        else if (collision.gameObject.tag == "IonBlue")
        {
            IonB = false;
        }
        else if (collision.gameObject.tag == "PoisonFish")
        {
            if(poisonF) 
            { 
                Debug.Log("PoisonFish 삭제가 되지 않음");
                speedBarScript.DecreaseSpeedByPoison();
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
                speedBarScript.DecreaseSpeedByPoison();
            }
            else
            {
                poisonS = true;
                Debug.Log("PoisonSnake 삭제됨");
            }

        }
    }
}
