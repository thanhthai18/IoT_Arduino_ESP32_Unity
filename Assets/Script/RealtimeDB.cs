using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System;
using Firebase;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class RealtimeDB : MonoBehaviour
{
    public static RealtimeDB instance;
    public DatabaseReference reference;
    public DependencyStatus dependencyStatus;

    public static event Action eventInitFirebaseThanhCong;
    public UserController userController;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }


    }


    private void Start()
    {
        InitializeFirebase();
        userController = UserController.instance;
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
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

    public async Task LoadDataUserAsync()
    {
        await reference.Child("User").Child("Profile").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.ChildrenCount > 0)
                {
                    // Profile of User
                    GlobalValue.countTaiKhoan = (int)snapshot.ChildrenCount;
                    Debug.Log(GlobalValue.countTaiKhoan);
                    for (int i = 0; i < snapshot.ChildrenCount; i++)
                    {
                        Profile profile = new Profile();
                        profile.idUser = snapshot.Child(i.ToString()).Key;
                        profile.ROLE = snapshot.Child(i.ToString()).Child("ROLE").GetValue(true).ToString();
                        profile.email = snapshot.Child(i.ToString()).Child("email").GetValue(true).ToString();
                        profile.fullname = snapshot.Child(i.ToString()).Child("fullname").GetValue(true).ToString();
                        profile.username = snapshot.Child(i.ToString()).Child("username").GetValue(true).ToString();
                        profile.password = snapshot.Child(i.ToString()).Child("password").GetValue(true).ToString();

                        // Device of this profile
                        List<Device> listDevice = new List<Device>();

                        DataSnapshot databaseDevice = snapshot.Child(i.ToString()).Child("Device");
                        for (int j = 0; j < databaseDevice.ChildrenCount; j++)
                        {
                            Device tmpDevice = new Device();
                            tmpDevice.idDevice = databaseDevice.Child(j.ToString()).Key;
                            tmpDevice.alive = databaseDevice.Child(j.ToString()).Child("alive").GetValue(true).ToString();
                            tmpDevice.nameDevice = databaseDevice.Child(j.ToString()).Child("nameDevice").GetValue(true).ToString();
                            tmpDevice.tokenAuth = databaseDevice.Child(j.ToString()).Child("tokenAuth").GetValue(true).ToString();
                            tmpDevice.tokenCollectData = databaseDevice.Child(j.ToString()).Child("tokenCollectData").GetValue(true).ToString();

                            //Data of this device
                            Data dataDevice = new Data();
                            dataDevice.value = databaseDevice.Child(j.ToString()).Child("data").Child("value").GetValue(true).ToString();
                            //dataDevice.updateAt = DateTime.ParseExact(databaseDevice.Child(j.ToString()).Child("data").Child("updateAt").ToString(),
                            //    "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
                            dataDevice.updateAt = databaseDevice.Child(j.ToString()).Child("data").Child("updateAt").GetValue(true).ToString();

                            //"2009-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture

                            tmpDevice.dataDevice = dataDevice;

                            //    //Sensor of this device
                            DataSnapshot databaseSensor = databaseDevice.Child(j.ToString()).Child("sensor");

                           
                            for (int z = 0; z < databaseSensor.ChildrenCount; z++)
                            {
                                Sensor tmpSensor = new Sensor();
                                tmpSensor.idSensor = databaseSensor.Child(z.ToString()).Key;
                                tmpSensor.nameSensor = databaseSensor.Child(z.ToString()).Child("nameSensor").GetValue(true).ToString();
                                tmpSensor.dataSensor.value = databaseSensor.Child(z.ToString()).Child("data").Child("value").GetValue(true).ToString();
                                //tmpSensor.dataSensor.updateAt = DateTime.ParseExact(databaseSensor.Child(z.ToString()).Child("data").Child("updateAt").ToString(),
                                //"yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
                                tmpSensor.dataSensor.updateAt = databaseSensor.Child(z.ToString()).Child("data").Child("updateAt").GetValue(true).ToString();



                                tmpDevice.listSensor.Add(tmpSensor);
                            }



                            profile.listDevice.Add(tmpDevice);



                        }
                        userController.listProfile.Add(profile);
                    }

                    task.Dispose();
                }



            }

        });
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


        var t = reference.Child("DataLuongNuoc").Child(year.ToString()).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.ChildrenCount > 0)
                {
                    for (int i = 0; i < snapshot.ChildrenCount; i++)
                    {
                        listData.Add(float.Parse(snapshot.Child((i + 1).ToString()).GetValue(true).ToString()));
                    }
                }
                task.Dispose();
            }
        });

        await t;
        t.Dispose();
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
