using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;         // 프리팹들을 보관할 변수
    Queue<GameObject>[] pools;           // 풀 담당을 하는 리스트들
    List<bool> synCol;                   // 부딪힌 여부를 저장할 리스트

    private void Awake()
    {
        pools = new Queue<GameObject>[prefabs.Length];
        synCol = new List<bool>();

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
            select.SetActive(true);
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
