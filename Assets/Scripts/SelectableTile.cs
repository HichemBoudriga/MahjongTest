
using UnityEngine;
using System;

public class SelectableTile : MonoBehaviour
{

    public EventHandler<bool> OnSelected; 

    private bool m_isSelected;
    private void OnMouseDown()
    {
        m_isSelected = !m_isSelected;
        OnSelected?.Invoke(this, m_isSelected);
    }

  

    public bool IsSelected()
    {
        return m_isSelected;
    }

    public void SetSelected(bool isSelected)
    {
        m_isSelected = isSelected;
    }
}
