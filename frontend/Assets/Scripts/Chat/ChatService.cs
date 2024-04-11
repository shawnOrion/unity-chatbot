
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class ChatService : MonoBehaviour
{
    private string baseURL = "https://unity-chatbot-8b1c8c5398d8.herokuapp.com/";

    public void CreateChat(string userId, Action<Chat> onSuccess, Action<string> onFailure)
    {
        StartCoroutine(CreateChatCoroutine(userId, onSuccess, onFailure));
    }

    public void GetChats(string userId, Action<List<Chat>> onSuccess, Action<string> onFailure)
    {
        StartCoroutine(GetChatsCoroutine(userId, onSuccess, onFailure));
    }

    private IEnumerator CreateChatCoroutine(string userId, Action<Chat> onSuccess, Action<string> onFailure)
    {
        string requestUrl = baseURL + "chat/" + userId;
        UnityWebRequest request = new UnityWebRequest(requestUrl, "POST");
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            onFailure?.Invoke(request.error);
        }
        else
        {
            try
            {
                Chat chat = ProcessChatResponse(request.downloadHandler.text);
                if (chat != null) onSuccess?.Invoke(chat);
                else onFailure?.Invoke("Failed to process chat response.");
            }
            catch (Exception ex)
            {
                onFailure?.Invoke("Error processing chat response: " + ex.Message);
            }
        }
    }

    private IEnumerator GetChatsCoroutine(string userId, Action<List<Chat>> onSuccess, Action<string> onFailure)
    {
        string requestUrl = baseURL + "chats/" + userId;
        UnityWebRequest request = UnityWebRequest.Get(requestUrl);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            onFailure?.Invoke(request.error);
        }
        else
        {
            try
            {
                List<Chat> chats = ProcessChatsResponse(request.downloadHandler.text);
                if (chats != null) onSuccess?.Invoke(chats);
                else onFailure?.Invoke("Failed to process chats response.");
            }
            catch (Exception ex)
            {
                onFailure?.Invoke("Error processing chats response: " + ex.Message);
            }
        }
    }

    private Chat ProcessChatResponse(string responseText)
    {
        try
        {
            ChatWrapper wrapper = JsonUtility.FromJson<ChatWrapper>(responseText);
            return wrapper.chat;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error processing chat response: " + ex.Message);
            return null;
        }
    }

    private List<Chat> ProcessChatsResponse(string responseText)
    {
        try
        {
            ChatsWrapper wrapper = JsonUtility.FromJson<ChatsWrapper>(responseText);
            return wrapper.chats;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error processing chats response: " + ex.Message);
            return new List<Chat>();
        }
    }

    [System.Serializable]
    public class ChatWrapper
    {
        public Chat chat;
    }

    [System.Serializable]
    public class ChatsWrapper
    {
        public List<Chat> chats;
        public ChatsWrapper()
        {
            chats = new List<Chat>();
        }
    }
}
