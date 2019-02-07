using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopApp : MonoBehaviour {

	public int curCode;
	public GameObject category_text;
	public GameObject title_text;
	public GameObject price_text;
	public GameObject product_img;
	public GameObject note_img;
	public GameObject coin_img;
	public GameObject heart_img;
	public GameObject purchase_btn;
	public GameObject wear_btn;
	public GameObject wore_img;
	public SoundManager2 soundManager;
	public GameObject user;

	public Text pointText;
	
	public GameObject moneyEffect;


	public List<GameObject> items = new List<GameObject>();

	private bool heartSelected = false;


	void Start() {

		setInitialProduct();
		StartCoroutine(showPoint(0.1f));
	}

	IEnumerator showPoint(float delayTime) {
		yield return new WaitForSeconds(delayTime);
		pointText.text = Utils.getPoint().ToString();
		pointText.rectTransform.sizeDelta = new Vector2(pointText.preferredWidth, pointText.preferredHeight);
	}
	
	
	
	//구매하기
	public void onClick_purchase() {
		soundManager.playSound(1); //클릭소리
		user.GetComponent<User>().startShowIcon(1);

		ItemInfo item = Utils.getItemInfo(curCode);
		int point = Utils.getPoint();
		
		Debug.Log("구매구매" + item.Code);
		
		if (item.Perchase == 0) {	//의류구매
			buyColth(item, point);
		}else if (item.Perchase == 1) {	 //악세사리 구매
			buyAccessory(item, point);
		}
		
		pointText.text = Utils.getPoint().ToString(); //포인트 새로고침
		showBtn(); //버튼상태 새로고침
		updateAllItemState(); //아이템 상태 새로고침
	}
	

	public void buyAccessory(ItemInfo item, int point) {
		
		//캐쉬로 사야하지만 지금은 무조건 살 수 있게 함
		Debug.Log("악세사리 구매");
		
		soundManager.playSound(3); //구매
		Utils.addMyItem(curCode); //구매목록에 추가
	}

	
	public void buyColth(ItemInfo item, int point) {
		
		if (point >= item.Price) {
			point -= item.Price;
			soundManager.playSound(3); 
			Utils.addMyItem(curCode); //구매목록에 추가
			Utils.modifyScore( -1 * item.Price);
		}
		else {
			Debug.Log("돈이없다. 가진돈: " + Utils.getPoint());
		}
		
	}

	
	//착용하기
	public void onClick_wear() {
		soundManager.playSound(1); //클릭소리

		if (curCode / 100 == 1) Utils.setTop(curCode);
		else if (curCode / 100 == 2) Utils.setBottom(curCode);
		else if (curCode / 100 == 3) Utils.setShoes(curCode);

		user.active = false;
		user.active = true;
		user.GetComponent<User>().wearCloth();

		showBtn(); //버튼변경
		updateAllItemState();
	}

	
	//각 아이템들의 착용상태 아이콘 업데이트
	public void updateAllItemState() {
		for (int i = 0; i < items.Count(); i++) {
			items[i].GetComponent<ItemObj>().updateItemState();
		}
	}

	//코드입력받기
	public void setCode(int curCode) {
		this.curCode = curCode;
	}
	
	
	//초기세팅(입고있는 상의를 표시)
	public void setInitialProduct() {
		int topCode = Utils.getTop();
		showInfo(topCode);
	}
	
	
	//하트터치
	public void onClick_heart() {
		soundManager.playSound(1);
		
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

	public void cancelHeart() {
		heart_img.GetComponent<Image>().sprite = GameObject.Find("app_heart_img").
			transform.Find("empty_heart_img").GetComponent<Image>().sprite;
		heartSelected = false;
	}
	
	//정보 삭제
	public void removeInfo() {
		heart_img.active = false;
		note_img.active = false;
		coin_img.active = false;
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
		ItemInfo itemInfo = Utils.getItemInfo(curCode);
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
		coin_img.active = false;
		note_img.active = true;
	}
	
	//코인출력
	public void showCoin() {
		note_img.active = false;
		coin_img.active = true;
	}

	//버튼출력(구매,착용,착용중)
	public void showBtn() {
		purchase_btn.active = false;
		wear_btn.active = false;
		wore_img.active = false;
		
		if (Utils.isHaveItem(curCode)) {
			if (Utils.isWearItem(curCode)) wore_img.active = true;
			else wear_btn.active = true;
		}
		else {
			purchase_btn.active = true;
		}
	}
}
