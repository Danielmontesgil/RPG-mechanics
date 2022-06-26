using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogInController : MonoBehaviour, IUIController
{
    [SerializeField] private GameObject _signInObj;
    [SerializeField] private GameObject _signUpObj;
    [SerializeField] private Button _signInMethodButton;
    [SerializeField] private TextMeshProUGUI _signInMethodText;
    [SerializeField] private Button _signInButton;
    [SerializeField] private Button _signUpButton;

    [Header("Sign In Variables")]
    [SerializeField] private TMP_InputField _usernameSignIn;
    [SerializeField] private TMP_InputField _passwordSignIn;

    [Header("Sign Up Variables")]
    [SerializeField] private TMP_InputField _usernameSignUp;
    [SerializeField] private TMP_InputField _passwordSignUp;
    [SerializeField] private TMP_InputField _confirmPasswordSignUp;
    [SerializeField] private TMP_InputField _emailSignUp;

    private bool _isSigningIn;

    public void Init()
    {
        gameObject.SetActive(true);

        SetupButtons();

        _signInMethodButton.gameObject.SetActive(true);
        _signInMethodText.SetText("Sign Up");

        _signInObj.SetActive(true);

        _isSigningIn = true;
    }

    public void Disable()
    {
        ResetButtons();

        _signInObj.SetActive(false);
        _signUpObj.SetActive(false);
        _signInMethodButton.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }

    private void SetupButtons()
    {
        _signInButton.onClick.AddListener(SignIn);
        _signUpButton.onClick.AddListener(SignUp);
        _signInMethodButton.onClick.AddListener(ChangeSignInMethod);
    }

    private void ResetButtons()
    {
        _signInButton.onClick.RemoveAllListeners();
        _signUpButton.onClick.RemoveAllListeners();
        _signInMethodButton.onClick.RemoveAllListeners();
    }

    private void ChangeSignInMethod()
    {
        _signInMethodText.SetText(_isSigningIn ? "Sign In" : "Sign Up");
        _signInObj.SetActive(!_isSigningIn);
        _signUpObj.SetActive(_isSigningIn);
        _isSigningIn = !_isSigningIn;
    }

    private void SignIn()
    {
        if (string.IsNullOrEmpty(_usernameSignIn.text))
        {
            Debug.LogError("You need to provide an username");
            return;
        }

        if (string.IsNullOrEmpty(_passwordSignIn.text))
        {
            Debug.LogError("You need to provide a password");
            return;
        }

        PlayFabManager.Instance.LogInWithPlayFab(_usernameSignIn.text, _passwordSignIn.text,
            () =>
            {
                Debug.Log("You sign in success");

                GameManager.Instance.ChangeGameState(new GameMenuState());
            });
    }

    private void SignUp()
    {
        if (string.IsNullOrEmpty(_usernameSignUp.text))
        {
            Debug.LogError("You need to provide an username");
            return;
        }

        if (string.IsNullOrEmpty(_passwordSignUp.text))
        {
            Debug.LogError("You need to provide a password");
            return;
        }

        if (string.IsNullOrEmpty(_confirmPasswordSignUp.text))
        {
            Debug.LogError("You need to confirm your password");
            return;
        }

        if (string.IsNullOrEmpty(_emailSignUp.text))
        {
            Debug.LogError("You need to provide an email");
            return;
        }

        if(_passwordSignUp.text != _confirmPasswordSignUp.text)
        {
            Debug.LogError("Your passwords does not match");
            return;
        }

        PlayFabManager.Instance.SignUpWithPlayFab(_usernameSignUp.text, _passwordSignUp.text, _emailSignUp.text,
            () =>
            {
                Debug.Log("You created an account");

                GameManager.Instance.ChangeGameState(new GameMenuState());
            });
    }
}