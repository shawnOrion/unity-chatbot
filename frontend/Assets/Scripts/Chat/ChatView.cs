using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ChatView : MonoBehaviour
{

    public GameObject chatContainer;
    public GameObject chatButtonPrefab;
    public ChatController chatController;
    public UserController userController;
    public MessageView messageView;
    public Button createChatButton;
    void Start()
    {
        createChatButton.onClick.AddListener(OnCreateChatClick);
    }

    public void AddChatButtons(List<Chat> chats)
    {
        foreach (Chat chat in chats)
        {
            GameObject chatButton = Instantiate(chatButtonPrefab, chatContainer.transform);
            TextMeshProUGUI buttonText = chatButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = chat.title;
            Button button = chatButton.GetComponent<Button>();
            button.onClick.AddListener(() => OnChatClick(chat));
        }
    }

    public void AddChatButton(Chat chat)
    {
        GameObject chatButton = Instantiate(chatButtonPrefab, chatContainer.transform);
        TextMeshProUGUI buttonText = chatButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = chat.title;
        Button button = chatButton.GetComponent<Button>();
        button.onClick.AddListener(() => OnChatClick(chat));
    }

    public void OnChatClick(Chat chat)
    {
        chatController.SelectChat(chat);
    }

    public void OnCreateChatClick()
    {
        string userId = userController.activeUser._id;
        if (userId == null)
        {
            Debug.LogError("Can't create chat without a user.");
            return;
        }

        chatController.CreateChat(userId);
    }
}
