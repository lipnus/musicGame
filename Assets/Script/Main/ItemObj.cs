using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//각 아이템이 들고 있는 스크립트
public class ItemObj : MonoBehaviour {

	public GameObject activated;
	public GameObject standard;
	public GameObject applied;
	public GameObject product;
	
	public int curCode; //이 옷의 코드

	private int topCode;
	private int bottomCode;
	private int shoesCode;
	
	private ShopApp shopApp;
	
	 
	void Start () {
		shopApp = GameObject.Find("Shop_App").GetComponent<ShopApp>();
		
		shopApp.items.Add(gameObject); //스스로의 정보를 전달
		updateAppliedState();
	}

	//착용상태 표시
	public void updateAppliedState() {
		applied.active = false;
		if (GlobalScript.isWearItem(curCode)) {
			applied.active = true;
		}
	}

	public void hideActivatedImg() {
		try {//선택된 표시 제거(하나도 선택된게 없으면 에러나서 try~catch)
			GameObject.Find("activated").gameObject.active = false; 
		}catch (Exception e) {}  
	}

	public void onClick_product() {
		
		//앱화면
		
		shopApp.setCode(curCode);
		shopApp.removeInfo();

		
		if (activated.active) {
			activated.active = false;
		}
		else {
			hideActivatedImg();
			activated.active = true;
			shopApp.showHeart();
			shopApp.showNote();
			
			//구매,착용 유무에 맞게 버튼출력
			shopApp.showBtn();
			
			//정보와 이미지 출력
			shopApp.showInfo(curCode);
			shopApp.setProductImg( product.GetComponent<Image>().sprite );
		} 
	}
}
