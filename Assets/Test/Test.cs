using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Test : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler{

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
        this.textObj
            = (GameObject.Find("StateText") as GameObject).GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseUp(){
        textObj.text = "OnMouseUp()";
    }
 
    void OnMouseDrag(){
        textObj.text = "OnMouseDrag()";
    }

    private void OnMouseExit(){
        textObj.text = "OnMouseExit()";
    }

    private void OnMouseUpAsButton(){
        textObj.text = "OnMouseUpAsButton()"; 
    }


    public void Up()
    {
        textObj.text = "Up()";
    }
}
