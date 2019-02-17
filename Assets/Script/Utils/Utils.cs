using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public static class Utils {
    


    
    //퀴즈 <-> 필드 사이의 데이터 전달을 위한 변수들
    //public static int userPosition=0;
    public static Vector3 userPosition = new Vector3(0,0,0); //위치 기억
    public static int lifeEvent; // -1:오답, 0:정답, 1:보너스
    public static string sceneName;
    
    public static List<Vector3> positionHolder = new List<Vector3>(); //오브젝트들의 위치를 저장
    
    private static string answer_title="";
    private static string answer_singer="";

    public static int difficulty; //난이도
    
    
    
    
    //==========================================================
    // 씬 이동
    //==========================================================
    
    private static string stageStr;
    private static string titleStr;
    private static string subTitleStr;
    private static string sceneNameStr;

    public static void changeScene(string stage, string title, string subtitle, string sceneName) {
        stageStr = stage;
        titleStr = title;
        subTitleStr = subtitle;
        sceneNameStr = sceneName;
        
        resetStage();
        SceneManager.LoadSceneAsync("BetweenScene");
    }

    public static string getStageStr() { return stageStr; }
    public static string getTitleStr() { return titleStr; }
    public static string getsubTitleStr() { return subTitleStr; }
    public static string getsceneNameStr() { return sceneNameStr; }
    
    
    
    
    
    

    //==========================================================
    // 게임진행과 관련된 함수
    //==========================================================
    
    public static PlayData playData = new PlayData();

    public static void resetPlayData() {
        playData.reset();
    }
    
    
    //최초실행 시 서버와의 동기화 여부
    public static void setSyncServer(int sync) {
        PlayerPrefs.SetInt("SyncServer", sync);
    }

    public static int getSyncServer() {
        return PlayerPrefs.GetInt("SyncServer", 0);  
    }
    
    
    
    //위치 초기화(스테이지 이동할 떼 호출)
    public static void resetStage() {
        userPosition = new Vector3(0,0,0);
    }
    
    //게임초기화
    public static void startGame() {
        resetPlayData();
        setLife(3);
        lifeEvent = 0;
        userPosition = new Vector3(0,0,0);
    }
    
    //부활(리워드 광고 보상)
    public static void responeGame() {
        setLife(2);
        lifeEvent = 0;
    }


    //User의 primary key
    public static void setUserPk(int user_pk) {
        PlayerPrefs.SetInt("UserPk", user_pk);
    }

    public static int getUserPk() {
        return PlayerPrefs.GetInt("UserPk", 0);  
    }
    
    
    
    //읽은 미시지 개수
    public static void setMessageLastCount(int messageCount) {
        PlayerPrefs.SetInt("MessageLastCount", messageCount);
    }

    public static int getMessageLastCount() {
        return PlayerPrefs.GetInt("MessageLastCount", 0);
    }
    
    
    
    //게임클리어 횟수
    public static void modifyGameClear(int g) {
        int gameClear = PlayerPrefs.GetInt("GameClear", 0) + g;
        PlayerPrefs.SetInt("GameClear", gameClear);

        playData.clear += g;
    }
    
    public static void setGameClear(int gameClear) {
        PlayerPrefs.SetInt("GameClear", gameClear);
    }

    public static int getGameClear() {
        return PlayerPrefs.GetInt("GameClear", 0);
    }
    
    
    
    //맞은 개수
    public static void modifyCorrect(int c) {
        int correct = PlayerPrefs.GetInt("Correct", 0) + c;
        PlayerPrefs.SetInt("Correct", correct);

        playData.correct += c;
    }

    public static void setCorrect(int correct) {
        PlayerPrefs.SetInt("Correct", correct);
    }

    public static int getCorrect() {
        return PlayerPrefs.GetInt("Correct", 0);
    }
    
    
    
    //틀린개수
    public static void modifyWrong(int w) {
        int wrong = PlayerPrefs.GetInt("Wrong", 0) + w;
        PlayerPrefs.SetInt("Wrong", wrong);

        playData.wrong += w;
    }

    public static void setWrong(int wrong) {
        PlayerPrefs.SetInt("Wrong", wrong);
    }

    public static int getWrong() {
        return PlayerPrefs.GetInt("Wrong", 0);
    }
    
    
    
    //점수조작(더하거나 뺌)
    public static void modifyScore(int p) {
        int point = PlayerPrefs.GetInt("Point", 0) + p;
        PlayerPrefs.SetInt("Point", point);

        playData.point += p;
    }

    public static void setPoint(int point) {
        PlayerPrefs.SetInt("Point", point);

    }

    public static int getPoint() {
        return PlayerPrefs.GetInt("Point", 0);
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

    
    //답 임시저장
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
        float playTime = 1f;
        if (Utils.isHaveItem(400)) playTime += 1f;
        if (Utils.isHaveItem(401)) playTime += 1.5f;
        return playTime;
    }
    
    //가이드를 봤는지 여부
    public static bool isGuide_Finished() {
        int result = PlayerPrefs.GetInt("GuideEnd", 0);
        
        if (result == 0) return false;
        else return true;
    }

    //가이드를 끝낸다
    public static void endGuide() {
        PlayerPrefs.SetInt("GuideEnd", 1);
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
    
    //옷을 입고있는지 확인(악세사리는 구매만 해도 입고 있는 걸로 간주
    public static bool isWearItem(int code) {
        if (code == getTop() || code == getBottom() || code == getShoes() || code/100==4) {
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
    
    //소유목록 불러오기
    public static string getMyItems() {
        return PlayerPrefs.GetString("my_item", "empty");
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
    
    

    
    
    //==========================================================
    // 아이템정보
    //==========================================================
    
    //아이템정보를 생성하고 조회한다(사실상 DB역할)
    public static ItemInfo getItemInfo(int code) {
        
        List<ItemInfo> items = new List<ItemInfo>();

        ItemInfo item;
        
        //상의
        item = new ItemInfo(0, 100, "유니클로 무지반팔", 8000, "전체보기 > 상의 > 반팔티셔츠");
        items.Add(item);
        item = new ItemInfo(0, 101, "아크네 맨투맨", 10, "전체보기 > 상의 > 맨투맨");
        items.Add(item);
                
        //하의
        item = new ItemInfo(0, 200, "지오다노 슬랙스", 12000, "전체보기 > 하의 > 슬랙스");
        items.Add(item);
        item = new ItemInfo(0, 201, "파타고니아 반바지", 25000, "전체보기 > 하의 > 반바지");
        items.Add(item);
        
        
        //신발
        item = new ItemInfo(0, 300, "락포트 페니로퍼", 45000, "전체보기 > 신발 > 구두");
        items.Add(item);
        item = new ItemInfo(0, 301, "꼼데 스니커즈", 10, "전체보기 > 신발 > 스니커즈");
        items.Add(item);
        item = new ItemInfo(0, 302, "이지 부스트", 10, "전체보기 > 신발 > 운동화");
        items.Add(item);
        
        //패시브아이템
        item = new ItemInfo(1, 400, "샤넬 No.5 (+1초)", 1000, "#듣는시간을 1초 증가");
        items.Add(item);
        item = new ItemInfo(1, 401, "AirPod (+1.5초)", 1500, "#듣는시간을 1.5초 증가");
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
    
    
    
    
    //==========================================================
    // 유저정보 Upload & download 에 사용
    //==========================================================

    //기기의 uuid 반환
    public static string getUUID() {
        return SystemInfo.deviceUniqueIdentifier;
    }
    
    //플레이게임 아이디저장
    public static void setPlayGameId(string id) {
        PlayerPrefs.SetString("playgame_id", id);
    }

    public static string getPlayGameId() {
        return PlayerPrefs.GetString("playgame_id", "empty_paygame_id");
    }
    
    //닉네임 설정
    public static void setNickname(string nickname) {
        PlayerPrefs.SetString("nickname", nickname);
    }
    
    //닉네임 가져오기
    public static string getNickname() {
        return PlayerPrefs.GetString("nickname", "empty_nickname");
    }

       
    //클라이언트의 유저정보를 반환
    public static UserInfo getUserInfo() {
        
        UserInfo userInfo = new UserInfo();

        userInfo.playgame_id = getPlayGameId();
        userInfo.nickname = getNickname();
        userInfo.point = getPoint();
        userInfo.item = getMyItems();
        userInfo.wear_top = getTop();
        userInfo.wear_bottom = getBottom();
        userInfo.wear_shoes = getShoes();
        userInfo.correct = getCorrect();
        userInfo.wrong = getWrong();
        userInfo.game_clear = getGameClear();

        return userInfo;
    }
    

    //서버에서 받아온 유저정보를 클라이언트 게임에 반영
    public static void updateUserInfo(UserInfo userInfo) {
        setUserPk( userInfo.user_pk );
        setNickname( userInfo.nickname );
        setPoint( userInfo.point );
        setMyItems( userInfo.item );
        setTop( userInfo.wear_top );
        setBottom( userInfo.wear_bottom );
        setShoes( userInfo.wear_shoes );
        setCorrect( userInfo.correct );
        setWrong( userInfo.wrong );
        setGameClear( userInfo.game_clear );
    }
}
