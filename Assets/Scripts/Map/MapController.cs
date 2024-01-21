using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    CarInputHandler pm;

    [Header("Optimization")]
    // list of chunks -- can add original chunk in pre-start to optimize that as well
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDist; // must be > length and width of tilemap
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDur;

    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<CarInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker(){

        // fail safe
        if(!currentChunk){
            return;
        }

        // right

        if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius, terrainMask)){
            noTerrainPosition = currentChunk.transform.Find("Right").position;
            SpawnChunk();
        }
        
        // left
  
        if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius, terrainMask)){
            noTerrainPosition = currentChunk.transform.Find("Left").position;
            SpawnChunk();
        }
        
        // up
  
        if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkerRadius, terrainMask)){
            noTerrainPosition = currentChunk.transform.Find("Up").position;
            SpawnChunk();
        }
        
        // down

        if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius, terrainMask)){
            noTerrainPosition = currentChunk.transform.Find("Down").position;
            SpawnChunk();
        }
        
        // right up
 
        if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Up").position, checkerRadius, terrainMask)){
            noTerrainPosition = currentChunk.transform.Find("Right Up").position;
            SpawnChunk();
        }
        
        // right down
    
        if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Down").position, checkerRadius, terrainMask)){
            noTerrainPosition = currentChunk.transform.Find("Right Down").position;
            SpawnChunk();
        }
        
        // left up
 
        if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Up").position, checkerRadius, terrainMask)){
            noTerrainPosition = currentChunk.transform.Find("Left Up").position;
            SpawnChunk();
        }
        
        // left down

        if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Down").position, checkerRadius, terrainMask)){
            noTerrainPosition = currentChunk.transform.Find("Left Down").position;
            SpawnChunk();
        }
        

        // // right
        // if(pm.inputVector.x > 0 && pm.inputVector.y == 0){ 
        //     if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius, terrainMask)){
        //         noTerrainPosition = currentChunk.transform.Find("Right").position;
        //         SpawnChunk();
        //     }
        // }
        // // left
        // else if(pm.inputVector.x < 0 && pm.inputVector.y == 0){ 
        //     if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius, terrainMask)){
        //         noTerrainPosition = currentChunk.transform.Find("Left").position;
        //         SpawnChunk();
        //     }
        // }
        // // up
        // else if(pm.inputVector.x == 0 && pm.inputVector.y > 0){ 
        //     if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkerRadius, terrainMask)){
        //         noTerrainPosition = currentChunk.transform.Find("Up").position;
        //         SpawnChunk();
        //     }
        // }
        // // down
        // else if(pm.inputVector.x == 0 && pm.inputVector.y < 0){ 
        //     if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius, terrainMask)){
        //         noTerrainPosition = currentChunk.transform.Find("Down").position;
        //         SpawnChunk();
        //     }
        // }
        // // right up
        // else if(pm.inputVector.x > 0 && pm.inputVector.y > 0){ 
        //     if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Up").position, checkerRadius, terrainMask)){
        //         noTerrainPosition = currentChunk.transform.Find("Right Up").position;
        //         SpawnChunk();
        //     }
        // }
        // // right down
        // else if(pm.inputVector.x > 0 && pm.inputVector.y < 0){ 
        //     if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Down").position, checkerRadius, terrainMask)){
        //         noTerrainPosition = currentChunk.transform.Find("Right Down").position;
        //         SpawnChunk();
        //     }
        // }
        // // left up
        // else if(pm.inputVector.x < 0 && pm.inputVector.y > 0){ 
        //     if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Up").position, checkerRadius, terrainMask)){
        //         noTerrainPosition = currentChunk.transform.Find("Left Up").position;
        //         SpawnChunk();
        //     }
        // }
        // // left down
        // else if(pm.inputVector.x < 0 && pm.inputVector.y < 0){ 
        //     if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Down").position, checkerRadius, terrainMask)){
        //         noTerrainPosition = currentChunk.transform.Find("Left Down").position;
        //         SpawnChunk();
        //     }
        // }
        
    }

    void SpawnChunk(){
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer(){
        optimizerCooldown -= Time.deltaTime;
        if(optimizerCooldown <= 0f){
            optimizerCooldown = optimizerCooldownDur;
        }
        foreach(GameObject chunk in spawnedChunks){
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if(opDist > maxOpDist){
                chunk.SetActive(false);
            }
            else{
                chunk.SetActive(true);
            }
        }
    }
}
