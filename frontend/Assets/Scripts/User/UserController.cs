using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class UserController : MonoBehaviour
{
    public User activeUser;
    public UserFormHandler userFormHandler;
    public ChatController chatController;
    public UserService userService; 
    public ErrorHandler errorHandler;       
    void Start()
    {
    }

    public void CreateUser(string email)
    {
        if (!userService.CheckValidEmail(email))
        {
            errorHandler.ShowErrorMessage("Please enter a valid email address.");
            return;
        }
        Debug.Log("Attempting to create user with email: " + email);
        userService.CreateUser(email, OnUserCreated, OnUserCreateFailed);
    }

    public void GetUser(string email)
    {
         if (!userService.CheckValidEmail(email))
        {
            errorHandler.ShowErrorMessage("Please enter a valid email address.");
            return;
        }
        Debug.Log("Attempting to get user with email: " + email);
        userService.GetUser(email, OnUserReceived, OnUserGetFailed);
    }

    private void OnUserCreated(User user)
    {
        activeUser = user;
        Debug.Log("User created with id: " + activeUser._id);
        userFormHandler.HideForm();
        chatController.CreateChat(activeUser._id); 
    }

    private void OnUserCreateFailed(string error)
    {
        Debug.LogError("Failed to create user. Error: " + error);
        if (error == "User already exists")
        {
            errorHandler.ShowErrorMessage("User already exists. Please log in.");
        } else {
            errorHandler.ShowErrorMessage("There was a problem creating the user.");
        }
    }

    private void OnUserReceived(User user)
    {
        activeUser = user;
        Debug.Log("User found with id: " + activeUser._id);
        userFormHandler.HideForm();
        chatController.GetChats(activeUser._id); 
        chatController.CreateChat(activeUser._id); 
    }

    private void OnUserGetFailed(string error)
    {
        Debug.LogError("Failed to get user. Error: " + error);
        if (error == "User not found")
        {
            errorHandler.ShowErrorMessage("User not found. Please sign up.");
        } else {
            errorHandler.ShowErrorMessage("There was a problem getting the user.");
        }
    }

    [System.Serializable]
    public class UserWrapper
    {
        public User user;
    }
}
