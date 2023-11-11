using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Profiling.Memory.Experimental;


public class Schema : FetchDataFromWeb, IPointerUpHandler, IPointerDownHandler, IPointerExitHandler
{
    static public bool mIsDragging;
    public LineDrawer mLineDrawer;
    public TextMeshProUGUI mText;
    public GameObject mButton;
    public GameObject mSchema;

    public List<Vector2> mSchemaPosition;
    public string mCurrentSchemaString;

    public Dictionary<Vector2, int> translater = new Dictionary<Vector2, int>()
    {
        {new Vector2(0f, 0f), 1 },
        {new Vector2(1f, 0f), 2 },
        {new Vector2(2f, 0f), 3 },
        {new Vector2(3f, 0f), 4 },
        {new Vector2(4f, 0f), 5 },

        {new Vector2(0f, 1f), 6 },
        {new Vector2(1f, 1f), 7 },
        {new Vector2(2f, 1f), 8 },
        {new Vector2(3f, 1f), 9 },
        {new Vector2(4f, 1f), 10 },

        {new Vector2(0f, 2f), 11 },
        {new Vector2(1f, 2f), 12 },
        {new Vector2(2f, 2f), 13 },
        {new Vector2(3f, 2f), 14 },
        {new Vector2(4f, 2f), 15 },

        {new Vector2(0f, 3f), 16 },
        {new Vector2(1f, 3f), 17 },
        {new Vector2(2f, 3f), 18 },
        {new Vector2(3f, 3f), 19 },
        {new Vector2(4f, 3f), 20 },

        {new Vector2(0f, 4f), 21 },
        {new Vector2(1f, 4f), 22 },
        {new Vector2(2f, 4f), 23 },
        {new Vector2(3f, 4f), 24 },
        {new Vector2(4f, 4f), 25 },
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
        if (mSchemaPosition.Count < 3)
        {
            mLineDrawer.clear();
            mSchemaPosition.Clear();
            return;
        }

        List<string> str_positions = new List<string>();
        foreach(Vector2 pos in mSchemaPosition)
        {
            // Before
            //str_positions.Add($"({pos.x},{pos.y})");
            string str = translater[pos].ToString();
            str_positions.Add(str);
        }

        //  Before
        //mCurrentSchemaString = string.Join(";", str_positions);
        mCurrentSchemaString = string.Join("", str_positions);

        checkSchema();

        mLineDrawer.clear();
        mSchemaPosition.Clear();
    }

    public void checkSchema()
    {
        string url = $"http://{IPConfig.IP}/{IPConfig.DEFAULT}/get_schema.php?mission={IPConfig.MISSION}&lang={Translation.lang}&schema={mCurrentSchemaString}";
        full_path = url;

        StartCoroutine(getData(url, response =>
        {
            if (!string.IsNullOrEmpty(response))
            {
                if(response.Contains("<Error>") || response.Contains("<Erreur>"))
                {
                    response = response.Replace("<Error>", "");
                    response = response.Replace("<Erreur>", "");
                }
                else
                {
                    mButton.SetActive(true);
                    mSchema.SetActive(false);
                }
                mText.gameObject.SetActive(true);
                mText.text = response;
            }
        }));
    }

}
