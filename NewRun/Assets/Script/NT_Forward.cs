using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NT_Forward : MonoBehaviour
{
    public float speed;

    [SerializeField]
    private Transform[] routes;

    private int routeToGo;          // 다음 곡선의 인덱스 저장
    private float tParam;           // 곡선 공식의 t 매개변수
    private float speedModifier;
    private Vector2 NTPosition;     // 신경전달물질의 위치
    private bool coroutineAllowed;  // 코루틴 1개만 실행되게 함
    private bool isBounded;         // 시냅스에 튕겼는지 확인


    private void Start()
    {
        speedModifier = Random.Range(0.05f, 0.1f);
        speed = Random.Range(25f, 35f);
        routeToGo = 0;
        tParam = 0f;
        coroutineAllowed = true;
    }

    private void Update()
    {
        if (!isBounded)
        {
            Vector2 position = transform.position;
            position.x -= -0.1f * speed * Time.deltaTime;
            transform.position = position;
        }
        else
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("NT_Destroy"))
        {
            isBounded = true;
        }
    }

    private IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;

        Vector2 p0 = routes[routeNumber].GetChild(0).position;
        Vector2 p1 = routes[routeNumber].GetChild(1).position;
        Vector2 p2 = routes[routeNumber].GetChild(2).position;
        Vector2 p3 = routes[routeNumber].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            NTPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
             Mathf.Pow(tParam, 3) * p3;

            transform.position = NTPosition;
            yield return null;
        }
        tParam = 0f;

        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
            GameManager.instance.pool.ReturnToPool(0, gameObject);
        }
        coroutineAllowed = true;
        isBounded = false;
    }
}
