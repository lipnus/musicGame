using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObj : MonoBehaviour {
	
	public int category; //0:상의, 1:하의 2:신발 3:기타
	public int perchase; //0:포인트로 구매, 1:캐쉬로 구매
	public int code; //아이템고유코드
	public string name; //제품명
	public int price; //가격
	public string shopping_category; //앱에 표시되는 가라 카테고리
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
