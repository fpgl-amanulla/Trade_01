using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create Trade Item", menuName = "New Trade Item")]
public class TradeItemSO : ScriptableObject
{
    public int itemId;
    public int starValue;
    public int numOfItem;
    public GameObject itemPrefab;
    public Sprite imgSprite;
}
