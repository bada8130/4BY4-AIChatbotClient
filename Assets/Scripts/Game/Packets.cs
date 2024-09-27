using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LoginResponse
{
    public string message;
    public int user_id;
}

[System.Serializable]
public class Item
{
    public int item_id;
    public int quantity;
}

[System.Serializable]
public class GetAllItemsResponse
{
    public List<Item> items;
    public string message;
}
