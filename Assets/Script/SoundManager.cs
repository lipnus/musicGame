using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class SoundManager : MonoBehaviour {

    public List<AudioSource> noteSoaunds = new List<AudioSource>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    //임의의 멜로디
    public void notePlay(){
        int noteIdx = Random.Range(0, noteSoaunds.Count-1);
        noteSoaunds[noteIdx].GetComponent<AudioSource>().Play();
    }
}
