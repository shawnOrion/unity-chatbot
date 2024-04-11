using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class UserService : MonoBehaviour
{
    private readonly string baseURL = "https://unity-chatbot-8b1c8c5398d8.herokuapp.com/";

    // Method to create a user
    public void CreateUser(string email, Action<User> onSuccess, Action<string> onFailure)
    {
        UserPayload payload = new UserPayload(email);
        string jsonPayload = JsonUtility.ToJson(payload);
        Debug.Log("Attempting to create user with payload: " + jsonPayload);
        StartCoroutine(SendRequest($"{baseURL}user", "POST", jsonPayload, onSuccess, onFailure));
    }

    // Method to retrieve a user
    public void GetUser(string email, Action<User> onSuccess, Action<string> onFailure)
    {
        UserPayload payload = new UserPayload(email);
        string jsonPayload = JsonUtility.ToJson(payload);
        StartCoroutine(SendRequest($"{baseURL}user", "GET", jsonPayload, onSuccess, onFailure));
    }

    // Generalized coroutine for sending web requests
    private IEnumerator SendRequest(string url, string method, string jsonPayload, Action<User> onSuccess, Action<string> onFailure)
    {
        using (UnityWebRequest request = new UnityWebRequest(url, method))
        {
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonPayload);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                UserWrapper userWrapper = JsonUtility.FromJson<UserWrapper>(request.downloadHandler.text);
                if (userWrapper != null && userWrapper.user != null)
                {
                    onSuccess?.Invoke(userWrapper.user);
                }
                else
                {
                    onFailure?.Invoke("User data is null or malformed.");
                }
            }
            else
            {
                onFailure?.Invoke(request.error);
            }
        }
    }

    public bool CheckValidEmail(string email)
    {
        string emailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        return System.Text.RegularExpressions.Regex.IsMatch(email, emailRegex);
    }


    [System.Serializable]
    public class UserWrapper
    {
        public User user;
    }

    [System.Serializable]
    public class UserPayload
    {
        public string email;
        public UserPayload(string email)
        {
            this.email = email;
        }
    }
}
