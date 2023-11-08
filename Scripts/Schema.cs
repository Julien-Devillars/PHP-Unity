using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;


public class Schema : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerExitHandler
{
    static public bool mIsDragging;
    public LineDrawer mLineDrawer;
    public TextMeshProUGUI mText;

    public List<Vector2> mSchemaPosition;
    private List<Vector2> test = new List<Vector2>
            {
            new Vector2(0f, 0f) ,
            new Vector2(1f, 0f) ,
            new Vector2(2f, 0f) ,
            new Vector2(3f, 0f) ,
            new Vector2(4f, 0f)
            };
public Dictionary<List<Vector2>, string> mSchemaToText = new Dictionary<List<Vector2>, string>()
    {
        { new List<Vector2>
            {
            new Vector2(0f, 0f) ,
            new Vector2(1f, 0f) ,
            new Vector2(2f, 0f) ,
            new Vector2(3f, 0f) ,
            new Vector2(4f, 0f)
            }
        , "This is the first Schema" },
        { new List<Vector2>
            {
            new Vector2(0f, 0f) ,
            new Vector2(1f, 1f) ,
            new Vector2(2f, 2f) ,
            new Vector2(3f, 3f) ,
            new Vector2(4f, 4f)
            }
        , "This is the second Schema" },
        { new List<Vector2>
            {
            new Vector2(0f, 0f) ,
            new Vector2(0f, 1f) ,
            new Vector2(0f, 2f) ,
            new Vector2(0f, 3f) ,
            new Vector2(0f, 4f)
            }
        , "This is the third Schema" }
    };

    void Start()
    {
        mLineDrawer = GameObject.FindObjectOfType<LineDrawer>();
        mSchemaPosition = new List<Vector2>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        mIsDragging = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        clear();
    }

    public void addPositionInSchema(Vector2 pos)
    {
        mSchemaPosition.Add(pos);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        clear();
    }

    bool AreListsEqual<T>(List<T> list1, List<T> list2)
    {
        // Check if the lists have the same count
        if (list1.Count != list2.Count)
        {
            return false;
        }

        // Iterate through the elements and compare them
        for (int i = 0; i < list1.Count; i++)
        {
            if (!list1[i].Equals(list2[i]))
            {
                return false;
            }
        }

        // If no differences were found, the lists are equal
        return true;
    }

    public void clear()
    {
        mIsDragging = false;
        foreach(Vector2 pos in mSchemaPosition)
        {
            Debug.Log(pos);
        }

        foreach (List<Vector2> schema in mSchemaToText.Keys)
        {
            if (AreListsEqual(schema, mSchemaPosition))
            {
                Debug.Log("Found");
                string text = mSchemaToText[schema];
                Debug.Log(text);

                mText.text = text;
                break;
            }
        }
        //if (mSchemaToText.ContainsKey(mSchemaPosition))
        //{
        //    Debug.Log("into the wild");
        //    string text = mSchemaToText[mSchemaPosition];
        //    mText.text = text;
        //}
        mLineDrawer.clear();
        mSchemaPosition.Clear();
    }

}
