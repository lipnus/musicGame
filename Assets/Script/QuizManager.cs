using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour {

    

	static string answer="ㅅㅂㄹㅁ"; //정답(서버에서 받아온다)
	public Image[] answerBoxImg; //제출된 정답
	public Text[] initialTexts; //정답입력칸
	private int initialTextSize; //보기로 주어진 자음의 개수

	
    // Use this for initialization
    void Start () {
	    
	    //자음은 답안길이 +2개
	    initialTextSize = answer.Length + 2;
	    
	    //자음텍스트 객체들을 배열에 저장
	    initialTexts = new Text[initialTextSize];
	    for (int i = 0; i < initialTextSize; i++){
		    initialTexts[i] = GameObject.Find("initText_Group").transform.Find("initText"+i).GetComponent<Text>();
		    initialTexts[i].gameObject.SetActive(true);
		    initialTexts[i].text = FakeText(); //임의의 자음 할당
	    }
	    
	    //임의의 자음텍스트중 답안 개수만큼 중복되지 않게 추출
	    int pickCount = 0;
	    List<int> picked = new List<int>();
	    
	    while (pickCount < answer.Length){
		    int pick = Random.Range(0, initialTextSize - 1);

		    bool overLap = false;
		    for (int i = 0; i < picked.Count; i++){
			    if (pick == picked[i]) overLap = true;
		    }

		    if (!overLap){
			    pickCount++;
			    picked.Add(pick);
		    }
	    }

	    //추출한 위치에 정답 자음 삽입
	    for (int i = 0; i < picked.Count; i++){
		    initialTexts[picked[i]].text = answer[i].ToString();
	    }
	    
	    //답안박스 객체들을 배열에 저장
	    answerBoxImg = new Image[answer.Length];
	    for (int i = 0; i < answer.Length; i++){
		    answerBoxImg[i] = GameObject.Find("answerBox_Group").transform.Find("answerBox"+i).GetComponent<Image>();
		    answerBoxImg[i].gameObject.SetActive(true);
	    }

	  
	    
	    //서버에서 데이터를 받아와서 할당해주는 부분
	    //answer, answerLength 할당
	    //initialText에 답과 오답 섞어서 뿌려주기
	    
	    //1초에 한번씩 답을 체크하는 코루틴
	    StartCoroutine("AnswerCheck", 1);
	}
	
	
	
	IEnumerator AnswerCheck(float delayTime) { 
		yield return new WaitForSeconds(delayTime);
		
		//칸을 다 채웠는지 확인
		bool completeSubmit = true;
		for (int i = 0; i < answer.Length; i++){
			if (answerBoxImg[i].GetComponent<AnswerBox>().mText == null){
				completeSubmit = false; 
			}
		}

		//칸을 다 채운경우, 정답이 맞는지 확인
		if (completeSubmit){
			
			bool result = true;
			for (int i = 0; i < answer.Length; i++){
				if ( answerBoxImg[i].GetComponent<AnswerBox>().mText.text != answer[i].ToString() ){
					Debug.Log(answerBoxImg[i].GetComponent<AnswerBox>().mText.text + " / " + answer[i]);
					result = false;
				}
			}
			Debug.Log("결과: " + result);
		}
		
		StartCoroutine("AnswerCheck", 1);
	}



	//임의의 자음 하나를 리턴
	string FakeText(){
		string consonant = "ㄱㄴㄷㄹㅁㅂㅅㅇㅈㅊㅋㅌㅍㅎㄲㅆ"; //17개
		int lotto = Random.Range(0, consonant.Length-1);
		return consonant[lotto].ToString();
	}
}
