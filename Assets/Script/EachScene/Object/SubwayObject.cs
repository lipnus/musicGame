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
			Utils.changeScene("Stage 2", "Mountain", "청계산", "ForestScene" );
		}

	}
}
