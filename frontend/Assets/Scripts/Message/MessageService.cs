using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using Unity.VisualScripting;

public class MessageService : MonoBehaviour
{
    private readonly string baseURL = "https://unity-chatbot-8b1c8c5398d8.herokuapp.com/";

    public void CreateUserMessage(string content, string chatId, Action<Message> onSuccess, Action<string> onFailure)
    {
        MessagePayload payload = new MessagePayload(content, chatId);
        string jsonPayload = JsonUtility.ToJson(payload,true);
        StartCoroutine(SendMessageCoroutine($"{baseURL}user-message", jsonPayload, onSuccess, onFailure));
    }

    public void CreateChatbotMessage(Chat chat, Action<Message> onSuccess, Action<string> onFailure)
    {
        ChatPayload payload = new ChatPayload(chat);
        string jsonPayload = JsonUtility.ToJson(payload);
        StartCoroutine(SendMessageCoroutine($"{baseURL}chatbot-message", jsonPayload, onSuccess, onFailure));
    }

    public void GetMessages(Chat chat, Action<List<Message>> onSuccess, Action<string> onFailure)
    {
        ChatPayload payload = new ChatPayload(chat);
        string jsonPayload = JsonUtility.ToJson(payload);
        Debug.Log($"Payload: {jsonPayload}");
        StartCoroutine(GetMessagesCoroutine($"{baseURL}messages", jsonPayload, onSuccess, onFailure));
    }

    private IEnumerator SendMessageCoroutine(string url, string jsonPayload, Action<Message> onSuccess, Action<string> onFailure)
    {
        using (UnityWebRequest request = new UnityWebRequest(url, "POST")) 
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Message message = ProcessMessageResponse(request.downloadHandler.text);
                if (message != null) onSuccess?.Invoke(message);
                else onFailure?.Invoke("Failed to process message response.");
            }
            else
            {
                onFailure?.Invoke(request.error);
            }
        }
    }

    private IEnumerator GetMessagesCoroutine(string url, string jsonPayload, Action<List<Message>> onSuccess, Action<string> onFailure)
    {
        using (UnityWebRequest request = new UnityWebRequest(url)) 
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                List<Message> messages = ProcessMessagesResponse(request.downloadHandler.text);
                if (messages != null) onSuccess?.Invoke(messages);
                else onFailure?.Invoke("Failed to process messages response.");
            }
            else
            {
                onFailure?.Invoke(request.error);
            }
        }
    }

    private Message ProcessMessageResponse(string jsonResponse)
    {
        try
        {
            MessageWrapper messageWrapper = JsonUtility.FromJson<MessageWrapper>(jsonResponse);
            return messageWrapper.message;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error processing message response: " + ex.Message);
            return null;
        }
    }

    private List<Message> ProcessMessagesResponse(string jsonResponse)
    {
        try
        {
            MessagesWrapper messagesWrapper = JsonUtility.FromJson<MessagesWrapper>(jsonResponse);
            return messagesWrapper.messages;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error processing messages response: " + ex.Message);
            return new List<Message>();
        }
    }


   [System.Serializable]
    public class MessagePayload
    {
        public MessageDetail message;

        public MessagePayload(string content, string chatId)
        {
            this.message = new MessageDetail { content = content, chatId = chatId };
        }

        [System.Serializable]
        public class MessageDetail
        {
            public string content;
            public string chatId;
        }
    }

    [System.Serializable]
    public class ChatPayload
    {
        public Chat chat;
        public ChatPayload(Chat chat)
        {
            this.chat = chat;
        }
    }


    [System.Serializable]
    public class MessageWrapper
    {
        public Message message;
    }

    [System.Serializable]
    public class MessagesWrapper
    {
        public List<Message> messages;
    }
}
