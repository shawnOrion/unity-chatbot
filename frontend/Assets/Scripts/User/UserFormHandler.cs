using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserFormHandler : MonoBehaviour
{
    public GameObject form;
    public TMP_InputField emailInput;
    public Button signUpButton;
    public Button loginButton;
    public UserController userController;

    private void Start()
    {
        signUpButton.onClick.AddListener(OnSignUpClick);
        loginButton.onClick.AddListener(OnLoginClick);
        ShowForm();
    }

    public void OnSignUpClick()
    {
        string email = emailInput.text;
        userController.CreateUser(email);
    }

    public void OnLoginClick()
    {
        string email = emailInput.text;
        userController.GetUser(email);
    }

    public void ShowForm()
    {
        form.SetActive(true);
    }

    public void HideForm()
    {
        form.SetActive(false);
    }

}
