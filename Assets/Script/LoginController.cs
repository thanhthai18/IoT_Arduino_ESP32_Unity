using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginController : MonoBehaviour
{
    public static LoginController instance;
    public string inputEmailCurrent;
    public string inputPasswordCurrent;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        LoginView.instance.btnLogin.onClick.AddListener(() => Login(inputEmailCurrent, inputPasswordCurrent));
    }
    public void Login(string _email, string _password)
    {
        //User user = CheckAccount();
        //if(user == null)
        //{
        //    LoginView.instance.txtThongBaoLogin.text = "Fail";
        //}
        //else
        //{
        //    LoginView.instance.txtThongBaoLogin.text = "Successful";
        //}
        
    }
    public bool CheckAdmin(string _email, string _password)
    {
        if (_email == "thaiht@gmail.com") return true;
        else
            return false;
    }
    //public User CheckAccount()
    //{
    //    User user = new User();
    //    return user;
    //}
}
