using System;
 
 /**
  * 시작부터 죽을때까지의 기록
  * 시작할때마가 초기화됨
  */
 
 [Serializable]
 public class PlayData {
     public int correct;
     public int wrong;
     public int clear;
     public int point;
     public int ad;
     public bool isRivival; //부활을 한번 했는지
     public int bossLife; //보스고양이 목숨
 }