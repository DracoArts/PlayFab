using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayFabManager : MonoBehaviour
{
    [Header("------UI Panels------")]
    public GameObject loginPanel;
    public GameObject registerPanel;
    public GameObject errorPanel;
    [Header("------Login------")]
    public InputField loginEmailInput;
    public InputField loginPassInput;
    [Header("------Register------")]
    public InputField regiUsernameInput;
    public InputField registerEmailInput;
    public InputField registerPassInput;

    [Header("------Other Element------")]
    public Text errorText;
    public string titleId = "";

    void Awake()
    {
        if (string.IsNullOrEmpty(titleId))
        {
            Debug.LogError("Title ID is not set. Please set your PlayFab Title ID in the inspector.");
        }
        else
        {
            PlayFabSettings.TitleId = titleId;
        }
    }
    void Start()
    {
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
        errorPanel.SetActive(false);
    }
    public void Register()
    {
        string userEmail = registerEmailInput.text;
        string userPassword = registerPassInput.text;
        string username = regiUsernameInput.text;
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest
        {
            Email = userEmail,
            Password = userPassword,
            Username = username
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        registerPanel.SetActive(false);
        loginPanel.SetActive(true);
        Debug.Log("User registered successfully!");
    }
    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogError("Error registering user: " + error.GenerateErrorReport());
        ShowError(error.GenerateErrorReport());
    }
    public void Login()
    {
        string userEmail = loginEmailInput.text;
        string userPassword = loginPassInput.text;

        LoginWithEmailAddressRequest request = new LoginWithEmailAddressRequest
        {
            Email = userEmail,
            Password = userPassword
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }
    private void OnLoginSuccess(LoginResult result)
    {
        SceneManager.LoadScene("GamePlay");
        Debug.Log("User logged in successfully!");
    }
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("Error logging in: " + error.GenerateErrorReport());
        ShowError(error.GenerateErrorReport());
    }
    private void ShowError(string message)
    {
        errorText.text = message;
        errorPanel.SetActive(true);
        registerPanel.SetActive(false);
        loginPanel.SetActive(false);

    }
}