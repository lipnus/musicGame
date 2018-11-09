﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopApp : MonoBehaviour {

	public int curCode;
	public GameObject category_text;
	public GameObject title_text;
	public GameObject price_text;
	public GameObject product_img;
	public GameObject note_img;
	public GameObject heart_img;
	public GameObject purchase_btn;
	public GameObject wear_btn;
	public GameObject wore_img;

	public List<GameObject> items = new List<GameObject>();

	private bool heartSelected = false;


	void Start() {
		setInitialProduct();
		
//		PlayerPrefs.DeleteAll();
//		GlobalScript.firstGift();
		GlobalScript.setScore(10000);
	}
	
	//구매하기
	public void onClick_purchase() {
		ItemInfo item = GlobalScript.getItemInfo(curCode);
		int score = GlobalScript.getScore();
		
		if (score >= item.Price) {

			score -= item.Price;
			GlobalScript.setScore(score);
			GlobalScript.addMyItem(curCode); //새로 산 아이템 표시
			showBtn(); //버튼이 바뀌겠지
			Debug.Log("구매완료");
		}
		else {
			Debug.Log("돈이없다. 가진돈: " + GlobalScript.getScore());
		}
	}

	//옷입기
	public void onClick_wear() {
		if (curCode / 100 == 1) GlobalScript.setTop(curCode);
		else if (curCode / 100 == 2) GlobalScript.setBottom(curCode);
		else if (curCode / 100 == 3) GlobalScript.setShoes(curCode);

		showBtn(); //버튼변경

		//각 아이템들의 착용상태 아이콘 업데이트
		for (int i = 0; i < 5; i++) {
			items[i].GetComponent<ItemObj>().updateAppliedState();
		}
	
	}

	//코드입력받기
	public void setCode(int curCode) {
		this.curCode = curCode;
	}
	
	
	//초기세팅(입고있는 상의를 표시)
	public void setInitialProduct() {
		int topCode = GlobalScript.getTop();
		showInfo(topCode);
	}
	
	
	//하트터치
	public void onClick_heart() {
		if (!heartSelected) {
			heart_img.GetComponent<Image>().sprite = GameObject.Find("app_heart_img").
				transform.Find("full_heart_img").GetComponent<Image>().sprite;
			heartSelected = true;
		}
		else {
			heart_img.GetComponent<Image>().sprite = GameObject.Find("app_heart_img").
				transform.Find("empty_heart_img").GetComponent<Image>().sprite;
			heartSelected = false;
		}

	}
	
	//정보 삭제
	public void removeInfo() {
		heart_img.active = false;
		note_img.active = false;
		category_text.GetComponent<Text>().text = "";
		title_text.GetComponent<Text>().text = "";
		price_text.GetComponent<Text>().text = "";
		product_img.GetComponent<Image>().sprite = null;
		purchase_btn.active = false;
		wear_btn.active = false;
		wore_img.active = false;
	}
	
	
	//정보표시
	public void showInfo(int curCode) {
		ItemInfo itemInfo = GlobalScript.getItemInfo(curCode);
		category_text.GetComponent<Text>().text = itemInfo.ShoppingCategory;
		title_text.GetComponent<Text>().text = itemInfo.Name;
		price_text.GetComponent<Text>().text = itemInfo.Price.ToString();
	}

	//제품샷 출력
	public void setProductImg(Sprite productSprite) {
		product_img.GetComponent<Image>().sprite = productSprite;

	}
	
	//하트출력
	public void showHeart() {
		heart_img.active = true;
	}

	//음표출력
	public void showNote() {
		note_img.active = true;
	}

	//버튼부분 출력
	public void showBtn() {
		purchase_btn.active = false;
		wear_btn.active = false;
		wore_img.active = false;
		
		if (GlobalScript.isHaveItem(curCode)) {
			if (GlobalScript.isWearItem(curCode)) wore_img.active = true;
			else wear_btn.active = true;
		}
		else {
			purchase_btn.active = true;
		}
	}
}