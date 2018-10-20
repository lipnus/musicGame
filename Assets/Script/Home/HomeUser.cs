using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeUser : MonoBehaviour {

	public GameObject threeCeilingLight_off;
	public GameObject threeCeilingLight_on;
	public GameObject ceilingLight_off;
	public GameObject ceilingLight_on;

	public GameObject dark2;
	public GameObject dark3;
	public GameObject dark4;

	public GameObject door_close;
	public GameObject door_open;

	public AudioSource switchSound;
	public AudioSource doorSound;
	public AudioSource clothSound;
	
	
	private void OnTriggerEnter2D(Collider2D col) {
		
		//3개짜리 전구 앞
		if (col.name.Equals("Switch1")) {
			threeCeilingLight_off.SetActive(false);
			threeCeilingLight_on.SetActive(true);
			dark2.SetActive(false);
			dark3.SetActive(true);
			switchSound.Play();
			
		}else if (col.name.Equals("Switch2")) {
			
		}else if (col.name.Equals("Switch3")) {
			ceilingLight_off.SetActive(false);
			ceilingLight_on.SetActive(true);
			dark3.SetActive(false);
			dark4.SetActive(true);
			switchSound.Play();

			
		}else if (col.name.Equals("Switch4")) {
			door_close.SetActive(false);
			door_open.SetActive(true);
			doorSound.Play();
			
		}
	}
}
