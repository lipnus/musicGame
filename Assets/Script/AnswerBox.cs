using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerBox : MonoBehaviour
{
	public Text mText; //현재 칸에 들어와있는 자음
	
	//자음이 답안박스에 와서 충돌
	void OnCollisionStay2D(Collision2D col){
		col.collider.GetComponent<InitialText>().collision = true;
		
		col.collider.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY; 
		
		//드래그가 끝남
		if (!col.collider.GetComponent<InitialText>().dragging){

			//원래 답안이 들어와 있던 상황에서 다른 게 들어옴
			if (mText != null && col.collider.GetComponent<Text>().text != mText.text) {
				mText.GetComponent<InitialText>().comebackToStartPoint();
			}
			else {
				col.collider.gameObject.transform.position = transform.position; //위치를 박스 안으로 고정
				col.collider.GetComponent<InitialText>().submit = true;
				mText = col.collider.GetComponent<Text>();
			}
		}
	}
	
	//모음이 답안박스와의 충돌에서 벗어남
	void OnCollisionExit2D(Collision2D col){
		col.collider.GetComponent<InitialText>().collision = false;
		col.collider.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		mText = null;
	}
}
