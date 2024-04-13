using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomaPanelManager : MonoBehaviour
{
    public Soma soma;
    public GameObject infoPanel;
    public GameObject continuePanel;

    private bool panelsActivated = false; 

    private void Update()
    {
        if (soma.isFinish && !panelsActivated)
        {
            StartCoroutine(ActivateInfoPanel());
            panelsActivated = true; 
        }
    }

    IEnumerator ActivateInfoPanel()
    {
        infoPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        infoPanel.SetActive(false);

        // �� ��° �г� Ȱ��ȭ
        continuePanel.SetActive(true);
    }
}
