using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    public static UserController instance;
    public List<Profile> listProfile = new List<Profile>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }
}



public class Profile
{
    public int idUser;
    public string ROLE;
    public string email;
    public string fullname;
    public string password;
    public string username;
    public DateTime createTimeDate;
    public List<Device> listDevice = new List<Device>();

}

public class Device
{
    public int idDevice;
    public int alive; // 0-1
    public string nameDevice;
    public string tokenAuth;
    public string tokenCollectData;
    public Data dataDevice;
    public List<Sensor> listSensor = new List<Sensor>();
}

public struct Data
{
    public DateTime updateAt;
    public float value;
}

public class Sensor
{
    public int idSensor;
    public Data dataSensor;
    public string nameSensor;
}
