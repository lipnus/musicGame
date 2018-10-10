using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler,  IDragHandler, IEndDragHandler {

    Text textObj;

    public bool dragging = false;
    public bool collision = false;
    Vector3 position;



    public void OnBeginDrag(PointerEventData eventData){
        position = gameObject.transform.position; //시작위치 저장
        dragging = true;
    }




    public void OnDrag(PointerEventData eventData) {

        //transform.position = Input.mousePosition;

        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen);

        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);

        textObj.text = mousePosition.ToString();
    }



    public void OnEndDrag(PointerEventData eventData){
       
        //충돌 이벤트가 일어나지 않았으면 원위치로 복귀
        if(!collision){
            //transform.localPosition = Vector3.zero;
            transform.position = position;
            Debug.Log("원위치:" + position);
        }
        dragging = false;
    }

    // Use this for initialization
    void Start () {
        textObj = (GameObject.Find("StateText") as GameObject).GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
