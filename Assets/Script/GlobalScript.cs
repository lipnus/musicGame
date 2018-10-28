using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;


public static class GlobalScript {
    
    //서버경로정보
    public static string serverPath = "http://ec2-13-125-247-189.ap-northeast-2.compute.amazonaws.com:9000/dduroon";
    public static string musicPath = "http://ec2-13-125-247-189.ap-northeast-2.compute.amazonaws.com/music";

    
    //퀴즈 풀고 필드로 돌아왔을때
    //public static int userPosition=0;
    public static Vector3 userPosition = new Vector3(0,0,0); //위치 기억
    public static string answerStr = ""; //정답표시
    public static bool isAnswer; //맞았는지 확인

    
    //점수조작
    public static void modifyScore(int s) {
        int score = PlayerPrefs.GetInt("Score", 0) + s;
        PlayerPrefs.SetInt("Score", score);
        showScoreText();
    }

    
    //목숨초기화
    public static void setLife(int life) {
        PlayerPrefs.SetInt("Life", life);
    }
    
    //목숨수정
    public static void modifyLife(int life) {
        int lf = PlayerPrefs.GetInt("Life", 0) + life;
        PlayerPrefs.SetInt("Life", lf);
        showScoreText();
    }
  
    //현재점수 표시
    public static void showScoreText() {
        int score = PlayerPrefs.GetInt("Score", 0);
        GameObject.Find("scoreText").GetComponent<Text>().text = score + "";
    }
    
    
    //음악재생시간
    public static float getPlayTime() {
        return PlayerPrefs.GetFloat("PlayTime", 1f); //기본값:2
    }
}
