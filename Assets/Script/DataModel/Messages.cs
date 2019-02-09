
using System;
using System.Collections.Generic;

[Serializable]
public class Messages {
    public List<Message> messageList = new List<Message>();
}

[Serializable]
public class Message {
    public int message_order;
    public string message;
    public string date;
}
