using System.Collections;
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
    public static int lifeEvent; // -1:오답 , 0:정답 1:보너스
    public static string sceneName;
    public static List<Vector3> positionHolder = new List<Vector3>(); //오브젝트들의 위치를 저장
    
    private static string answer_title="";
    private static string answer_singer="";
    
    

    //==========================================================
    // 게임진행과 관련된 함수
    //==========================================================
    
    //위치 초기화(스테이지 이동할 떼 호출)
    public static void resetStage() {
        userPosition = new Vector3(0,0,0);
    }
    
    //게임초기화
    public static void startGame() {
        setLife(3);
        lifeEvent = 0;
        userPosition = new Vector3(0,0,0);
    }
    
    //부활(리워드 광고 보상)
    public static void responeGame() {
        setLife(2);
        lifeEvent = 0;
    }
    
    //점수조작(더하거나 뺌)
    public static void modifyScore(int s) {
        int score = PlayerPrefs.GetInt("Score", 0) + s;
        PlayerPrefs.SetInt("Score", score);
    }

    public static void setScore(int score) {
        PlayerPrefs.SetInt("Score", score);

    }

    public static int getScore() {
        return PlayerPrefs.GetInt("Score", 0);
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
    }


    public static void setAnswer(string title, string singer) {
        answer_title = title;
        answer_singer = singer;
    }

    public static string getAnswerTitle() {
        return answer_title;
    }

    public static string getAnswerSinger() {
        return answer_singer;
    }
    
    //음악재생시간
    public static float getPlayTime() {
        return PlayerPrefs.GetFloat("PlayTime", 4f); //기본값
    }
    
    //가이드를 봤는지 여부
    public static bool isGuide_Finished() {
        int result = PlayerPrefs.GetInt("Guide", 0);
        
        if (result == 0) return false;
        else return true;
    }

    //가이드를 끝낸다
    public static void endGuide() {
        PlayerPrefs.SetInt("Guide", 1);
    }

    public static bool soundOn() {
        return true;
    }

    
    
    
    //==========================================================
    // 아이템관련함수
    //==========================================================

    //소유목록에 새 아이템 추가
    public static void addMyItem(int code) {
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
        
        curItem = JsonUtility.FromJson<MyItem>(jsonStr);

        bool isHave = false;
        for (int i = 0; i < curItem.owns.Count(); i++) {
            if (code == curItem.owns[i]) isHave = true;
        }
        return isHave;
    }
    
    //옷을 입고있는지 확인
    public static bool isWearItem(int code) {
        if (code == getTop() || code == getBottom() || code == getShoes()) {
            return true;
        }
        else {
            return false;
        }
        
    }
    
    //소유목록 업데이트
    public static void setMyItems(string item_list) {
        PlayerPrefs.SetString("my_item", item_list);
    }
    
    

    //옷입히기
    public static void setTop(int code) {
        PlayerPrefs.SetInt("wear_top", code);
    }
    
    public static void setBottom(int code) {
        PlayerPrefs.SetInt("wear_bottom", code);
    }
    
    public static void setShoes(int code) {
        PlayerPrefs.SetInt("wear_shoes", code);
    }
    
    
    
    //무슨 옷 입고있는지 확인
    public static int getTop() {
        return PlayerPrefs.GetInt("wear_top", 0);
    }
    
    public static int getBottom() {
        return PlayerPrefs.GetInt("wear_bottom", 0);
    }
    
    public static int getShoes() {
        return PlayerPrefs.GetInt("wear_shoes", 0);
    }

    
    
    
    //아이템정보를 생성하고 조회한다(사실상 DB역할)
    public static ItemInfo getItemInfo(int code) {
        
        List<ItemInfo> items = new List<ItemInfo>();

        ItemInfo item;
        
        //상의
        item = new ItemInfo(0, 100, "유니클로 무지반팔", 12000, "전체보기 > 상의 > 반팔티셔츠");
        items.Add(item);
        item = new ItemInfo(0, 101, "아크네 맨투맨", 1000, "전체보기 > 상의 > 맨투맨");
        items.Add(item);
                
        //하의
        item = new ItemInfo(0, 200, "지오다노 슬랙스", 35000, "전체보기 > 하의 > 슬랙스");
        items.Add(item);
        
        //신발
        item = new ItemInfo(0, 300, "락포트 페니로퍼", 50000, "전체보기 > 신발 > 구두");
        items.Add(item);
        item = new ItemInfo(0, 301, "꼼데 스니커즈", 1000, "전체보기 > 신발 > 스니커즈");
        items.Add(item);
        item = new ItemInfo(0, 302, "이지 부스트", 1000, "전체보기 > 신발 > 운동화");
        items.Add(item);


        ItemInfo returnObj = new ItemInfo(0, 0, "투명한 옷", 10, "전체보기 > 상의 > 투명망토");
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
            setTop(100);
            setBottom(200);
            setShoes(300);
        
            //기본 소유목록작성
            MyItem mItem = new MyItem();
            mItem.owns.Add(100);
            mItem.owns.Add(200);
            mItem.owns.Add(300);
        
            //소유목록 초기화
            string jsonStr = JsonUtility.ToJson(mItem);
            setMyItems(jsonStr);   
        }
    }
}
