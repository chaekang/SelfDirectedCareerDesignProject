using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState { run, turn_up, turn_down, turn_right }; // �÷��̾� ���� ����
public class _Player : MonoBehaviour
{
    public GameObject PlayerObject;
    public GameObject Dendrite;
    private Vector3 initialPosition;

    public float playerSpeed; // �÷��̾� �ӵ�

    PlayerState state; // �÷��̾� ����

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

    // Ʃ�丮�� �ʿ�
    public bool wall = false;
    public bool finish = false;
    public bool Na = false;
    public bool K = false;
    public bool NaPoison = false;
    public bool KPoison = false;
    public bool canChangeDirection = true;

    // ü�� ����
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

    // �÷��̾� �̵�
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

    // �÷��̾� ���� ��ȯ ���� ����
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
        // ���� �浹
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("Wall �浹");
            wall = true;
        }
        // �ó��� ���� ����
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
        else if (collision.tag == "BackgroundIon_Space") // Axon ��Ż
        {
            speedBarScript.OutAxon();
        }

        // Ʃ�丮��
        if (collision.gameObject.tag == "Finish")
        {
            finish = true;
        }
    }

    // ü���� �ִϸ��̼� Ȱ��ȭ ������Ʈ, �� �Ҵ�
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

    // ü�� �ݶ��̴� ������ ���� ���
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
            Debug.Log("PoisonFish ����");
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
            Debug.Log("PoisonSnake ����");
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

    // �̿� ȹ�� 
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

    // �����ȣ ����
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
                    Debug.Log("PoisonFish ������ ���� ����");
                    speedBarScript.DecreaseSpeedByPoison();

                }
            }
            else
            {
                Debug.Log("PoisonFish ������");
            }
        }
        else if (collision.gameObject.tag == "Channel_K_Poison")
        {
            if (poisonS)
            {
                if (!GameManager.instance.changeScene.isTutorial)
                {
                    Debug.Log("PoisonFPoisonSnakeish ������ ���� ����");
                    speedBarScript.DecreaseSpeedByPoison();
                }
            }
            else
            {
                Debug.Log("PoisonSnake ������");
            }

        }
    }
}
