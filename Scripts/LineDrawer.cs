using Shapes;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    private Polyline polyline;

    void Start()
    {
        polyline = gameObject.GetComponent<Polyline>();
        polyline.points.Clear();
    }
    private void Update()
    {
        if(Schema.mIsDragging)
        {
            updateMousePoint();
        }
    }
    private Vector2 getMousePoint()
    {
        RectTransform ui_element = transform.parent.GetComponent<RectTransform>();

        Vector2 mousePosition = Input.mousePosition;
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(ui_element, mousePosition, Camera.main, out Vector2 localMousePosition);

        //Vector2 mousePosition = Input.mousePosition;
        //RectTransform ui_element = GetComponent<RectTransform>();
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(ui_element, mousePosition, null, out Vector2 localMousePosition);

        return localMousePosition;
    }

    private bool isMousePoint(Vector3 pos)
    {
        return pos != new Vector3(0, 0, -1f);
    }

    private void updateMousePoint()
    {
        Vector3 mouse = getMousePoint();
        //if (!isMousePoint(mouse)) return;

        polyline.SetPointPosition(polyline.points.Count - 1, mouse);
        //PolylinePoint point = polyline.points[polyline.points.Count - 1];
        //Debug.Log("Update");
        //Debug.Log(mouse);
        //Debug.Log(point.point);
        //point.point = mouse;
        //Debug.Log(point.point);
        //polyline.UpdateMesh(true);
    }

    private void addMouseAsPoint()
    {
        Vector3 mouse = getMousePoint();
        Debug.Log(mouse);
        //if (!isMousePoint(mouse)) return;
        polyline.AddPoint(mouse, polyline.points[^1].thickness);
    }

    public void addPoint(Vector3 point, float thickness)
    {
        if(polyline.points.Count > 0)
        {
            polyline.SetPointPosition(polyline.points.Count - 1, point);
        }
        else
        {
            polyline.AddPoint(point, thickness);
        }
        addMouseAsPoint();
        //Debug.Log(polyline.points.Count);
        //if(polyline.points.Count == 1)
        //{
        //}
    }

    public void clear()
    {
        polyline.points.Clear();
        polyline.UpdateMesh(true);
    }
}