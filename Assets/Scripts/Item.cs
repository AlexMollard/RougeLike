using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public int ID;
    new public string name = "new item";
    public Sprite icon;
    public int stackAmount = 99;

    public virtual void Use()
    {

    }
}
