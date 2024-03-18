using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;         // �����յ��� ������ ����
    Queue<GameObject>[] pools;           // Ǯ ����� �ϴ� ����Ʈ��

    private void Awake()
    {
        pools = new Queue<GameObject>[prefabs.Length];

        for (int index = 0;index < pools.Length; index++)
        {
            pools[index] = new Queue<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        if (pools[index].Count > 0)
        {
            select = pools[index].Dequeue();
        }
        else
        {
            select = Instantiate(prefabs[index], transform);
        }

        return select;
    }

    public void ReturnToPool(int index, GameObject obj)
    {
        obj.SetActive(false);
        pools[index].Enqueue(obj);
    }
}
