using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GuideManager : MonoBehaviour {

	public GameObject fieldManager;
	public GameObject guideObj; //가이드가 발동되면 여기가 등록시켜두고, 클릭시 해당 가이드 파괴
	public GameObject midText;
	
	
	public void onClick_guide() {
		if (guideObj != null) {
			Debug.Log("가이드 파괴");
			
			midText.GetComponent<Animator>().SetBool("showText", false);
			fieldManager.GetComponent<TutorialFieldManager>().resumeMove();
			Destroy(guideObj);	
		}
		
	}
	 
}
