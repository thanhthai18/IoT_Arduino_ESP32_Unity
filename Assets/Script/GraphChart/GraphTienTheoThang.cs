using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphTienTheoThang : WindowGraph
{
    private List<string> month = new List<string>() { "Jan" , "Feb" , "Mar" , "Apr", "May", "Jun", "Jul", "Aug"
    , "Sep", "Oct", "Nov", "Dec"};

    public Dropdown dropDownYear;
    private void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        OnChangeDropDown();
    }



    private void OnEnable()
    {
        base.OnEnable();
    }
    public void OnChangeDropDown()
    {
        DestroyChildNotBG(graphContainer.transform);
        dataLuongNuocScriptableObject.yearData = int.Parse(dropDownYear.options[dropDownYear.value].text);
        dataLuongNuocScriptableObject.GetDataLuongNuocServer(dataLuongNuocScriptableObject.yearData);
    }

    public override void ShowGraph(List<float> valueList)
    {
        float xSize = graphContainer.rect.width / 12;
        float yMaxinum = dataLuongNuocScriptableObject.valueMaxY;
        float graphHeight = graphContainer.sizeDelta.y;

        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i] / yMaxinum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            CreateUnitText(month[i], xPosition, -40);
            CreateUnitText(valueList[i].ToString(), xPosition, yPosition + 30);
            CreateDotConnection(new Vector2(xPosition, 0)
                , circleGameObject.GetComponent<RectTransform>().anchoredPosition);
        }

        //graphContainer.sizeDelta = new Vector2(graphContainer.rect.width + 20, graphContainer.rect.height); 
    }




    private void OnDisable()
    {
        base.OnDisable();
    }
    #region BACKUP
    //private List<string> month = new List<string>() { "Jan" , "Feb" , "Mar" , "Apr", "May", "Jun", "Jul", "Aug"
    //, "Sep", "Oct", "Nov", "Dec"};

    //public Dropdown dropDownYear;
    //private void Awake()
    //{
    //    base.Awake();
    //}

    //private void Start()
    //{
    //    OnChangeDropDown();
    //}

    //private void OnEnable()
    //{
    //    base.OnEnable();
    //}

    //public void OnChangeDropDown()
    //{
    //    DestroyChildNotBG(graphContainer.transform);
    //    dataLuongNuocScriptableObject.yearData = int.Parse(dropDownYear.options[dropDownYear.value].text);
    //    dataLuongNuocScriptableObject.GetDataLuongNuocServer(dataLuongNuocScriptableObject.yearData);
    //}


    //public override void ShowGraph(List<float> valueList)
    //{
    //    float xSize = graphContainer.rect.width / 12;
    //    float yMaxinum = dataLuongNuocScriptableObject.valueMaxY;
    //    float graphHeight = graphContainer.sizeDelta.y;

    //    for (int i = 0; i < valueList.Count; i++)
    //    {
    //        float xPosition = xSize + i * xSize;
    //        float yPosition = (valueList[i] / yMaxinum) * graphHeight;
    //        GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
    //        CreateUnitText(month[i], xPosition, -40);
    //        CreateUnitText(valueList[i].ToString(), xPosition, yPosition + 30);
    //        CreateDotConnection(new Vector2(xPosition, 0)
    //            , circleGameObject.GetComponent<RectTransform>().anchoredPosition);

    //    }
    //    //graphContainer.sizeDelta = new Vector2(graphContainer.rect.width + 20, graphContainer.rect.height);
    //}


    //private void OnDisable()
    //{
    //    base.OnDisable();
    //}
    #endregion
}
