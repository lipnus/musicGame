using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MouseEvent : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler{

    // 인터페이스 트리거 관련
    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log(transform.name);
    }

    public void OnPointerEnter(PointerEventData data)
    {
        Debug.Log("MouseOver");
    }




    Text textObj;

    // Use this for initialization
    void Start () {
        textObj = (GameObject.Find("StateText") as GameObject).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Up(){
        Debug.Log("Up()");
        textObj.text = "Up()";
    }

    public void Down(){
        Debug.Log("Down()");
        textObj.text = "Down()";
    }

    public void EndDrag(){
        Debug.Log("EndDrag()");
        textObj.text = "EndDrag()";
    }

    public void Enter(){
        Debug.Log("Enter()");
        textObj.text = "Enter()";
    }
}
