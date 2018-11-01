using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class SoundManager : MonoBehaviour {

    public List<AudioSource> noteSounds = new List<AudioSource>();
	public List<AudioSource> catSounds = new List<AudioSource>();
	public AudioSource footSound;
	public AudioSource fireworkSound1;
	public AudioSource fireworkSound2;
	

    //임의의 멜로디
    public void notePlay(){
        int idx = Random.Range(0, noteSounds.Count-1);
        noteSounds[idx].GetComponent<AudioSource>().Play();
    }

	//임의의 고양이 울음소리
	public void catPlay() {
		int idx = Random.Range(0, catSounds.Count-1);
		catSounds[idx].GetComponent<AudioSource>().Play();
	}
	
	//발소리
	public void footPlay() {
		footSound.GetComponent<AudioSource>().Play();
	}
	
	//폭죽소리
	public void fireworkPlay1() {
		fireworkSound1.GetComponent<AudioSource>().Play();
	}
	
	public void fireworkPlay2() {
		fireworkSound2.GetComponent<AudioSource>().Play();
	}
}
