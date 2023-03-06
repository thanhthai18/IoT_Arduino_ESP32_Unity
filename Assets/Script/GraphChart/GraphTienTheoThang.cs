using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphTienTheoThang : WindowGraph
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

    public GameObject CreateCircle(Vector2 anchoredPosition)
    {
        return base.CreateCircle(anchoredPosition);
    }

    public override void ShowGraph(List<float> valueList)
    {
        float xSize = graphContainer.rect.width / 12;
        float yMaxinum = dataLuongNuocScriptableObject.valueLuongNuocMax;
        float graphHeight = graphContainer.sizeDelta.y;
        GameObject lastCircleGameObject = null;

        GameObject root = CreateCircle(new Vector2(0, 0));
        lastCircleGameObject = root;

        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i] / yMaxinum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition
                    , circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;
        }
    }

    public void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        base.CreateDotConnection(dotPositionA, dotPositionB);
    }

    public float GetAngleFromVectorFloat(Vector2 dir)
    {
        return base.GetAngleFromVectorFloat(dir);
    }

    public void HandleDataLuongNuoc(List<float> listDataLuongNuoc)
    {
        base.HandleDataLuongNuoc(listDataLuongNuoc);
    }

    private void OnDisable()
    {
        base.OnDisable();
    }
}
