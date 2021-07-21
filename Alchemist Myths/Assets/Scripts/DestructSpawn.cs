using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructSpawn : MonoBehaviour
{
    public Tilemap destructibleTilemap;
    public LayerMask targetLayerMask;
    public float health = 10;
    public float damage = 2;
    private RaycastHit2D hit;
    private Vector3 mouseWorldPosition;
    private Color tileColor;
    private void Awake() 
    {
        destructibleTilemap = GetComponent<Tilemap>();
    }
    void Start()
    {
        StartCoroutine(BreakBlocks());
    }
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        hit = Physics2D.Raycast(new Vector2(mouseWorldPosition.x,mouseWorldPosition.y),Vector2.zero,0,targetLayerMask);
        if(health <= 0)
            destructibleTilemap.SetTile(destructibleTilemap.WorldToCell(mouseWorldPosition),null);
    }
    
    IEnumerator BreakBlocks()
    {
        tileColor = destructibleTilemap.GetColor(destructibleTilemap.WorldToCell(mouseWorldPosition));
        while (true)
        {
            yield return new WaitUntil(()=>hit&&Input.GetMouseButton(0));
            health--;
            Debug.Log(health);
            yield return StartCoroutine(Fade());
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator Fade()
    {
        tileColor.a -= 0.1f;
        Debug.Log(tileColor);
        destructibleTilemap.SetColor(destructibleTilemap.WorldToCell(mouseWorldPosition),tileColor);
        yield return null;
    }
        
    
}
