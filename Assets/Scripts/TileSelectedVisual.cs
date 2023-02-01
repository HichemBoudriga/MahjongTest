using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileSelectedVisual : MonoBehaviour
{

    private SpriteRenderer  m_spriteRenderer;
    private SelectableTile m_selectableTile;



    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_selectableTile = GetComponentInParent<SelectableTile>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_selectableTile.OnSelected += SelectableTile_OnSelected;

        m_spriteRenderer.enabled = false;
    }




    private void SelectableTile_OnSelected(object sender, bool isSelected)
    {
        m_spriteRenderer.enabled = isSelected;
    }



}
