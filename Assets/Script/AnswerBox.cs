using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerBox : MonoBehaviour
{
	public string mAnswer = ""; //현재 칸에 입력된 텍스트
	
	//모음이 답안박스에 와서 충돌
	void OnCollisionStay2D(Collision2D col){
		col.collider.GetComponent<InitialText>().collision = true;
		
		//드래그가 끝남
		if (!col.collider.GetComponent<InitialText>().dragging){
			col.collider.gameObject.transform.position = transform.position; //위치를 박스 안으로 고정
			col.collider.GetComponent<InitialText>().submit = true;

			mAnswer = col.collider.GetComponent<Text>().text;
		}
	}
	
	//모음이 답안박스와의 충돌에서 벗어남
	void OnCollisionExit2D(Collision2D col){
		col.collider.GetComponent<InitialText>().collision = false;
		
		mAnswer = "";
	}
}
