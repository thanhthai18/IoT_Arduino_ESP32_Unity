using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : View
{
    public static MainView instance;

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

    private void Awake() => instance = this;
    public override void Initialize()
    {
        btnLogOut.onClick.AddListener(() => ViewManager.Show<LoginView>());
    }

    private void OnEnable()
    {
        MainController.instance.Init();
    }


}
