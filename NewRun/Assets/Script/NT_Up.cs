using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NT_Up : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;

    private int routeToGo;          // ���� ��� �ε��� ����
    private float tParam;           // � ������ t �Ű�����
    private Vector2 NTPosition;     // �Ű����޹����� ��ġ
    private float speedModifier;     // �Ű����޹����� �ӵ�
    private bool coroutineAllowed;  // �ڷ�ƾ 1���� ����ǰ� ��

    private void Start()
    {
        speedModifier = Random.Range(0.3f, 0.8f);
        routeToGo = 0;
        tParam = 0f;
        coroutineAllowed = true;
    }

    private void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
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
            GameManager.instance.pool.ReturnToPool(1, gameObject);
        }
        coroutineAllowed = true;
    }
}
