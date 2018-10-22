using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public static class GlobalScript {
//    public static int userPosition=0;
    public static Vector3 userPosition = new Vector3(0,0,0);
    public static string answerStr = "";

    
    //점수조작
    public static void modifyScore(int s) {
        int score = PlayerPrefs.GetInt("Score", 0) + s;
        PlayerPrefs.SetInt("Score", score);
        showScoreText();
    }
    
    //현재점수 표시
    public static void showScoreText() {
        int score = PlayerPrefs.GetInt("Score", 0);
        GameObject.Find("scoreText").GetComponent<Text>().text = score + "";
    }

}
