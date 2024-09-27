using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemTable", menuName = "Inventory/ItemTable")]
public class ItemTable : ScriptableObject
{
    [SerializeField]
    public List<ItemInfo> itemList;
}
