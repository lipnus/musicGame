﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour {
    
    private List<Message> messageList = new List<Message>();
    public Image bulletCircle;
    public Text bulletText;
    public ConnectServer connectServer;

    public List<Text> dateTexts = new List<Text>();
    public List<Text> msgTexts = new List<Text>();



    //서버에서 메시지를 받아온다
    public void downloadMessage() {
        connectServer.requestMessage(3);
    }
    
    // conncectServer에서 여기를 호출하며 시작
    public void setMessage(List<Message> messageList) {
        this.messageList = messageList;
        
        //읽지 않은 메시지 개수 표시
        int count = messageList[0].message_order - Utils.getMessageLastCount();
//        Debug.Log("count: " + count);
        showBulletNum(count);
        
        //메시지 할당
        for (int i = 0; i < 3; i++) {
            dateTexts[i].text = messageList[i].date;
            msgTexts[i].text = messageList[i].message;
        }
    }

    
    //가장 마지막으로 읽은 메시지번호를 갱신
    public void updateLastReadNum() {
        if (messageList != null) {
            Utils.setMessageLastCount( messageList[0].message_order );
        }
    }

    
    private void showBulletNum(int count) {
        if (count < 1) {
            bulletCircle.enabled = false;
            bulletText.enabled = false;
            return;
        }
        
        bulletCircle.enabled = true;
        bulletText.enabled = true;
        bulletText.text = count.ToString();
        Debug.Log("showBulletNum");
    }

}
