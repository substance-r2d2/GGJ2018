using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// MoveToClickPoint.cs
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveToClickPoint : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit;

            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if(hit.collider != null)
            {
                CustomTileData tileData = hit.collider.GetComponent<CustomTileData>();
                if (tileData != null)
                    Debug.LogError("custom tile data");
            }
        }
    }
}