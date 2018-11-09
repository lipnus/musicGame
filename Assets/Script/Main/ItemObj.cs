using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//각 아이템이 들고 있는 스크립트
public class ItemObj : MonoBehaviour {

	public Image active;
	public Image inActive;
	public Image applied;
	public Image product;
	
	public int curCode; //이 옷의 코드

	private int topCode;
	private int bottomCode;
	private int shoesCode;
	
	//(각자 아이템마다 이런식으로 하는게 효율은 떨어질 것 같지만 이 부분은 연산을 크게 요하는 부분이 아니니 성능에는 크게 문제가 없을 것 같음)
	void Start () {
		
		//캐릭터 착용상태 확인
		int topCode = GlobalScript.getTop();
		int bottomCode = GlobalScript.getBottom();
		int shoesCode = GlobalScript.getShoes();
		
		//착용중 아이콘 표시
		if (curCode == topCode || curCode == bottomCode || curCode == shoesCode) {
			applied.enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
