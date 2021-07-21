using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DistructibleTilemap : MonoBehaviour
{
    public Tilemap destructableTilemap;
    public LayerMask layermask;
    private Color blue;

    void Start()
    {
        destructableTilemap = GetComponent<Tilemap>();
        blue = Color.blue;
        
    }
    void Update()
    {
        Vector3 mousepos = Input.mousePosition;
        mousepos.z = Camera.main.nearClipPlane;
        Vector3 worldposition = Camera.main.ScreenToWorldPoint(mousepos);
        RaycastHit2D hitData = Physics2D.Raycast(new Vector2(worldposition.x,worldposition.y),Vector2.zero,0,layermask);
        if(hitData&&Input.GetMouseButtonDown(0)){        
            destructableTilemap.SetColor(destructableTilemap.WorldToCell(worldposition),blue);
        }

    }

    
    
}

    
    
    

