using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructSpawn : MonoBehaviour
{
    public Tilemap destructibleTilemap;
    public LayerMask targetLayerMask;
    private Vector3 mousePosition;
    public float health;
    private void Awake() 
    {
        destructibleTilemap = GetComponent<Tilemap>();
        targetLayerMask = LayerMask.GetMask("Destructible");
    }
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(mouseWorldPosition.x,mouseWorldPosition.y),Vector2.zero,0,targetLayerMask);
        if(hit&&Input.GetMouseButtonDown(0))
        {
            
        }
    }
}
