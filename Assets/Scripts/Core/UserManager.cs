using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserManager : SingletonMono<UserManager>
{
    public string MyAccount { get; private set; }
    public int MyAccountID { get; private set; }


    public List<Item> items { get; private set; } = new List<Item>();

    [SerializeField]
    public EventSO_Void ItemListRefresh;


    public void ReqLogin(string tryAccount, string tryPw, Action<string> errorCallback)
    {
        NetworkManager.Instance.Login(tryAccount, tryPw, (isSuccess, result) =>
        {
            if (isSuccess)
            {
                // JSON을 LoginResponse 클래스로 파싱
                LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(result);

                // 파싱한 데이터를 사용
                Debug.Log("Message: " + loginResponse.message);
                Debug.Log("User ID: " + loginResponse.user_id);

                MyAccount = tryAccount;
                MyAccountID = loginResponse.user_id;

                ReqItemList(() =>
                {
                    SceneManager.LoadScene(1);
                });
                //SceneManager.LoadScene(1);
            }
            else
            {
                errorCallback?.Invoke("로그인 실패 : " + result);
            }
        });
    }


    public void ReqItemList(Action callback)
    {
        NetworkManager.Instance.GetItemAll(MyAccountID, (isSuccess, result) =>
        {
            if (isSuccess)
            {
                // JSON을 LoginResponse 클래스로 파싱
                GetAllItemsResponse res = JsonUtility.FromJson<GetAllItemsResponse>(result);

                items = res.items;

                SceneManager.LoadScene(1);
            }

            callback?.Invoke();
        });
    }

    public void ReqUseItem(int _itemId, int _quantity, Action callback)
    {
        NetworkManager.Instance.UseItem(MyAccountID, _itemId, _quantity, (isSuccess, result) =>
               {
                   if (isSuccess)
                   {
                       for (int i = 0; i < items.Count; i++)
                       {
                           if (items[i].item_id == _itemId)
                           {
                               items[i].quantity = Mathf.Max(0, items[i].quantity - _quantity);
                           }
                       }
                   }

                   callback?.Invoke();
               });

    }
}