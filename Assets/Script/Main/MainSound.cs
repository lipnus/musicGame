using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSound : MonoBehaviour {

	public AudioSource shutter;

	public void playShutter() {
		shutter.GetComponent<AudioSource>().Play();
	}
}
