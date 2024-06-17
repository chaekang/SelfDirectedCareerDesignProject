using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public bool channel_Na = false;
    public bool channel_K = false;

    public SpeedBar speedBarScript;

    private Transform init_channel;
    private Transform channel_anim;
    private Transform poison;

    private Transform player;

    public bool onSynapse = false;
    public bool camMove = false;
    float time = 0;
    public bool disappear = false;
    public bool appear = false;

    public SpriteRenderer electronicSpace;
    public Sprite electricWhite;
    public Sprite electricRed;

    public SpriteRenderer playerGarphic;
    public Sprite up;
    public Sprite down;
    public Sprite right;

    public GameObject synapsePlayer;

    // 튜토리얼에 필요
    public bool wall = false;
    public bool finish = false;
    public bool Na = false;
    public bool K = false;
    public bool NaPoison = false;
    public bool KPoison = false;
    public bool canChangeDirection = true;

    // 체널 개수
    public TextMeshProUGUI Na_num;
    public TextMeshProUGUI K_num;
    public TextMeshProUGUI NaToxic_num;
    public TextMeshProUGUI KToxic_num;
    public int NaNum;
    public int KNum;
    public int NaToxicNum;
    public int KToxicNum;

    void Start()
    {
        initialPosition = transform.position;
        speedBarScript = FindObjectOfType<SpeedBar>();
        state = PlayerState.run;
        synapsePlayer.SetActive(false);
    }

    void Update()
    {
        PlayerchangeDirection();
        PlayerMove();

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
            playerGarphic.sprite = right;
            transform.Translate(1 * playerSpeed * Time.deltaTime, 0, 0);
        }
        else if (state == PlayerState.turn_up)
        {
            playerGarphic.sprite = up;
            transform.Translate(0, 1 * playerSpeed * Time.deltaTime, 0);
        }
        else if (state == PlayerState.turn_down)
        {
            playerGarphic.sprite = down;
            transform.Translate(0, -1 * playerSpeed * Time.deltaTime, 0);

        }
    }

    // 플레이어 방향 전환 상태 저장
    void PlayerchangeDirection()
    {
        if (canChangeDirection)
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
            camMove = true;
            if (!GameManager.instance.changeScene.isTutorial && !disappear)
            {
                onSynapse = true;
            }
            gameObject.SetActive(false);
            synapsePlayer.SetActive(true);
            GameManager.instance.SynapseBar.curPoint = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Channel_Na") 
        {
            channel_Na = true;
            SetChild(collision); 
        }
        else if (collision.gameObject.tag == "Channel_K")
        {
            channel_K = true;
            SetChild(collision);
        }
        else if (collision.gameObject.tag == "Channel_Na_Poison")
        {
            poisonF = true;
            SetChild(collision);
        }
        else if (collision.gameObject.tag == "Channel_K_Poison")
        {
            poisonS = true;
            SetChild(collision);
        }
        else if (collision.gameObject.tag == "PoisonFish" || collision.gameObject.tag == "PoisonSnake") SetChild(collision);
        else if (collision.tag == "BackgroundIon_Space") // Axon 이탈
        {
            speedBarScript.OutAxon();
        }

        // 튜토리얼
        if (collision.gameObject.tag == "Finish")
        {
            finish = true;
        }
    }

    // 체널의 애니메이션 활성화 오브젝트, 독 할당
    private void SetChild(Collider2D collision)
    {
        if (collision.gameObject.tag == "Channel_Na" || collision.gameObject.tag == "Channel_K")
        {
            init_channel = collision.transform.GetChild(0);
            channel_anim = collision.transform.GetChild(1);
        }
        else if (collision.gameObject.tag == "Channel_Na_Poison" || collision.gameObject.tag == "Channel_K_Poison")
        {
            poison = collision.transform.GetChild(1);
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // 체널 콜라이더 안으로 들어올 경우
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Channel_Na")
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                if (channel_Na)
                {
                    if (!GameManager.instance.changeScene.isTutorial)
                    {
                        NaNum--;
                        Na_num.text = NaNum.ToString();
                    }
                    Na = true;
                    TakeIon();
                }
            } 
            else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.F))
            {
                if (!GameManager.instance.changeScene.isTutorial)
                    speedBarScript.PushWrong();
            }
        }
        else if (collision.gameObject.tag == "Channel_K")
        {
            if (Input.GetKeyUp(KeyCode.S))
            {
                if (channel_K)
                {
                    if (!GameManager.instance.changeScene.isTutorial)
                    {
                        KNum--;
                        K_num.text = KNum.ToString();
                    }
                    K = true;
                    TakeIon();
                }
            }
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.F))
            {
                if (!GameManager.instance.changeScene.isTutorial)
                    speedBarScript.PushWrong();
            }
        }
        else if (collision.gameObject.tag == "Channel_Na_Poison")
        {
            Debug.Log("PoisonFish 공간");
            if (Input.GetKeyUp(KeyCode.D))
            {
                poisonF = false;
                NaPoison = true;
                if (!GameManager.instance.changeScene.isTutorial)
                {
                    NaToxicNum--;
                    NaToxic_num.text = NaToxicNum.ToString();
                }
                DeletePoison();
            }
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.F))
            {
                if (!GameManager.instance.changeScene.isTutorial)
                    speedBarScript.PushWrong();
            }

        }
        else if (collision.gameObject.tag == "Channel_K_Poison")
        {
            Debug.Log("PoisonSnake 공간");
            if (Input.GetKeyUp(KeyCode.F))
            {
                poisonS = false;
                KPoison = true;
                if (!GameManager.instance.changeScene.isTutorial)
                {
                    KToxicNum--;
                    KToxic_num.text = KToxicNum.ToString();
                }
                DeletePoison();
            }
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S))
            {
                if (!GameManager.instance.changeScene.isTutorial)
                    speedBarScript.PushWrong();
            }

        }
    }

    // 이온 획득 
    private void TakeIon()
    {
        channel_K = false;  
        channel_Na = false;
        init_channel.gameObject.SetActive(false);
        channel_anim.gameObject.SetActive(true);
        StartCoroutine(Elec());
        if (!GameManager.instance.changeScene.isTutorial) {
            speedBarScript.IncreaseSpeedByIon();
        }
    }

    // 전기신호 받음
    private IEnumerator Elec()
    {
        electronicSpace.sprite = electricRed;
        yield return new WaitForSeconds(0.3f);
        electronicSpace.sprite = electricWhite;
    }

    private void DeletePoison()
    {
        poison.gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Channel_Na")
        {
            channel_Na = false;
        }
        else if (collision.gameObject.tag == "Channel_K")
        {
            channel_K = false;
        }
        else if (collision.gameObject.tag == "Channel_Na_Poison")
        {
            if (poisonF)
            {
                if (!GameManager.instance.changeScene.isTutorial)
                {
                    Debug.Log("PoisonFish 삭제가 되지 않음");
                    speedBarScript.DecreaseSpeedByPoison();

                }
            }
            else
            {
                Debug.Log("PoisonFish 삭제됨");
            }
        }
        else if (collision.gameObject.tag == "Channel_K_Poison")
        {
            if (poisonS)
            {
                if (!GameManager.instance.changeScene.isTutorial)
                {
                    Debug.Log("PoisonFPoisonSnakeish 삭제가 되지 않음");
                    speedBarScript.DecreaseSpeedByPoison();
                }
            }
            else
            {
                Debug.Log("PoisonSnake 삭제됨");
            }

        }
    }
}
