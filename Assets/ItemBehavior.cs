using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public float lifeTimeInSeconds = 10.0f;
    float lifeTimeMultiplier;
    public float currentLifeTime = 1.0f;
    public TileBehaviour tile;
    public bool isOnGround = false;
    public int amount;
    public Sprite invIcon;
    public int ID = 1;
    public bool CanCutWood = false;
    public bool CanFish = false;

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
        lifeTimeMultiplier = 1.0f / (lifeTimeInSeconds);
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
}
