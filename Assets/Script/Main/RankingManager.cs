using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour {

    public Text nicknameText;
    public Text rankingText;
    public Text correctText;
    public Text percentText;
    public Text gameClearText;
    public Text levelText;

    public List<GameObject> rankBox = new List<GameObject>();

    
    public ConnectServer connectServer;
    

    void Start() {
        setNicknameText();
        setCorrectText();
        setPercentText();
        setGameClearText();
        
        setUserRankingText(1);
        exhibit_setRankBox();
    }


    void setLevelText(int percent) {
        if (90 < percent) {
            levelText.text = "[" + "갓" + "]";
        }else if (80 < percent) {
            levelText.text = "[" + "음악 그 자체" + "]";
        }else if (70 < percent) {
            levelText.text = "[" + "고인물" + "]";
        }else if (60 < percent) {
            levelText.text = "[" + "반타작" + "]";
        }else if (50 < percent) {
            levelText.text = "[" + "심해" + "]";
        }else if (40 < percent) {
            levelText.text = "[" + "음악 쇄국정책" + "]";
        }else if (30 < percent) {
            levelText.text = "[" + "멜로디 알레르기" + "]";
        }else if (20 < percent) {
            levelText.text = "[" + "귀가 없음" + "]";
        }
        else {
            levelText.text = "[" + "자, 게임을 시작하지" +"]";

        }
    }
    
    
    //메인에서 아이콘을 누를때 호출
    public void initRank() {
        
        int correct = Utils.getCorrect();
        int wrong = Utils.getWrong();
        int gameClear = Utils.getGameClear();
        int score = 20 * gameClear + correct - wrong;
        
//        
//        connectServer.requestUserRank( score );
//        connectServer.requestAccumulateRanking( rankBox.Count );
        
    }

    //connectServer에서 호출
    public void setRankBox( List<Ranking> rankings ) {

        for (int i = 0; i < rankBox.Count; i++) {
            rankBox[i].transform.Find("nicknameText").GetComponent<Text>().text = rankings[i].nickname;
            rankBox[i].transform.Find("scoreText").GetComponent<Text>().text = rankings[i].score.ToString();

            
            if (2 < i) {
                rankBox[i].transform.Find("rankText").GetComponent<Text>().text = (i+1).ToString();
                rankBox[i].transform.Find("nicknameText").GetComponent<Text>().text = rankings[i].nickname;
                rankBox[i].transform.Find("scoreText").GetComponent<Text>().text = rankings[i].score.ToString();
            }
        }

    }
    
    
    //connectServer에서 호출
    public void setUserRankingText(int ranking) {
        rankingText.text = ranking + "등";
    }
    
    
    public void setNicknameText() {
        string nickname = Utils.getNickname();

        if (nickname != "empty_nickname") {
            nicknameText.text = nickname;
        }
        else {
            nicknameText.text = "???";
        }
    }
    

    public void setCorrectText() {
        correctText.text = "맞힌 문제: " + Utils.getCorrect();
    }
    

    public void setPercentText() {
        float correct = Utils.getCorrect();
        float wrong = Utils.getWrong();
        if (wrong == 0) wrong=1;
        
        Debug.Log("틀린거: " +wrong + " 맞은거: " + correct);
        
        double percent = correct / (correct+wrong) * 100;
        percent = Math.Floor(percent);
        percentText.text = "정답률: " + percent  + "%";
        
        setLevelText((int)percent);
    }
    

    public void setGameClearText() {
        gameClearText.text = "게임클리어: " + Utils.getGameClear();
    }
    
    
    
    
  

    //connectServer에서 호출
    public void exhibit_setRankBox() {

        rankBox[0].transform.Find("nicknameText").GetComponent<Text>().text = "여기";
        rankBox[0].transform.Find("scoreText").GetComponent<Text>().text = "100";
        
        rankBox[1].transform.Find("nicknameText").GetComponent<Text>().text = "와이파이가";
        rankBox[1].transform.Find("scoreText").GetComponent<Text>().text = "90";
        
        rankBox[2].transform.Find("nicknameText").GetComponent<Text>().text = "안돼요ㅜㅜ";
        rankBox[2].transform.Find("scoreText").GetComponent<Text>().text = "80";



        rankBox[3].transform.Find("rankText").GetComponent<Text>().text = "4";
        rankBox[3].transform.Find("nicknameText").GetComponent<Text>().text = "BTS";
        rankBox[3].transform.Find("scoreText").GetComponent<Text>().text = "70";
        
        rankBox[4].transform.Find("rankText").GetComponent<Text>().text = "5";
        rankBox[4].transform.Find("nicknameText").GetComponent<Text>().text = "TWICE";
        rankBox[4].transform.Find("scoreText").GetComponent<Text>().text = "60";
        
        rankBox[5].transform.Find("rankText").GetComponent<Text>().text = "6";
        rankBox[5].transform.Find("nicknameText").GetComponent<Text>().text = "타노스";
        rankBox[5].transform.Find("scoreText").GetComponent<Text>().text = "40";
        
        rankBox[6].transform.Find("rankText").GetComponent<Text>().text = "7";
        rankBox[6].transform.Find("nicknameText").GetComponent<Text>().text = "아이엠그루트";
        rankBox[6].transform.Find("scoreText").GetComponent<Text>().text = "30";
        
        rankBox[7].transform.Find("rankText").GetComponent<Text>().text = "8";
        rankBox[7].transform.Find("nicknameText").GetComponent<Text>().text = "태진아";
        rankBox[7].transform.Find("scoreText").GetComponent<Text>().text = "20";
        
        rankBox[8].transform.Find("rankText").GetComponent<Text>().text = "9";
        rankBox[8].transform.Find("nicknameText").GetComponent<Text>().text = "엄복동";
        rankBox[8].transform.Find("scoreText").GetComponent<Text>().text = "10"; 
        
        rankBox[9].transform.Find("rankText").GetComponent<Text>().text = "10";
        rankBox[9].transform.Find("nicknameText").GetComponent<Text>().text = "술한잔했습니다";
        rankBox[9].transform.Find("scoreText").GetComponent<Text>().text = "5"; 
         


    }
}
