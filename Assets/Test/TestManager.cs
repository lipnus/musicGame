using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		Debug.Log("테스트씬");
		
		//테스트용, 반드시 지울것
		Utils.resetPlayData();
		SceneManager.LoadSceneAsync("RiverScene");
//		PlayerPrefs.DeleteAll();
//		PlayerPrefs.SetString();
//		Utils.firstGift();
//		Utils.addMyItem(1818);
//		
//		Debug.Log("가지고있는가: " + Utils.isHaveItem(1818));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
