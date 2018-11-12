using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeSceneManager : MonoBehaviour {

	public List<GameObject> layer=new List<GameObject>();
	public List<float> layer_speed =new List<float>();
	float userSpeed;
	public GameObject user;
	
	private static List<Vector3> positionHolder = new List<Vector3>(); //오브젝트들의 위치를 저장
	private static Vector3 userPosition;
	
	//배경스크롤
	void Update () {
		for (int i = 0; i < layer.Count; i++) {
			layer[i].transform.Translate(Vector3.right *(userSpeed-layer_speed[i]) * Time.deltaTime);
		}
	}

	void Start() {
		savePosition();
		StartCoroutine(backgroundLoop());
	}

	IEnumerator backgroundLoop() {
		yield return new WaitForSeconds(58f);
		loadPosition();
		StartCoroutine(backgroundLoop());
	}

	//모든 레이어의 위치를 전역에 저장
	public void savePosition() {
		
		//레이어위치 저장
		positionHolder.Clear();
		for (int i = 0; i < layer.Count; i++) {
			positionHolder.Add( layer[i].transform.position );
		}
		
		//유저위치 저장
		userPosition = user.transform.position;
	}
	
	//모든 레이어의 위치를 전역에서 불러옴
	public void loadPosition() {
		//레이어 복구
		for (int i = 0; i < layer.Count; i++) {
			Vector3 lPosition = positionHolder[i];
			layer[i].transform.position = lPosition;
		}
		
		//유저위치 복구
		Vector3 uPosition = userPosition;
		user.transform.position = uPosition;
	}

}
