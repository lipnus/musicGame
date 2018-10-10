using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class A : MonoBehaviour {

    Text textObj;
    //Button button;

	// Use this for initialization
	void Start () {
        //button = (GameObject.Find("Button") as GameObject).GetComponent<Button>();
        textObj = (GameObject.Find("StateText") as GameObject).GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UPUP(){
        Debug.Log("까꿍");
    }

    public void Drag(){

        //스크린과의 거리(z)
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen);

        //button.transform.position = Camera.main.ScreenToWorldPoint( mousePosition );



        textObj.text = "Mouse: " + mousePosition.ToString() + "\nInput: " + Input.mousePosition;

        //==========================================
        //화면바깥으로 튀어나가지 않게
        //==========================================


    }



    private void OnTriggerEnter2D(Collider2D col){
        Debug.Log("충돌");       
    }




}
