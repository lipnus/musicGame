using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InitialText : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{
 

    public bool dragging = false; //드래그 되고있는지
    public bool collision = false; //답안칸과 접촉여부
    public bool submit = false; //답안칸에 제출된상태인지
    Vector3 startPosition; //초기 생성위치

    
    public void OnBeginDrag(PointerEventData eventData){
        dragging = true;
    }

    //드래그할 동안은 터치 위치를 따라온다
    public void OnDrag(PointerEventData eventData){
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen);
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData){

        //답안칸 밖에서 시작한 드래그의 끝
        if (!submit){
            if (!collision) comebackToStartPoint();
            dragging = false;    
        }
        
        //답안칸 안에서 시작한 드래그의 끝
        if (submit){
            if (!collision){
                comebackToStartPoint(); 
                submit = false;
            }
            dragging = false;
        }
    }

    //초기생성위치로 복귀
    public void comebackToStartPoint() {
        transform.position = startPosition;
    }

    // Use this for initialization
    void Start(){
        startPosition = gameObject.transform.position; //초기 생성위치 저장
    }
}
