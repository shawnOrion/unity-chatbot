using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatController : MonoBehaviour
{
    public Chat activeChat;
    public ChatView chatView;
    public ChatService chatService;
    public MessageView messageView;
    public MessageController messageController;
    public UserController userController;
    public ErrorHandler errorHandler;

    void Start()
    {
    }

    public void CreateChat(string userId)
    {
        Debug.Log("Creating chat for user id: " + userId);
        chatService.CreateChat(userId, OnChatCreated, OnChatCreationFailed);
    }

    public void GetChats(string userId)
    {
        Debug.Log("Getting chats for user id: " + userId);
        chatService.GetChats(userId, OnChatsReceived, OnChatsRetrievalFailed);
    }

    private void OnChatCreated(Chat chat)
    {
        Debug.Log("Chat created: " + chat._id);
        activeChat = chat;
        userController.activeUser.chats.Add(chat._id);
        messageView.ClearMessageContainer();
    }

    private void OnChatCreationFailed(string error)
    {
        errorHandler.ShowErrorMessage("There was a problem creating the chat.");
        Debug.Log("Failed to create chat: " + error);
    }

    private void OnChatsReceived(List<Chat> chats)
    {
        if (chats.Count == 0)
        {
            Debug.Log("No chats found.");
            return;
        }

        Debug.Log("Chats found: " + chats.Count);
        chatView.AddChatButtons(chats);
    }

    private void OnChatsRetrievalFailed(string error)
    {
        errorHandler.ShowErrorMessage("There was a problem retrieving chats.");
        Debug.LogError("Failed to get chats: " + error);
    }

    public void SelectChat(Chat chat)
    {
        activeChat = chat;
        Debug.Log("Selected chat: " + chat._id);
        messageController.GetMessages(chat);
    }
}
