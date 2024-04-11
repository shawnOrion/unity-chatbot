using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageController : MonoBehaviour
{
    public ChatController chatController;
    public UserController userController;
    public MessageView messageView;
    public MessageService messageService;
    public ErrorHandler errorHandler;

    void Start()
    {
    }

    public void CreateUserMessage(string content)
    {
        string chatId = chatController.activeChat._id;
        Debug.Log("Sending user message.");
        messageService.CreateUserMessage(content, chatId, OnUserMessageSent, OnUserMessageFailed);
    }

    private void OnUserMessageSent(Message message)
    {
        Debug.Log("User message sent.");
        messageView.AddMessageToView(message);
        chatController.activeChat.messages.Add(message._id);
        CreateChatbotMessage(chatController.activeChat);
    }


    private void OnUserMessageFailed(string error)
    {
        errorHandler.ShowErrorMessage("There was a problem sending your message.");
        Debug.LogError("Failed to send message: " + error);
    }

    public void CreateChatbotMessage(Chat chat)
    {
        Debug.Log("Sending chatbot message.");
        messageService.CreateChatbotMessage(chat, OnChatbotMessageSent, OnChatbotMessageFailed);
    }

    private void OnChatbotMessageSent(Message message)
    {
        Debug.Log("User message sent.");
        messageView.AddMessageToView(message);
        chatController.activeChat.messages.Add(message._id);
    }


    private void OnChatbotMessageFailed(string error)
    {
        errorHandler.ShowErrorMessage("There was a problem responding to your message.");
        Debug.LogError("Failed to send message: " + error);
    }


    public void GetMessages(Chat chat)
    {
        Debug.Log("Retrieving messages.");
        messageService.GetMessages(chat, OnMessagesReceived, OnMessagesFailed);
    }

    private void OnMessagesReceived(List<Message> messages)
    {
        Debug.Log("Messages retrieved.");
        messageView.LoadMessagesToView(messages);
    }

    private void OnMessagesFailed(string error)
    {
        errorHandler.ShowErrorMessage("There was a problem retrieving messages.");
        Debug.LogError("Failed to get messages: " + error);
    }
}
