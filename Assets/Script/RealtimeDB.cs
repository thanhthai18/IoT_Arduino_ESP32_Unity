using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System;
using Firebase;
using System.Threading.Tasks;

public class RealtimeDB : MonoBehaviour
{
    public static RealtimeDB instance;
    public DatabaseReference reference;
    public DependencyStatus dependencyStatus;

    public static event Action eventInitFirebaseThanhCong;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

      
    }

    
    private void Start()
    {
        //Invoke(nameof(InitializeFirebase), 0.1f);
        InitializeFirebase();
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                var app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
        //InitializeFirebase();
        //Check that all of the necessary dependencies for Firebase are present on the system
        //FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        //{
        //    dependencyStatus = task.Result; ;
        //    if (dependencyStatus == DependencyStatus.Available)
        //    {
        //        //If they are avalible Initialize Firebase
        //        InitializeFirebase();
        //    }

        //    else
        //    {
        //        Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
        //    }
        //});

    }



    public void InitializeFirebase()
    {
        try
        {
            reference = FirebaseDatabase.DefaultInstance.RootReference;
            eventInitFirebaseThanhCong?.Invoke();
            Debug.Log(reference);

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    public void SaveData()
    {
        //DonHang donhang = new DonHang();
        //reference.Child("DonHang").GetValueAsync().ContinueWith(task =>
        //{
        //    if (task.IsCompleted)
        //    {
        //        DataSnapshot snapshot = task.Result;
        //        donhang.id = (snapshot.ChildrenCount).ToString();

        //        donhang.name = AppController.instance.tenNguoiMua.text;
        //        donhang.sdt = AppController.instance.soDienThoai.text;
        //        donhang.diaChi = AppController.instance.diaChi.text;
        //        donhang.listSanPham = AppController.instance.txtListSanPhamDat.text;
        //        donhang.thanhTien = AppController.instance.txtThanhTien.text;
        //        donhang.phuongThuc = AppController.instance.txtPhuongThucThanhToan.options[0].text;
        //        donhang.date = DateTime.Now.ToString();
        //        donhang.trangThai = "Chờ duyệt";
        //        string json = JsonUtility.ToJson(donhang);

        //        reference.Child("DonHang").Child(donhang.id).SetRawJsonValueAsync(json).ContinueWith(task =>
        //        {
        //            if (task.IsCompleted)
        //            {
        //                Debug.Log("thanh cong");
        //            }
        //            else
        //            {
        //                Debug.Log("that bai");
        //            }
        //        });
        //    }
        //});
    }

    public void LoadData()
    {
        //reference.Child("DonHang").GetValueAsync().ContinueWith(task =>
        //{
        //    if (task.IsCompleted)
        //    {
        //        DataSnapshot snapshot = task.Result;
        //        if (snapshot.ChildrenCount > 1)
        //        {
        //            ListDonHang.instance.countDonHang = snapshot.ChildrenCount - 1;
        //            for (int i = 1; i < snapshot.ChildrenCount; i++)
        //            {
        //                ListDonHang.instance.date_List.Add(snapshot.Child(i.ToString()).Child("date").GetValue(true).ToString());
        //                ListDonHang.instance.diaChi_List.Add(snapshot.Child(i.ToString()).Child("diaChi").GetValue(true).ToString());
        //                ListDonHang.instance.id_List.Add(snapshot.Child(i.ToString()).Child("id").GetValue(true).ToString());
        //                ListDonHang.instance.listSanPham_List.Add(snapshot.Child(i.ToString()).Child("listSanPham").GetValue(true).ToString());
        //                ListDonHang.instance.name_List.Add(snapshot.Child(i.ToString()).Child("name").GetValue(true).ToString());
        //                ListDonHang.instance.phuongThuc_List.Add(snapshot.Child(i.ToString()).Child("phuongThuc").GetValue(true).ToString());
        //                ListDonHang.instance.sdt_List.Add(snapshot.Child(i.ToString()).Child("sdt").GetValue(true).ToString());
        //                ListDonHang.instance.thanhTien_List.Add(snapshot.Child(i.ToString()).Child("thanhTien").GetValue(true).ToString());
        //                ListDonHang.instance.trangThai_List.Add(snapshot.Child(i.ToString()).Child("trangThai").GetValue(true).ToString());
        //            }
        //        }

        //    }
        //});
    }



    public void SetTrangThai(string trangthai, string id)
    {
        reference.Child("DonHang").Child(id).Child("trangThai").SetValueAsync(trangthai).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("thay doi trang thai");
            }
            else
            {
                Debug.Log("that bai thay doi trang thai");
            }
        });
    }

    public async Task<List<float>> GetDataLuongNuocTheoThangTrongNamAsync(int year)
    {
        List<float> listData = new List<float>();

        await reference.Child("DataLuongNuoc").Child(year.ToString()).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.ChildrenCount > 0)
                {
                    for (int i = 0; i < snapshot.ChildrenCount; i++)
                    {
                        listData.Add(float.Parse(snapshot.Child((i+1).ToString()).GetValue(true).ToString()));
                    }
                }
            }
        });

        return listData;

    }

    public async Task<List<float>> GetDataSoTienTheoThangTrongNamAsync(int year, float giaTienKhoiNuoc)
    {
        List<float> listSoTien = new List<float>();
        var listTmpLuongNuoc = await GetDataLuongNuocTheoThangTrongNamAsync(year);
        listTmpLuongNuoc.ForEach(s => { listSoTien.Add(s * giaTienKhoiNuoc * 1000); });

        return listSoTien;

    }

}
