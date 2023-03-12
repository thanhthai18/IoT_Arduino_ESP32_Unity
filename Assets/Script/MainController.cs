using EasyUI.Tabs;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public static MainController instance;
    public GameObject[] arrayTabAll;
    public List<GameObject> listTabCurrent = new List<GameObject>();
    public GameObject currentTab;

    public List<Button> l = new List<Button>();








    private void Awake() => instance = this;
    private void Start()
    {

    }

    public async void Init()
    {
        GlobalValue.isAdmin = true;
        GlobalValue.isAdmin = LoginController.instance.CheckAdmin(MainView.instance.inputEmail.text, MainView.instance.inputPass.text); ;
        TabsUI.eventOnTabClick += HandleOnTabClick;
        if (GlobalValue.isAdmin)
        {
            l.ForEach(s => s.gameObject.SetActive(false));
            for (int i = 0; i < arrayTabAll.Length - 1; i++)
            {
                listTabCurrent.Add(arrayTabAll[i]);
                TabsUI.instance.GetTabBtns(0);
                l[i].gameObject.SetActive(true);
            }
        }
        else
        {
            listTabCurrent.Add(arrayTabAll[1]);
            listTabCurrent.Add(arrayTabAll[5]);
            TabsUI.instance.GetTabBtns(1);
            l.ForEach(s => s.gameObject.SetActive(false));
            l[1].gameObject.SetActive(true);
            l[5].gameObject.SetActive(true);
        }

        listTabCurrent.ForEach(s => s.SetActive(true));


        GC.Collect();
        await RealtimeDB.instance.LoadDataUserAsync();
        GC.Collect();
        Debug.Log(UserController.instance.listProfile.Count);
        //string js = "[{\"idUser\":\"0\",\"ROLE\":\"ADMIN\",\"email\":\"thaiht@gmail.com\",\"fullname\":\"thanhthai\",\"password\":\"thaiht\",\"username\":\"thaiht\",\"createTimeDate\":null,\"listDevice\":[{\"idDevice\":\"0\",\"alive\":\"\",\"nameDevice\":\"\",\"tokenAuth\":\"\",\"tokenCollectData\":\"\",\"dataDevice\":{\"updateAt\":\"\",\"value\":\"\"},\"listSensor\":[{\"idSensor\":\"0\",\"dataSensor\":{\"updateAt\":\"\",\"value\":\"\"},\"nameSensor\":\"\"},{\"idSensor\":\"1\",\"dataSensor\":{\"updateAt\":\"\",\"value\":\"\"},\"nameSensor\":\"\"}]}]},{\"idUser\":\"1\",\"ROLE\":\"USER\",\"email\":\"\",\"fullname\":\"\",\"password\":\"\",\"username\":\"\",\"createTimeDate\":null,\"listDevice\":[{\"idDevice\":\"0\",\"alive\":\"1\",\"nameDevice\":\"\",\"tokenAuth\":\"\",\"tokenCollectData\":\"\",\"dataDevice\":{\"updateAt\":\"\",\"value\":\"\"},\"listSensor\":[{\"idSensor\":\"0\",\"dataSensor\":{\"updateAt\":\"\",\"value\":\"0.00512\"},\"nameSensor\":\"\"},{\"idSensor\":\"1\",\"dataSensor\":{\"updateAt\":\"\",\"value\":\"\"},\"nameSensor\":\"\"}]}]}]";
        MainView.instance.submitAddDevice.onClick.AddListener(AddNewDevice);
        MainView.instance.submitNewUser.onClick.AddListener(AddNewUser);
    }

    public void HandleOnTabClick(int tabIndex)
    {
        Debug.Log("vl" + tabIndex);
    }




    private void OnDisable()
    {
        TabsUI.eventOnTabClick -= HandleOnTabClick;
    }

    public void AddNewDevice()
    {
        MainView.instance.txtToken.text = SinhToken();
        MainView.instance.panelDone.SetActive(true);
        RealtimeDB.instance.reference.Child("User").Child("Profile").Child("0").Child("listDevice").Child("0").Child("tokenAuth").SetValueAsync(MainView.instance.txtToken.text);

    }

    public async void AddNewUser()
    {
        string js = JsonConvert.SerializeObject(UserController.instance.listProfile[0]);
        await RealtimeDB.instance.reference.Child("User").Child("Profile").Child("2").SetRawJsonValueAsync(js).ContinueWith(task1 =>
        {
            if (task1.IsCompleted)
            {
                Debug.Log("thanh cong");
                MainView.instance.panelDone.SetActive(true);
            }
            else
            {
                Debug.Log("that bai");
            }
        });
    }

    public string SinhToken()
    {
        var allChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new System.Random();
        var resultToken = new string(
           Enumerable.Repeat(allChar, 8)
           .Select(token => token[random.Next(token.Length)]).ToArray());

        string authToken = resultToken.ToString();
        return authToken;
    }


}
