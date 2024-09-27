using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : SingletonMono<NetworkManager>
{
    public string baseUrl = "http://127.0.0.1:5000"; // Flask 서버 주소 (localhost와 포트 번호)

    [SerializeField]
    private GameObject LoadingUI;

    // 공통 POST 요청 처리 함수
    private IEnumerator PostRequest<T>(string endpoint, T postData, System.Action<bool, string> callback)
    {
        string url = $"{baseUrl}/{endpoint}";
        string jsonData = JsonUtility.ToJson(postData);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            LoadingUI?.SetActive(true);
            yield return request.SendWebRequest();
            LoadingUI?.SetActive(false);

            if (request.result == UnityWebRequest.Result.Success)
            {
                var result = request.downloadHandler.text;
                Debug.Log($"{endpoint} Response: " + result);
                callback(true, result); // 요청 성공
            }
            else
            {
                Debug.LogError($"{endpoint} Error: " + request.error);
                callback(false, request.error); // 요청 실패
            }
        }
    }

    // 유저 로그인 함수 (코루틴 호출 내부 처리)
    public void Login(string username, string password, System.Action<bool, string> callback)
    {
        LoginData loginData = new LoginData
        {
            username = username,
            password = password
        };
        // 외부에서 사용할 때는 StartCoroutine을 내부적으로 호출
        StartCoroutine(PostRequest("login", loginData, callback));
    }

    // 아이템 지급 함수 (코루틴 호출 내부 처리)
    public void GiveItem(int userId, int itemId, int quantity, System.Action<bool, string> callback)
    {
        GiveItemData itemData = new GiveItemData
        {
            user_id = userId,
            item_id = itemId,
            quantity = quantity
        };

        // 외부에서 사용할 때는 StartCoroutine을 내부적으로 호출
        StartCoroutine(PostRequest("give_item", itemData, callback));
    }

    public void GetItemAll(int userId, System.Action<bool, string> callback)
    {
        GetItemAll sendData = new GetItemAll
        {
            user_id = userId,
        };
        // 외부에서 사용할 때는 StartCoroutine을 내부적으로 호출
        StartCoroutine(PostRequest("get_all_items", sendData, callback));
    }

    public void UseItem(int _userId, int _itemId, int _quantity, System.Action<bool, string> callback)
    {
        UseItem sendData = new UseItem
        {
            user_id = _userId,
            item_id = _itemId,
            quantity = _quantity,
        };
        // 외부에서 사용할 때는 StartCoroutine을 내부적으로 호출
        StartCoroutine(PostRequest("use_item", sendData, callback));
    }





}

// 로그인 데이터 구조체
[System.Serializable]
public class LoginData
{
    public string username;
    public string password;
}

// 아이템 지급 데이터 구조체
[System.Serializable]
public class GiveItemData
{
    public int user_id;
    public int item_id;
    public int quantity;
}

// 로그인 데이터 구조체
[System.Serializable]
public class GetItemAll
{
    public int user_id;
}


// 아이템 지급 데이터 구조체
[System.Serializable]
public class UseItem
{
    public int user_id;
    public int item_id;
    public int quantity;
}