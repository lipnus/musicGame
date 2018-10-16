using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class SoundManager : MonoBehaviour {

    public List<AudioSource> noteSounds = new List<AudioSource>();
	public List<AudioSource> catSounds = new List<AudioSource>();
	

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
}
