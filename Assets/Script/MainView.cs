using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : View
{
    public static MainView instance;



    [Header("All UI")]

    public InputField inputEmail;
    public InputField inputPass;

    public Button btnTongQuan;
    public Button btnThongTinTaiKhoan;
    public Button btnDanhSachTaiKhoan;
    public Button btnThemTaiKhoan;
    public Button btnThemThietBi;
    public Button btnLogOut;


    public Text txtChucVu;
    public Text txtTongTaiKhoan;
    public Text txtTongThietBi;
    public Text txtTongDuLieu;

    public Text txtROLE_Profile;
    public Text txtUsername_Profile;
    public Text txtFullname_Profile;
    public Button btnUpdateProfile_Profile;

    public GameObject contentScrollView_ListUser;

    public Button submitAddDevice;
    public Text txtToken;

    public Button submitNewUser;
    public GameObject panelDone;
    



    private void Awake() => instance = this;
    public override void Initialize()
    {
        btnLogOut.onClick.AddListener(() => ViewManager.Show<LoginView>());
    }

    private void OnEnable()
    {
        MainController.instance.Init();
        panelDone.SetActive(false);
    }


}
