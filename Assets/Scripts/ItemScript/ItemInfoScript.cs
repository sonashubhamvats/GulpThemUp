using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemName
{
    _2xMultiplier,
    _3xMultiplier,
    RainOfLife,
    AmmoPack,
    NectarOfTheGods,
    GunsOfRetribution,
}
[System.Serializable]
public struct ItemInfo
{
    public ItemName _name;

    public int unlocks_at;

    public int cost;

    public int maximumAllowed;
    

}