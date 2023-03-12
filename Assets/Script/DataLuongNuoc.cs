using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DataLuongNuoc", menuName = "ScriptableObjects/Data")]
public class DataLuongNuoc : ScriptableObject
{
    public int id;
    public int numDataX;
    public float valueMaxY;
    public float valueMinY;
    public List<float> listDataLuongNuoc = new List<float>();
    public List<float> listDataTungNam = new List<float>();
    public event Action<List<float>> eventNhanDataLuongNuoc; // k thể để static vì ScriptableObject được truyen
                                                             //cập thông qua 1 biến có kiểu SciprtableObject, mà thuộc tính static ko phụ thuộc vào biến, ko thể gọi qua biến

    public int yearData = 2020;
    public int yearStartForGraphYear = 2020;
    private void OnEnable()
    {
        RealtimeDB.eventInitFirebaseThanhCong += LayData;
    }

    private void OnDisable()
    {
        RealtimeDB.eventInitFirebaseThanhCong -= LayData;
    }
    public void LayData()
    {
        if (id == 0)
        {
            GetDataLuongNuocServerAsync();
        }
        else if (id == 1 || id == 2)
        {
            GetDataLuongNuocServerAsync(yearData);
        }
    }

    public async void GetDataLuongNuocServerAsync(int year)
    {
        switch (id)
        {
            case 1: // luong nuoc theo thang
                //if (year == 2020)
                //    listDataLuongNuoc = RealtimeDB.instance.GetDataLuongNuocTheoThangTrongNam(year); //new List<float>() { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37 };
                //if (year == 2021)
                //    listDataLuongNuoc = RealtimeDB.instance.GetDataLuongNuocTheoThangTrongNam(year); //new List<float>() { 5, 44, 55, 44, 33, 22, 11, 11, 13, 77, 66, 22 };
                //if (year == 2022)
                //    listDataLuongNuoc = RealtimeDB.instance.GetDataLuongNuocTheoThangTrongNam(year); //new List<float>() { 22, 22, 22, 22, 30, 22, 22, 15, 13, 17, 25, 37 };
                var a = RealtimeDB.instance.GetDataLuongNuocTheoThangTrongNamAsync(year);
                await a;
                listDataLuongNuoc = a.Result;
                yearData = year;
                a.Dispose();
                break;
            case 2: // so tien theo thang
                //if (year == 2020)
                //    listDataLuongNuoc = RealtimeDB.instance.GetDataSoTienTheoThangTrongNam(year, 3.5f); //new List<float>() { 299, 422, 111, 145, 130, 122, 58, 80, 13, 17, 25, 377 };
                //if (year == 2021)
                //    listDataLuongNuoc = RealtimeDB.instance.GetDataSoTienTheoThangTrongNam(year, 3.5f); //new List<float>() { 5, 44, 55, 44, 33, 22, 11, 11, 13, 77, 66, 22 };
                //if (year == 2022)
                //    listDataLuongNuoc = RealtimeDB.instance.GetDataSoTienTheoThangTrongNam(year, 3.5f); //new List<float>() { 22, 22, 22, 22, 30, 22, 22, 15, 13, 17, 25, 37 };

                listDataLuongNuoc = await RealtimeDB.instance.GetDataSoTienTheoThangTrongNamAsync(year, 3.5f);
                yearData = year;
                break;
            default:
                break;
        }

        numDataX = 12;
        valueMaxY = listDataLuongNuoc.Max();
        valueMinY = listDataLuongNuoc.Min();
        CallEventDataLuongNuoc();
    }

    public void GetDataLuongNuocServerAsync()
    {
        //luong nuoc theo nam
        listDataTungNam.Clear();
        if (id == 0)
        {
            RealtimeDB.instance.reference.Child("DataLuongNuoc").GetValueAsync().ContinueWith(task =>
            {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log(snapshot.Children.ToList().Count);
                    snapshot.Children.ToList().ForEach(s =>
                    {
                        //float max = 0;
                        //s.Children.ToList().ForEach(z =>
                        //{
                        //    if (float.Parse(z.Value.ToString()) > max)
                        //    {
                        //        max = float.Parse(z.Value.ToString());
                        //    }

                        //});
                        float sum = 0;
                        s.Children.ToList().ForEach(z =>
                        {
                            sum += float.Parse(z.Value.ToString());

                        });

                        listDataTungNam.Add(sum);


                    });

                    numDataX = (int)snapshot.ChildrenCount;
                    valueMaxY = listDataTungNam.Max();
                    valueMinY = listDataTungNam.Min();

                    CallEventDataLuongNuoc();
                }
            });
        }
    }



    public void CallEventDataLuongNuoc()
    {
        if (id == 0)
        {
            eventNhanDataLuongNuoc?.Invoke(listDataTungNam);
        }
        else
        {
            eventNhanDataLuongNuoc?.Invoke(listDataLuongNuoc);
        }
    }
}
