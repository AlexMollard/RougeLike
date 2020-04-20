using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{

    public Item item;

    [HideInInspector]
    public Sprite invIcon;
    [HideInInspector]
    public int maxStack = 99;

    int ID = 1;
    string itemName;
    int amount = 1;
    float DespawnTime = 10.0f;
    float lifeTimeMultiplier;
    float currentLifeTime = 1.0f;
    TileBehaviour tile;
    bool isOnGround = false;

    private void Start()
    {
        itemName = item.name;
        invIcon = item.icon;
        maxStack = item.stackAmount;
        ID = item.ID;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnGround)
            OnGround();
    }

    public void SpawnOnGround(TileBehaviour tile)
    {
        this.tile = tile;
        isOnGround = true;
        tile.hasItem = true;
        currentLifeTime = 2.0f;
        lifeTimeMultiplier = 1.0f / (DespawnTime);
    }

    void OnGround()
    {
        currentLifeTime -= Time.deltaTime * lifeTimeMultiplier;
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, currentLifeTime);


        if (currentLifeTime <= 0.0f)
        {
            tile.hasItem = false;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player)
        {
            if (player.PickUp(this))
            {
                if (tile)
                    tile.hasItem = false;
            }
        }
    }

    private void OnMouseOver()
    {
        MouseOverBehaviour.SetText(itemName);
    }

    public int GetAmount()
    {
        return amount;
    }

    public void AddAmount(int toAdd)
    {
        amount += toAdd;
    }

    public void SetAmount(int newAmount)
    {
        amount = newAmount;
    }

    public int GetID()
    {
        return ID;
    }

    public void SetID(int newID)
    {
        ID = newID;
    }

    public TileBehaviour GetTile()
    {
        return tile;
    } 

    public void SetTile(TileBehaviour newTile)
    {
        tile = newTile;
    }

    public string GetName()
    {
        return itemName;
    }

}
