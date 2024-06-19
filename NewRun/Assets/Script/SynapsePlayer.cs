using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynapsePlayer : MonoBehaviour
{
    public GameObject synapseBar;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.spawner.ntFinish)
        {
            synapseBar.SetActive(true);
            GameManager.instance.audioManager.PlaySound("Synapse");
        }

        // �ó������� �Ӹ� ���� �������� ����
        synapseBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2f, 0));

        if (GameManager.instance.player.disappear)
        {
            synapseBar.SetActive(false);
            gameObject.SetActive(false);
            GameManager.instance.player.transform.position = transform.position;
            GameManager.instance.player.gameObject.SetActive(true);
        }
    }
}
