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
    public int tongSoThangData;
    public float valueLuongNuocMax;
    public List<float> listDataLuongNuoc = new List<float>();
    public event Action<List<float>> eventNhanDataLuongNuoc; // k thể để static vì ScriptableObject được truyen
    //cập thông qua 1 biến có kiểu SciprtableObject, mà thuộc tính static ko phụ thuộc vào biến, ko thể gọi qua biến
    private void OnEnable()
    {
        GetDataLuongNuocServer();
    }

    void GetDataLuongNuocServer()
    {
        switch (id)
        {
            case 0: //luong nuoc theo nam
                listDataLuongNuoc = new List<float>() { 17, 25, 40 };
                break;
            case 1: // luong nuoc theo thang
                listDataLuongNuoc = new List<float>() { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37 };
                break;
            case 2: // so tien theo thang
                listDataLuongNuoc = new List<float>() { 299, 422, 111, 145, 130, 122, 58, 80, 13, 17, 25, 377 };
                break;
            default:
                break;
        }

        tongSoThangData = listDataLuongNuoc.Count;
        valueLuongNuocMax = listDataLuongNuoc.Max();
    }

    public void CallEventDataLuongNuoc()
    {
        eventNhanDataLuongNuoc?.Invoke(listDataLuongNuoc);
    }
}
