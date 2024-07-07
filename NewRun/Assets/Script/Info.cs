using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Info : MonoBehaviour
{
    public Image[] info;
    public GameObject info_pannel;
    public GameObject exitBtn;

    int index = 0; //sprite�� �迭�� ���� ���� ù ��° 0���� ����

    public void Click_info_btn()
    {
        info[index].gameObject.SetActive(true);
        info_pannel.SetActive(true);
        exitBtn.SetActive(false); // info_pannel�� ������ exitBtn�� ��
    }

    public void Click_x_btn()
    {
        info[index].gameObject.SetActive(false);
        index = 0;
        info_pannel.SetActive(false);
        exitBtn.SetActive(true); // info_pannel�� ������ exitBtn�� ��
    }

    public void Click_rightbtn()
    {
        if (info.Length > index + 1)
        {
            info[index].gameObject.SetActive(false);
            index++;
            info[index].gameObject.SetActive(true);
        }
        else if (info.Length == index + 1)
        {
            info[index].gameObject.SetActive(false);
            index = 0;
            info[index].gameObject.SetActive(true);
        }
    }

    public void Click_leftbtn()
    {
        if (0 < index)
        {
            info[index].gameObject.SetActive(false);
            index--;
            info[index].gameObject.SetActive(true);
        }
        else if (index == 0)
        {
            info[index].gameObject.SetActive(false);
            index = info.Length - 1;
            info[index].gameObject.SetActive(true);
        }
    }
}
