using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//각 아이템이 들고 있는 스크립트
public class ItemObj : MonoBehaviour {

	public GameObject focused;
	public GameObject standard;
	public GameObject applied;
	public GameObject product;
	public GameObject locked;

	public SoundManager2 soundManager;

	
	public int curCode; //이 옷의 코드

	private int topCode;
	private int bottomCode;
	private int shoesCode;
	
	private ShopApp shopApp;
	
	 
	void Start () {
		shopApp = GameObject.Find("Shop_App").GetComponent<ShopApp>();
		
		shopApp.items.Add(gameObject); //스스로의 갹체를 전달
		updateItemState();
	}

	//각 아이콘의 상태 표시
	public void updateItemState() {
		applied.active = false;
		locked.active = false;

		if (Utils.isHaveItem(curCode)) {
			if (Utils.isWearItem(curCode)) { //입고있다
				applied.active = true;
			}
			else{ //가지고만 있다
			
			}
		}else { //없다
			locked.active = true;
		}
		
	}

	public void hideActivatedImg() {
		try {//선택된 표시 제거(하나도 선택된게 없으면 에러나서 try~catch)
			GameObject.Find("focused").gameObject.active = false; 
		}catch (Exception e) {}  
	}

	public void onClick_product() {
		soundManager.playSound(1); //클릭소리

		
		//앱화면
		shopApp.setCode(curCode);
//		shopApp.cancelHeart();
		shopApp.removeInfo();

		
		if (focused.active) {
			focused.active = false;
		}
		else {
			hideActivatedImg();
			focused.active = true;
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
