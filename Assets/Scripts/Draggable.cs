using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Draggable : MonoBehaviour 
{
    [SerializeField]
    private bool m_isDraggable;

    private bool m_isDragged = false;
    private Vector3 m_screenPoint;

 
    //
    public bool IsDraggable()
    {
        return m_isDraggable;
    }

    public void SetDraggable(bool isDraggable )
    {
        m_isDraggable = isDraggable;
    }


    private void OnMouseDown()
    {
        if (!m_isDraggable)
            return;

        m_isDragged = true;
        m_screenPoint = Camera.main.WorldToScreenPoint(transform.position);

    }

    private void OnMouseDrag()
    {
        if (!m_isDraggable)
            return;

        if (m_isDragged)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
            transform.position = curPosition;
        }
    }

    //
    private void OnMouseUp()
    {
        if (!m_isDraggable)
            return;

        m_isDragged = false;
    }

}
