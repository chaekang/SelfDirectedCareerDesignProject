using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerState { run, turn_up, turn_down, turn_right }; // 플레이어 상태 변수
public class _Player : MonoBehaviour
{
    public GameObject PlayerObject;
    public GameObject Dendrite;
    private Vector3 initialPosition;

    public float playerSpeed; // 플레이어 속도

    PlayerState state; // 플레이어 상태

    public bool poisonS = true;
    public bool poisonF = true;
    public bool IonR = false;
    public bool IonB = false;

    public SpeedBar speedBarScript;

    private Transform child;
    private SpriteRenderer childRenderer;
    private Transform ionItemspace;
    private Transform player;

    public GameObject synapseBar;
    public bool onSynapse = false;
    public bool camMove = false;

    float time; // 작아지는 시간 변수
    public bool disappear = false;
    public bool appear = false;

    public bool wall = false;

    public SpriteRenderer electronicSpace;
    public Sprite electricWhite;
    public Sprite electricRed;

    private SpriteRenderer channel;
    public Sprite channelK_fin;
    public Sprite channelNa_fin;

    void Start()
    {
        initialPosition = transform.position;
        speedBarScript = FindObjectOfType<SpeedBar>();
        state = PlayerState.run;
    }

    void Update()
    {
        //Debug.Log("Player State: " + state);
        PlayerchangeDirection();
        PlayerMove();

        // 시냅스에서 머리 위로 게이지바 띄우기
        synapseBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2f, 0));

        if (disappear)
        {
            if (time >= 2f)
            {
                transform.localScale = new Vector3(0.35f, 0.35f, 1) * (3 - time);
            }

            if (time > 3f)
            {
                time = 0;
                gameObject.SetActive(false);
                GameManager.instance.SynapseBar.gameObject.SetActive(false);
                appear = true;
                disappear = false;
                Debug.Log(appear);
            }
            time += Time.deltaTime;
        }
        if (appear)
        {
            Dendrite.gameObject.SetActive(true);
        }
    }

    // 플레이어 이동
    void PlayerMove()
    {
        if (state == PlayerState.run || state == PlayerState.turn_right)
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
        // 벽과 충돌
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("Wall 충돌");
            wall = true;
        }
        // 시냅스 끝에 도착
        else if (collision.gameObject.tag == "StopPoint")
        {
            playerSpeed = 0f;
            synapseBar.SetActive(true);
            onSynapse = true;
            camMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "IonRed") 
        {
            IonR = true;
            SetChild(collision); 
        }
        else if (collision.gameObject.tag == "IonBlue")
        {
            Debug.Log("B Trigger Enter");
            IonB = true;
            SetChild(collision);
        }
        else if (collision.gameObject.tag == "PoisonFish" || collision.gameObject.tag == "PoisonSnake") SetChild(collision);
        else if (collision.tag == "BackgroundIon_Space")
        {
            transform.position = initialPosition;
            speedBarScript.OutAxon();
        }
    }

    private void SetChild(Collider2D collision)
    {
        if (collision.gameObject.tag == "IonRed" || collision.gameObject.tag == "IonBlue") ionItemspace = collision.transform.GetChild(2);

        child = collision.transform.GetChild(0);
        childRenderer = child.GetComponent<SpriteRenderer>();
        
        channel = collision.transform.GetChild(1).GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "IonRed")
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                if (IonR && child.name == "IonR")
                {
                    ionItemspace.gameObject.SetActive(false);
                    TakeIon();
                }
            }
        }
        else if (collision.gameObject.tag == "IonBlue")
        {
            if (Input.GetKeyUp(KeyCode.S))
            {
                if (IonB && child.name == "IonB")
                {
                    Debug.Log("IN K Space, " + IonB);
                    ionItemspace.gameObject.SetActive(false);
                    TakeIon();
                }
            }
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
        Vector3 playerPosition = player.transform.position;
        Vector3 direction = playerPosition - child.position;
        if (child.name == "IonB")
        {
            child.position -= direction.normalized * 4f;
            speedBarScript.IncreaseSpeedByIon();
            IonB = false;

            channel.sprite = channelK_fin;

            SpriteRenderer KSpriteRenderer = childRenderer;
            StartCoroutine(IonFadeOut(KSpriteRenderer));
            StartCoroutine(Elec());
        }
        else if (child.name == "IonR")
        {
            child.position += direction.normalized * 4f;
            speedBarScript.IncreaseSpeedByIon();
            IonR = false;

            channel.sprite = channelNa_fin;

            SpriteRenderer NaSpriteRenderer = childRenderer;
            StartCoroutine(IonFadeOut(NaSpriteRenderer));
            StartCoroutine(Elec());


        }
    }

    // 전기신호 받음
    private IEnumerator Elec()
    {   
        electronicSpace.sprite = electricRed;
        yield return new WaitForSeconds(0.3f);
        electronicSpace.sprite = electricWhite;
    }

    // 이온 획득 후 fadeout
    IEnumerator IonFadeOut(SpriteRenderer childRenderer)
    {
        float cAlpha = childRenderer.color.a;
        while(cAlpha > 0)
        {
            //Debug.Log(childRenderer + " " + cAlpha);
            cAlpha -= Time.deltaTime * 1.2f;

            Color childColor = childRenderer.color;
            childColor.a = cAlpha;
            childRenderer.color = childColor;

            yield return new WaitForSeconds(0.02f);
        }
        child.gameObject.SetActive(false);
    }

    private void DeletePoison()
    {
        if (child != null)
        {
            child.gameObject.SetActive(false);
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
            if (poisonF)
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
