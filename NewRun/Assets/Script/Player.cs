using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState { run, turn_up, turn_down, turn_right }; // �÷��̾� ���� ����
public class Player : MonoBehaviour
{
    public GameObject PlayerObject;

    public float playerSpeed; // �÷��̾� �ӵ�

    PlayerState state; // �÷��̾� ����

    public bool poisonS = true;
    public bool poisonF = true;
    public bool IonR = false;
    public bool IonB = false;

    public SpeedBar speedBarScript;

    private Transform child;
    private Transform ionItemspace;
    private Transform player;

    public GameObject synapseBar;
    public bool onSynapse = false;    // NT ����
    public bool isStart = false;      // �ó��� ��� ����

    void Start()
    {
        speedBarScript = FindObjectOfType<SpeedBar>();
        state = PlayerState.run;
    }

    void Update()
    {
        Debug.Log("Player State: " + state);
        PlayerchangeDirection();
        PlayerMove();

        // �ó������� �Ӹ� ���� �������� ����
        synapseBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f, 0));
    }

    // �÷��̾� �̵�
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

    // �÷��̾� ���� ��ȯ ���� ����
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
        // ���� �浹
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("Wall �浹");
        }
        // �ó��� ���� ����
        else if (collision.gameObject.tag == "StopPoint")
        {
            playerSpeed = 0f;
            synapseBar.SetActive(true);
            onSynapse = true;
            isStart = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "IonRed" || collision.gameObject.tag == "IonBlue") SetChild(collision);
        else if (collision.gameObject.tag == "PoisonFish" || collision.gameObject.tag == "PoisonSnake") SetChild(collision);

        if (collision.gameObject.tag == "IonR" || collision.gameObject.tag == "IonB")
        {
            Debug.Log("�÷��̾� �̿� ����");
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
            if (IonR && child.name == "IonR") TakeIon();
            else Debug.Log("No in IonRed collider");
        }
        else if (collision.gameObject.tag == "IonBlue")
        {
            if (Input.GetKeyUp(KeyCode.S))
            {
                ionItemspace.gameObject.SetActive(false);
                IonB = true;
            }
            if (IonB && child.name == "IonB") TakeIon();
            else Debug.Log("No in IonBlue collider");
        }
        else if (collision.gameObject.tag == "PoisonFish")
        {
            Debug.Log("PoisonFish ����");
            if (Input.GetKeyUp(KeyCode.D))
            {
                poisonF = false;
                DeletePoison();
            }
        }
        else if (collision.gameObject.tag == "PoisonSnake")
        {
            Debug.Log("PoisonSnake ����");
            if (Input.GetKeyUp(KeyCode.F))
            {
                poisonS = false;
                DeletePoison();
            }
        }
    }

    private void TakeIon()
    {
        if (child != null)
        {
            Vector3 direction = player.position - child.position;
            direction.Normalize();
            child.position += direction * 30f * Time.deltaTime;
        }
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
                Debug.Log("PoisonFish ������ ���� ����");
                speedBarScript.DecreaseSpeedByPoison();
            }
            else
            {
                poisonF = true;
                Debug.Log("PoisonFish ������");
            }
        }
        else if (collision.gameObject.tag == "PoisonSnake")
        {
            if (poisonS)
            {
                Debug.Log("PoisonFPoisonSnakeish ������ ���� ����");
                speedBarScript.DecreaseSpeedByPoison();
            }
            else
            {
                poisonS = true;
                Debug.Log("PoisonSnake ������");
            }

        }
    }
}
