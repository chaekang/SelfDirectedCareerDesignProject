using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_action : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Transform mouseOverObj;
    // Start is called before the first frame update
    void Start()
    {
        mouseOverObj = gameObject.transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData) 
    {
        //Debug.Log("button Mouse In " + mouseOverObj.name);
        mouseOverObj.gameObject.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("button Mouse Out" + mouseOverObj.name);
        mouseOverObj.gameObject.SetActive(false);

    }
}
