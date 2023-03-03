using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginView : View
{
    public static LoginView instance;

    public InputField inputEmailLogin;
    public InputField inputPasswordLogin;
    public Button btnLogin;
    public Button btnRegiser;
    public Text txtThongBaoLogin;

    private void Awake() => instance = this;
    public override void Initialize()
    {
        btnRegiser.onClick.AddListener(() => ViewManager.Show<RegisterView>());
        btnLogin.onClick.AddListener(() => ViewManager.Show<MainView>());
    }
}
