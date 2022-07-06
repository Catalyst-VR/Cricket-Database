using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class RetrievePlayerData : MonoBehaviour
{

    private string baseUrl = "https://cwc-vr-backend-mock.herokuapp.com/game-session";


    private string playerUrl = "";

    // Start is called before the first frame update
    public void GetData()
    {

        playerUrl = "/" + pairingCode.text;

        StartCoroutine(GetData_Coroutine());

    }

    IEnumerator GetData_Coroutine()
    {       
        using (UnityWebRequest request = UnityWebRequest.Get(baseUrl + playerUrl))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                print(request.error);
            else
                print(request.responseCode);
            print(request.downloadHandler.text);
                playerData.text = request.downloadHandler.text;
        }
    }


    [SerializeField]
    TMP_InputField pairingCode;

    [SerializeField]
    TMP_InputField playerData;
}
