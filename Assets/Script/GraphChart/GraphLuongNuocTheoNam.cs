using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphLuongNuocTheoNam : WindowGraph
{
    private void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        base.Start();
    }

    private void OnEnable()
    {
        base.OnEnable();
    }


    public override void ShowGraph(List<float> valueList)
    {    
        float xSize = graphContainer.rect.width / dataLuongNuocScriptableObject.numDataX;
        float yMaxinum = dataLuongNuocScriptableObject.valueMaxY;
        float graphHeight = graphContainer.sizeDelta.y;
        GameObject lastCircleGameObject = null;

        GameObject root = CreateCircle(new Vector2(0, 0));
        lastCircleGameObject = root;

        int yearNum = dataLuongNuocScriptableObject.yearStartForGraphYear;

        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i] / yMaxinum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            CreateUnitText(yearNum++.ToString(), xPosition, -40);
            CreateUnitText(valueList[i].ToString(), -60, yPosition);
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition
                    , circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;
        }
        //graphContainer.sizeDelta = new Vector2(graphContainer.rect.width + 20, graphContainer.rect.height);
    }
    

    private void OnDisable()
    {
        base.OnDisable();
    }
}
