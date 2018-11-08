﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;


public static class GlobalScript {
    
    //서버경로정보
    public static string serverPath = "http://ec2-13-125-247-189.ap-northeast-2.compute.amazonaws.com:9000/dduroon";
    public static string musicPath = "http://ec2-13-125-247-189.ap-northeast-2.compute.amazonaws.com/music";

    
    //퀴즈 <-> 필드 사이의 데이터 전달을 위한 변수들
    //public static int userPosition=0;
    public static Vector3 userPosition = new Vector3(0,0,0); //위치 기억
    public static string answerStr = ""; //정답표시
    public static int lifeEvent; // -1:오답 , 0:정답 1:보너스
    public static string sceneName;
    public static List<Vector3> positionHolder = new List<Vector3>(); //오브젝트들의 위치를 저장
    
    

    //==========================================================
    // 게임진행과 관련된 함수
    //==========================================================
    
    //위치 초기화(스테이지 이동할 떼 호출)
    public static void resetStage() {
        answerStr = "";
        userPosition = new Vector3(0,0,0);
    }
    
    //게임초기화
    public static void startGame() {
        setLife(1);
        lifeEvent = 0;
        userPosition = new Vector3(0,0,0);
    }
    
    //부활(리워드 광고 보상)
    public static void responeGame() {
        setLife(2);
        lifeEvent = 0;
    }
    
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
    
    //목숨값받기
    public static int getLife() {
        return PlayerPrefs.GetInt("Life", 1);
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
    
    
    //==========================================================
    // 아이템관련함수
    //==========================================================

    //소유목록에 새 아이템 추가
    public static void addItem(int code) {
        MyItem curItem = new MyItem();
        string jsonStr = PlayerPrefs.GetString("my_item");
        
        curItem = JsonUtility.FromJson<MyItem>(jsonStr);
        curItem.owns.Add(code);
        jsonStr = JsonUtility.ToJson(curItem);
        
        setMyItems(jsonStr);
    }
    
    //옷을 가지고 있는지 확인
    public static bool isHaveItem(int code) {
        MyItem curItem = new MyItem();
        string jsonStr = PlayerPrefs.GetString("my_item");
        
        Debug.Log("뽑은텍스트: " + jsonStr);
        curItem = JsonUtility.FromJson<MyItem>(jsonStr);

        bool isHave = false;
        for (int i = 0; i < curItem.owns.Count(); i++) {
            if (code == curItem.owns[i]) isHave = true;
        }
        return isHave;
    }
    
    //소유목록 업데이트
    public static void setMyItems(string item_list) {
        PlayerPrefs.SetString("my_item", item_list);
    }
    
    

    //옷입히기
    public static void setTop1(int code) {
        PlayerPrefs.SetInt("wear_top1", code);
    }
    
    public static void setTop2(int code) {
        PlayerPrefs.SetInt("wear_top2", code);
    }
    
    public static void setBottom(int code) {
        PlayerPrefs.SetInt("wear_bottom", code);
    }
    
    public static void setShoes(int code) {
        PlayerPrefs.SetInt("wear_shoes", code);
    }
    
    
    
    
    //무슨 옷 입고있는지 확인
    public static void getTop1(int code) {
        PlayerPrefs.GetInt("wear_top1", 0);
    }
    
    public static void getTop2(int code) {
        PlayerPrefs.GetInt("wear_top2", 0);        
    }
    
    public static void getBottom(int code) {
        PlayerPrefs.GetInt("wear_bottom", 0);
    }
    
    public static void getShoes(int code) {
        PlayerPrefs.GetInt("wear_shoes", 0);
    }

    //아이템정보는 여기다가 입력하고 여기서 조회한다
    public static Item getItemInfo(int code) {
        
        List<Item> items = new List<Item>();

        Item item;
        
        //상의1
        item = new Item(0, 100, "유니클로 무지반팔", 12000, "전체보기>상의>반팔티셔츠");
        items.Add(item);
        
        //상의2
        item = new Item(0, 200, "유니클로 무지반팔", 12000, "전체보기>상의>반팔티셔츠");
        items.Add(item);
        
        //하의
        item = new Item(0, 300, "지오다노 슬랙스", 12000, "전체보기>상의>");
        items.Add(item);
        
        //신발
        item = new Item(0, 400, "락포트 페니로퍼", 12000, "전체보기>신발>구두");
        items.Add(item);


        Item returnObj = new Item(0, 100, "투명한 옷", 10, "전체보기>상의>투명망토");
        for (int i = 0; i < items.Count(); i++) {
            if (code == items[i].Code) returnObj = items[i];
        }

        return returnObj;

    }


    //게임 최초실행 시 기본템들을 나눠준다
    public static void firstGift() {
        int isFirstStart = PlayerPrefs.GetInt("firstGift_b", 0);
        
        //안받은경우
        if (isFirstStart == 0) {
            
            //입혀준다
            setTop1(100);
            setTop2(200);
            setBottom(300);
            setShoes(400);
        
            //기본 소유목록작성
            MyItem mItem = new MyItem();
            mItem.owns.Add(100);
            mItem.owns.Add(200);
            mItem.owns.Add(300);
            mItem.owns.Add(400);
        
            //소유목록 초기화
            string jsonStr = JsonUtility.ToJson(mItem);
            setMyItems(jsonStr);   
        }
    }
}
