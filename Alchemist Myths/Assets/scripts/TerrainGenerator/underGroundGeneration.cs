using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class underGroundGeneration : MonoBehaviour//0不填(洞穴) 1填(石頭/礦物) 玩家生成
{
    //public GameObject player;

    [Header("underGround Gen")]
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] float smoothness;// 最好>=2
    int [] perlinHeightList;

    [Header("Cave Gen")]
    /*[Range(0,1)]
    [SerializeField] float modifier;*/
    [Range(0,100)]
    [SerializeField] int randomFillPercent;
    [SerializeField] int smoothAmount;

    [Header("Tile")]
    [SerializeField] TileBase groundTile;//自定義地形(Ruled Tile)
    [SerializeField] TileBase underGroundBG;
    //[SerializeField] TileBase topgroundTile;//草地
    [SerializeField] Tilemap groundTilemap;//要將生成的地形放入Grid內的哪個Tilemap?
    [SerializeField] Tilemap backGround;
    [SerializeField] int seed;//隨便調

    int[,] map;

    void Start(){
        perlinHeightList = new int[width];
        GenWorld();
        //playerSpawn();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G)){
            GenWorld();
        }
    }
    public void GenWorld(){
        groundTilemap.ClearAllTiles();
        map = GenerateArray(width,height,true);
        map = TerrenGeneration(map);
        SmoothMap(smoothAmount);
        RenderMap(map,groundTilemap,groundTile);//畫出地下
        RenderBG(backGround,underGroundBG);//畫出地下背景
        //Debug.Log(transform.position.x = 0);
    }
    public void playerSpawn(){
        
    }
    public int[,] GenerateArray(int width,int height,bool empty){//
        int[,] map= new int[width,height];
        for(int x=0;x< width;x++){
            for(int y=0;y<height;y++){
                map[x,y] = (empty) ? 1:2;
            }
        }

        return map;
    }
    
    public int[,] TerrenGeneration(int[,] map){
        System.Random pesudoRandom = new System.Random(seed.GetHashCode());//random value but accroding to seed
        int perlinHeight;
        for(int x=0;x< width;x++){
            perlinHeight = Mathf.RoundToInt(Mathf.PerlinNoise(x/smoothness,seed)*height);
            perlinHeightList[x]= perlinHeight;
            for(int y=0;y<perlinHeight;y++){
                //map[x,y] =1; //FOR DEBUG
                /*int caveValue = Mathf.RoundToInt(Mathf.PerlinNoise((x*modifier)+seed,(y*modifier)+seed));
                map[x,y]= (caveValue ==1) ? 1:2;*/
                map[x,y]= (pesudoRandom.Next(1,100)<randomFillPercent)? 1:2;
            }
        }
        return map;
    }
    void SmoothMap(int smoothAmount){

        for(int i=0;i<smoothAmount;i++){
            for(int x=0;x< width;x++){
                for(int y=0;y<perlinHeightList[x];y++){
                    if(y==perlinHeightList[x]-1||y==perlinHeightList[x]){   //上邊界
                        map[x,y] =2;
                    }else{
                        int surroundingGroundCount = GetSurroundingGroundCount(x,y);
                        if(surroundingGroundCount>4){
                            map[x,y]=1;
                        }
                        else if(surroundingGroundCount<4){
                            map[x,y] = 2;
                        }
                    }
                }
            }
        }

        
    }

    int GetSurroundingGroundCount(int gridX,int gridY){
        int groundCount =0;

        for(int nebX = gridX-1;nebX<=gridX+1;nebX++){
            for(int nebY =gridY-1;nebY<=gridY+1;nebY++){
                if(nebX>=0 && nebX<width && nebY>=0 && nebY<height){
                    if(nebX != gridX ||nebY != gridY){
                        if(map[nebX,nebY]==1){
                            groundCount++;
                        }
                    }
                }
            }
        }

        return groundCount;
    }    
    
    public void RenderMap(int[,] map,Tilemap groundTileMap,TileBase groundTilebase){
        for(int x=0;x< width;x++){
            for(int y=0;y<height;y++){
                if(map[x,y]==2){
                    groundTileMap.SetTile(new Vector3Int(x,y,0),groundTilebase);
                }
            }
        }
    }

    public void RenderBG(Tilemap backGround,TileBase BG){
        for(int x=0;x<width;x++){
            for(int y=0;y<height;y++){
                if(y<=perlinHeightList[x]-1){
                    backGround.SetTile(new Vector3Int(x,y,0),BG);
                }
                
            }
        }
    }
}
