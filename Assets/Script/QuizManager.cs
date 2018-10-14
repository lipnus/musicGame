using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour {

	//자음을 담고있을 텍스트(정답 포함 5개)
	public int initialTextSize = 5;
    public List<Text> initialTexts = new List<Text>();

	public int answerLength = 4;
	public string answer="0123";    //정답(서버에서 받아온다)
	public Image[] answerBoxImg; //제출된 정답
 

	
    // Use this for initialization
    void Start () {
	    
	    Debug.Log(answer + ", " + answer[0]);
	    
	    //자음텍스트 객체들을 배열에 저장
	    for (int i = 0; i < initialTextSize; i++){
		    initialTexts.Add(GameObject.Find( "initText"+i ).GetComponent<Text>());
		    initialTexts[i].text = ""+ i;
	    }
	    
	    //답안박스 객체들을 배열에 저장
	    answerBoxImg = new Image[answerLength];
	    for (int i = 0; i < answerLength; i++){
		    answerBoxImg[i] = GameObject.Find("answerBox" + i).GetComponent<Image>();
	    }
	    
	    
	    //서버에서 데이터를 받아와서 할당해주는 부분
		
	    //1초에 한번씩 답을 체크하는 코루틴
	    StartCoroutine("AnswerCheck", 1);
	}
	
	
	
	IEnumerator AnswerCheck(float delayTime) { 
		yield return new WaitForSeconds(delayTime);
		
		//칸을 다 채웠는지 확인
		bool completeSubmit = true;
		for (int i = 0; i < answerLength; i++){
			if (answerBoxImg[i].GetComponent<AnswerBox>().mAnswer == ""){
				completeSubmit = false; 
			}
		}

		//칸을 다 채운경우, 정답이 맞는지 확인
		if (completeSubmit){
			
			bool result = true;
			for (int i = 0; i < answerLength; i++)
			{
				if (!answerBoxImg[i].GetComponent<AnswerBox>().mAnswer.Equals(answer[i]))
				{
					Debug.Log(answerBoxImg[i].GetComponent<AnswerBox>().mAnswer + " / " + answer[i]);
					result = false;
				}
			}
			
			Debug.Log("결과: " + result);
		}
		

		
		
		StartCoroutine("AnswerCheck", 1);
	}

 


	

	// Update is called once per frame
	void Update () { 

		
	}
}
