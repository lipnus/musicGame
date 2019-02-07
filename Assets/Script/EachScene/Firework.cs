using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour {

	public int bombTime;
	
	private void OnTriggerEnter2D(Collider2D col){
		StartCoroutine(bomb(bombTime));
	}

	IEnumerator bomb(float delayTime) {
		GameObject.Find("SoundManager").GetComponent<SoundManager>().fireworkPlay1();
		yield return new WaitForSeconds(delayTime);
		GetComponent<Animator>().SetTrigger("bomb_t");
		GameObject.Find("SoundManager").GetComponent<SoundManager>().fireworkPlay2();
	}
}
