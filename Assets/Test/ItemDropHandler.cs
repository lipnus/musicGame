using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDropHandler : MonoBehaviour{




    void OnCollisionStay2D(Collision2D col){

        Debug.Log("충돌");

        if(col.gameObject.tag == "item"){

            col.collider.GetComponent<ItemDragHandler>().collision = true;

            if(!col.collider.GetComponent<ItemDragHandler>().dragging){
                col.collider.gameObject.transform.position = transform.position;
                Destroy(col.collider.gameObject);//없에버리기
            }

        }
    }

    void OnCollisionExit2D(Collision2D col){
        col.collider.GetComponent<ItemDragHandler>().collision = false;

    }
}


     

	
