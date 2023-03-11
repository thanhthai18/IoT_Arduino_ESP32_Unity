using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public event Action<List<float>> eventNhanDataLuongNuoc; // k thể để static vì ScriptableObject được truyen
                                                             //cập thông qua 1 biến có kiểu SciprtableObject, mà thuộc tính static ko phụ thuộc vào biến, ko thể gọi qua biến

    public int yearData = 2019;
    public int yearStartForGraphYear = 2019;
    private void OnEnable()
    {
        if (id == 0)
        {
            GetDataLuongNuocServer();
        }
        else if (id == 1 || id == 2)
        {
            GetDataLuongNuocServer(yearData);
        }
    }

    public void GetDataLuongNuocServer(int year)
    {
        Debug.Log("data: " + year.ToString());
        switch (id)
        {
            case 1: // luong nuoc theo thang
                if (year == 2020)
                    listDataLuongNuoc = new List<float>() { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37 };
                if (year == 2021)
                    listDataLuongNuoc = new List<float>() { 5, 44, 55, 44, 33, 22, 11, 11, 13, 77, 66, 22 };
                if (year == 2022)
                    listDataLuongNuoc = new List<float>() { 22, 22, 22, 22, 30, 22, 22, 15, 13, 17, 25, 37 };
                yearData = year;
                break;
            case 2: // so tien theo thang
                if (year == 2020)
                    listDataLuongNuoc = new List<float>() { 299, 422, 111, 145, 130, 122, 58, 80, 13, 17, 25, 377 };
                if (year == 2021)
                    listDataLuongNuoc = new List<float>() { 5, 44, 55, 44, 33, 22, 11, 11, 13, 77, 66, 22 };
                if (year == 2022)
                    listDataLuongNuoc = new List<float>() { 22, 22, 22, 22, 30, 22, 22, 15, 13, 17, 25, 37 };
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

    void GetDataLuongNuocServer()
    {
        //luong nuoc theo nam
        if (id == 0)
        {
            listDataLuongNuoc = new List<float>() { 17, 25, 40 };
            numDataX = 3;
            valueMaxY = listDataLuongNuoc.Max();
            valueMinY = listDataLuongNuoc.Min();
        }
        CallEventDataLuongNuoc();
    }



    public void CallEventDataLuongNuoc()
    {
        eventNhanDataLuongNuoc?.Invoke(listDataLuongNuoc);
    }
}
