using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour {

	public AudioSource audioSource;
	
	// Use this for initialization
	void Start () {
		
		Debug.Log("테스트씬");
		audioSource.time = 10f;
		audioSource.Play();



//		테스트용, 반드시 지울것
		PlayerPrefs.DeleteAll();
//		Utils.resetPlayData();
//		SceneManager.LoadSceneAsync("RiverScene");
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
