using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ErrorHandler : MonoBehaviour
{
    public TextMeshProUGUI errorMessageText;
    public GameObject errorMessagePanel;
    void Start()
    {
        errorMessagePanel.SetActive(false);
    }

    public void ShowErrorMessage(string message)
    {
        errorMessageText.text = message;
        errorMessagePanel.SetActive(true);
        StartCoroutine(HideErrorMessage());
    }

    IEnumerator HideErrorMessage()
    {
        yield return new WaitForSeconds(5);
        errorMessagePanel.SetActive(false);
    }
}
