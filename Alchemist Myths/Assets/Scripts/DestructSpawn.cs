using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructSpawn : MonoBehaviour
{
    public Tilemap destructibleTilemap;
    public LayerMask targetLayerMask;
    private Vector3 mousePosition;
    public float health = 10;
    public float damage = 2;
    private RaycastHit2D hit;
    private void Awake() 
    {
        destructibleTilemap = GetComponent<Tilemap>();
    }
    void Start()
    {
        StartCoroutine(BreaktheBlock());
    }
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        hit = Physics2D.Raycast(new Vector2(mouseWorldPosition.x,mouseWorldPosition.y),Vector2.zero,0,targetLayerMask);
    }
    IEnumerator BreaktheBlock()
    {
        Debug.Log("coroutine starts");
        while (true)
        {
            Debug.Log("check");
            if(hit&&Input.GetMouseButtonDown(0))
            {
                health -= damage;
                Debug.Log(health);
            }
            yield return new WaitForSeconds(10);
        }
    }
}
