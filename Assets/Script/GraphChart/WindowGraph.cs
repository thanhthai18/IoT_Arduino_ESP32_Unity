using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class WindowGraph : MonoBehaviour
{
    public RectTransform graphContainer;
    [SerializeField] public Sprite circleSprite;
    [SerializeField]
    public DataLuongNuoc dataLuongNuocScriptableObject;

    public virtual void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
    }

    public virtual void Start()
    {
        dataLuongNuocScriptableObject.CallEventDataLuongNuoc();

    }

    public virtual void OnEnable()
    {
        dataLuongNuocScriptableObject.eventNhanDataLuongNuoc += HandleDataLuongNuoc;
    }


    public GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(20, 20);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    public abstract void ShowGraph(List<float> valueList);
    //{

    //    float xSize = graphContainer.rect.width / 15;
    //    float yMaxinum = dataLuongNuocScriptableObject.valueLuongNuocMax;
    //    float graphHeight = graphContainer.sizeDelta.y;
    //    GameObject lastCircleGameObject = null;

    //    GameObject root = CreateCircle(new Vector2(0, 0));
    //    lastCircleGameObject = root;

    //    for (int i = 0; i < valueList.Count; i++)
    //    {
    //        float xPosition = xSize + i * xSize;
    //        float yPosition = (valueList[i] / yMaxinum) * graphHeight;
    //        GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
    //        if (lastCircleGameObject != null)
    //        {
    //            CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition
    //                , circleGameObject.GetComponent<RectTransform>().anchoredPosition);
    //        }
    //        lastCircleGameObject = circleGameObject;
    //    }
    //}




    public virtual void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * 0.5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));
    }

    public float GetAngleFromVectorFloat(Vector2 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public virtual void HandleDataLuongNuoc(List<float> listDataLuongNuoc)
    {
        ShowGraph(listDataLuongNuoc);
    }

    public virtual void OnDisable()
    {
        dataLuongNuocScriptableObject.eventNhanDataLuongNuoc -= HandleDataLuongNuoc;
    }
}
