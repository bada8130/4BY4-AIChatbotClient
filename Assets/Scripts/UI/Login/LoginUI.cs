using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;
    public Text errorText;

    public void OnLoginButtonClicked()
    {
        string username = usernameField.text;
        string password = passwordField.text;

        UserManager.Instance.ReqLogin(username, password, (error) =>
        {
            errorText.text = error;
        });
    }
}