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
    public GameObject mine;
    private RaycastHit2D hit;
    private Color tileColor;
    private void Awake() 
    {
        destructibleTilemap = GetComponent<Tilemap>();
    }
    void Start()
    {
        StartCoroutine(BreakBlocks());
        tileColor = Color.white;
    }
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        hit = Physics2D.Raycast(new Vector2(mouseWorldPosition.x,mouseWorldPosition.y),Vector2.zero,0,targetLayerMask);
        if(health <= 0)
        {
            destructibleTilemap.SetTile(destructibleTilemap.WorldToCell(mouseWorldPosition),null);
            Instantiate(mine,mouseWorldPosition,Quaternion.identity);
            health = 10;
            //destructibleTilemap.SetColor(destructibleTilemap.WorldToCell(mouseWorldPosition), Color.red); this is not working
        }
    }
    
    IEnumerator BreakBlocks()
    {
        while (true)
        {
            yield return new WaitUntil(()=>hit&&Input.GetMouseButton(0));
            health--;
            Debug.Log(health);
            yield return StartCoroutine(Fade());
            yield return new WaitForSeconds(.6f);
        }
    }

    IEnumerator Fade()
    {
        tileColor.a -= 0.1f;
        Debug.Log(tileColor);
        yield return null;
    }
        
    
}
