using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DataLuongNuoc", menuName = "ScriptableObjects/Data")]
public class DataLuongNuoc : ScriptableObject
{
    public int tongSoThangData;
    public List<float> listDataLuongNuoc = new List<float>();
    public event Action<List<float>> eventNhanDataLuongNuoc; // k thể để static vì ScriptableObject được truyen
    //cập thông qua 1 biến có kiểu SciprtableObject, mà thuộc tính static ko phụ thuộc vào biến, ko thể gọi qua biến
    private void OnEnable()
    {
        GetDataLuongNuocServer();
    }

    void GetDataLuongNuocServer()
    {
        listDataLuongNuoc = new List<float>() { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33 };
    }

    public void CallEventDataLuongNuoc()
    {
        eventNhanDataLuongNuoc?.Invoke(listDataLuongNuoc);
    }
}
