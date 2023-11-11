using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image mImage;
    public LineDrawer mLineDrawer;
    public Schema mSchema;
    private bool isAdded = false;
    static string mPattern = @"\d+";
    public Vector2 mPosition;
    public string mText;

    int getParsing(string str)
    {
        Match match = Regex.Match(str, mPattern);

        if (match.Success)
        {
            int number;
            if (int.TryParse(match.Value, out number))
            {
                return number;
            }
        }
        return -1;
    }

    void Start()
    {
        mImage = transform.GetChild(0).GetComponent<Image>();
        mLineDrawer = GameObject.FindObjectOfType<LineDrawer>();
        mSchema = GameObject.FindObjectOfType<Schema>();

        int x = getParsing(gameObject.name);
        int y = getParsing(gameObject.transform.parent.name);

        mPosition = new Vector2(x, y);
    }

    private void Update()
    {
        if(!Schema.mIsDragging)
        {
            isAdded = false;
            mImage.color = Color.white;
        }
    }

    public void addDot()
    {
        if (Schema.mIsDragging && !isAdded)
        {
            isAdded = true;
            RectTransform ui_element = GetComponent<RectTransform>();
            RectTransform parentRectTransform = ui_element.parent.GetComponent<RectTransform>();
            Vector3 localPosition = ui_element.localPosition + parentRectTransform.localPosition;

            mLineDrawer.addPoint(localPosition, ui_element.rect.width);
            mSchema.addPositionInSchema(mPosition);
            mImage.color = Color.red;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        addDot();
        mImage.color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //mImage.color = Color.white;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        ExecuteEvents.Execute(mSchema.gameObject, eventData, ExecuteEvents.pointerDownHandler);
        addDot();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        ExecuteEvents.Execute(mSchema.gameObject, eventData, ExecuteEvents.pointerUpHandler);
    }

}
