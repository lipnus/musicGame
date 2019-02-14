using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager2 : MonoBehaviour {

	public List<AudioSource> sounds = new List<AudioSource>(); 
	

	public void playSound(int index) {
		sounds[index].Play();
	}
	
	
}
