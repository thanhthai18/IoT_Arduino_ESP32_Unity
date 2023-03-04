using EasyUI.Tabs;
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


    private void Awake() => instance = this;


    public void Init()
    {
        GlobalValue.isAdmin = true;
        TabsUI.eventOnTabClick += HandleOnTabClick;
        if (GlobalValue.isAdmin)
        {
            for (int i = 0; i < arrayTabAll.Length - 1; i++)
            {
                listTabCurrent.Add(arrayTabAll[i]);
                TabsUI.instance.GetTabBtns(0);
            }            
        }
        else
        {
            listTabCurrent.Add(arrayTabAll[1]);
            listTabCurrent.Add(arrayTabAll[5]);
            TabsUI.instance.GetTabBtns(1);
        }

        listTabCurrent.ForEach(s => s.SetActive(true));
     
        
    }

    public void HandleOnTabClick(int tabIndex)
    {
        Debug.Log("vl" + tabIndex);
    }




    private void OnDisable()
    {
        TabsUI.eventOnTabClick -= HandleOnTabClick;
    }
}
