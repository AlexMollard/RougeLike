using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    public GameObject playerObject;
    public SpriteRenderer renderer;
    public GameObject log;
    public Vector2 pos;
    Vector2[] suroundingTiles = {
        new Vector2(-1,1), new Vector2(0, 1), new Vector2(1, 1), 
        new Vector2(-1,0), new Vector2(1, 0), 
        new Vector2(-1,-1), new Vector2(0, -1), new Vector2(1, -1) 
    }; 
    public List<List<GameObject>> tiles;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (playerObject.transform.position.y - 0.1f > transform.position.y)
        {
            renderer.sortingOrder = 2;
        }
        else
        {
            renderer.sortingOrder = -1;
        }
    }

    void DropItem()
    {
        int tileChance = Random.Range(0, 8);
        TileBehaviour tile = tiles[(int)pos.x + (int)suroundingTiles[tileChance].x][(int)pos.y + (int)suroundingTiles[tileChance].y].GetComponent<TileBehaviour>();

        if (!tile.hasItem)
        {
            GameObject currentLog = Instantiate(log, tile.transform.position, Quaternion.identity);
            currentLog.GetComponent<ItemBehavior>().SpawnOnGround(tile);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "HeldItem")
        {
            EquipedItemBehavior item = collision.GetComponent<EquipedItemBehavior>();
            if (item.currentObject == null)
                return;

            if (item.hit == false && !item.canUse && item.currentObject.GetComponent<ItemBehavior>().CanCutWood)
            {
                item.hit = true;
                DropItem();
            }
        }
        
    }

}
