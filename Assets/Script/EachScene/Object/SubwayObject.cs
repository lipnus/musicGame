using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubwayObject : MonoBehaviour {


	
	public void startSubway()
	{
		GameObject.Find("fadeEffect").GetComponent<FadeEffect>().FadeOut(3f); //페이드아웃
		StartCoroutine(moveNextStage(5f));//2초후 이동
	}

	IEnumerator moveNextStage(float delayTime) {
		yield return new WaitForSeconds(delayTime);
		
		//스테이지 이동
		if (Application.loadedLevelName.Equals("CityScene")) {
			Utils.changeScene( "Stage 2", "Seoul Forest Park", "서울숲 공원", "ParkScene" );
		}
		
		if (Application.loadedLevelName.Equals("ParkScene")) {
			Utils.changeScene( "Stage 3", "Mountain", "청계산", "ForestScene" );
		}
		
		if (Application.loadedLevelName.Equals("ForestScene")) {
			Utils.changeScene( "Final Stage", "River", "여의도 한강공원", "RiverScene" );
		}
		
		if (Application.loadedLevelName.Equals("RiverScene")) {
			Utils.resetStage();
			SceneManager.LoadSceneAsync("EndingScene");
		}

	}
}
