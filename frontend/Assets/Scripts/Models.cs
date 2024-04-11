using System.Collections.Generic;
[System.Serializable]
public class Message
{
    public string _id;
    public string role;
    public string content;
}

[System.Serializable]
public class Messages
{
    public List<Message> messages; 
    public Messages(){
        messages = new List<Message>();
    }
}

[System.Serializable]
public class Chat
{
    public List<string> messages; 
    public string title;
    public string _id;
    public string userId;
    public Chat()
    {
        messages = new List<string>();
    }
}

[System.Serializable]
public class User
{
    public string _id;
    public string email = null;
    public List<string> chats; //ids of chats
    public User()
    {
        chats = new List<string>();
    }
}
