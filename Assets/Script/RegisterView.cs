using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterView : View
{
    public static RegisterView instance;

    public InputField inputEmailRegister;
    public InputField inputFullName;
    public InputField inputUserName;
    public InputField inputPasswordRegister;
    public InputField inputConfirmPasswordRegister;
    public Button btnConfirmRegister;
    public Button btnCancelRegister;
    public Text txtThongBaoRegister;

    private void Awake() => instance = this;
    public override void Initialize()
    {
        btnCancelRegister.onClick.AddListener(() => ViewManager.ShowLast());
    }
}
