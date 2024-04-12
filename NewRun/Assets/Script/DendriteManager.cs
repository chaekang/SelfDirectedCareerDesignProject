using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DendriteManager : MonoBehaviour
{
    public List<GameObject> dendrites;
    public GameObject ActionPotential;
    public bool isOverlap = false;  // 5���� �� �𿴴��� Ȯ��
    public bool isDisappear =false;

    private void Start()
    {
        ActivateDendrite(0);
    }

    private void Update()
    {
        for (int i = 0; i < dendrites.Count - 1; i++)
        {
            // SomaDendrites ��ũ��Ʈ�� isActivated ������ ����Ͽ� ���� Ȯ��
            SomaDendrite dendriteScript = dendrites[i].GetComponent<SomaDendrite>();
            if (dendriteScript != null && dendriteScript.onSoma)
            {
                ActivateDendrite(i + 1);
            }
        }

        if (dendrites[dendrites.Count - 1].transform.position.x >= 148.95)
        {
            isOverlap = true;
        }
    }


    void ActivateDendrite(int index)
    {
        dendrites[index].gameObject.SetActive(true);
    }
}
