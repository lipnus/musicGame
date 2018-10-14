using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventT : MonoBehaviour {

    Text text;

    // Use this for initialization
    void Start () {
        text = (GameObject.Find("StateText") as GameObject).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
		
	}

   
}
