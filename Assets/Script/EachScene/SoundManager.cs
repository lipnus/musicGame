using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class SoundManager : MonoBehaviour {

    public List<AudioSource> noteSounds = new List<AudioSource>();
	public List<AudioSource> catSounds = new List<AudioSource>(); 
	public AudioSource jumpSound;
	public AudioSource correctSound;
	public AudioSource clickSound;
	public AudioSource okSound;
	public AudioSource backgroundMusic;

	
	

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
	
	//점프 후 착지소리
	public void jumpPlay() {
		jumpSound.GetComponent<AudioSource>().Play();
	}
	
	//띠리링~
	public void okPlay() {
		okSound.GetComponent<AudioSource>().Play();
	}
	


	//정답
	public void correctPlay() {
		correctSound.GetComponent<AudioSource>().Play();
	}
	
	//클릭
	public void clickPlay() {
		clickSound.GetComponent<AudioSource>().Play();
	}
	
	//배경음악
	public void backgroundMusicPlay() {
		backgroundMusic.GetComponent<AudioSource>().Play();		
	}
	
	//배경음악 위치
	public float getBackgroundMusicTime() {
		return backgroundMusic.time;
	}

	public void playBackgroundMusic() {
		
		if(Utils.backgroundMusicTime!=null){
			backgroundMusic.time = Utils.backgroundMusicTime;
			StartCoroutine(FadeIn(backgroundMusic, 4f));
		}
		else {
			backgroundMusic.Play();
		}
	}
	
	
	
	//사운드 페이드아웃
	public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime) {
		float startVolume = audioSource.volume;
		while (audioSource.volume > 0) {
			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
			yield return null;
		}
		audioSource.Stop();
	}
	
	
	//사운드 페이드인
	public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime) {
		audioSource.Play();
		audioSource.volume = 0.1f;
		while (audioSource.volume < 0.8f) {
			audioSource.volume += Time.deltaTime / FadeTime;
			yield return null;
		}
	}
}
