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

    public void reset() {
        correct = 0;
        wrong = 0;
        clear = 0;
        point = 0;
    }
}