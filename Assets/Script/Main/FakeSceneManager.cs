using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeSceneManager : MonoBehaviour {

	public List<GameObject> layer=new List<GameObject>();
	public List<float> layer_speed =new List<float>();
	float userSpeed;
	
	//배경스크롤
	void Update () {
		for (int i = 0; i < layer.Count; i++) {
			layer[i].transform.Translate(Vector3.right * (userSpeed-layer_speed[i]) * Time.deltaTime);
		}
	}

}
