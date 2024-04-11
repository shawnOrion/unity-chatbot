// MessageView.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MessageView : MonoBehaviour
{
    public GameObject messagePrefab;
    public GameObject messageContainer;
    public ScrollRect messagesScrollRect;
    public TMP_InputField messageInput;
    public MessageController messageController;
    public Button sendButton;

    void Start()
    {
        sendButton.onClick.AddListener(OnSendClick);
    }

    public void LoadMessagesToView(List<Message> messages)
    {
        ClearMessageContainer();

        foreach (Message message in messages)
        {
            AddMessageToView(message);
        }
    }

    public void ClearMessageContainer()
    {
        foreach (Transform child in messageContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void AddMessageToView(Message message)
    {
        GameObject newMessageEl = Instantiate(messagePrefab, messageContainer.transform);

        TextMeshProUGUI senderText = newMessageEl.transform.Find("Sender").GetComponent<TextMeshProUGUI>();
        senderText.text = message.role;

        TextMeshProUGUI contentText = newMessageEl.transform.Find("Content").GetComponent<TextMeshProUGUI>();
        contentText.text = message.content;
    
        // Scroll to the bottom after a screen update
        StartCoroutine(ScrollToBottomNextFrame());
    }


    public void OnSendClick()
    {
        string content = messageInput.text;
        if (content == "")
        {
            return;
        }
        messageInput.text = "";
        messageController.CreateUserMessage(content);
    }

    IEnumerator ScrollToBottomNextFrame()
    {
        yield return new WaitForEndOfFrame();

        messagesScrollRect.verticalNormalizedPosition = 0f;
    }

}
