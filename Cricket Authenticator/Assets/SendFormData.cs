using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class SendFormData : MonoBehaviour
{

    private string baseUrl = "https://cwc-vr-backend-mock.herokuapp.com/game-session";



    private Form playerForm = new Form();

    Dictionary<string, string> form = new Dictionary<string, string>();

    public void SendForm()
    {

        playerForm.pairingCode = pairingCode.text;

        player.Add("name", playerName.text);
        player.Add("email", email.text);
        player.Add("dob", dayDOB.value.ToString() + "/" + monthDOB.value.ToString() + "/" + yearDOB.value.ToString());


        teams.Add("player", playerTeam.value);

        teams.Add("opponent", enemyTeam.value);


        StartCoroutine(PostData_Coroutine());

    }

    public Dictionary<string, string> player = new Dictionary<string, string>();

    public Dictionary<string, int> teams = new Dictionary<string, int>();


    public class Form
    {
        

        public string pairingCode;

        public string player;

        public string teams;


    }

    

    IEnumerator PostData_Coroutine()
    {
        playerForm.player = JsonUtility.ToJson(player);
        playerForm.teams = JsonUtility.ToJson(teams);

        string myData = JsonUtility.ToJson(playerForm);
        
        print(myData);
        

        using (UnityWebRequest request = UnityWebRequest.Post(baseUrl, myData))
        {

            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                print(request.error);
            else
                print(request.responseCode);
                print(request.downloadHandler.text);
        }
    }


    public string customData;

    //IEnumerator PostFormInfo()
    //{


    //    yield return new WaitForFixedUpdate();

    //    // Request has to be from scratch since UnityWebRequest.Post url-encodes the request body but Lambda needs it as a JSON string
    //    UnityWebRequest request = new UnityWebRequest(baseUrl);
    //    request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes());
    //    request.downloadHandler = new DownloadHandlerBuffer();
    //    request.method = UnityWebRequest.kHttpVerbPOST;
    //    request.SetRequestHeader("Content-Type", "application/json");

    //    try
    //    {
    //        Debug.Log("Sending data to server.");
    //        //yield return request.SendWebRequest();
    //        request.SendWebRequest();
    //        Debug.Log(request.responseCode == 200 ? "Success" : "Failure");
    //        Debug.Log(request.downloadHandler.text);
    //    }
    //    catch (WebException e)
    //    {
    //        Debug.LogError("POST WebRequest failed. --> " + e);
    //    }

    //}



    // Update is called once per frame



    [SerializeField]
    TMP_InputField pairingCode;

    [SerializeField]
    TMP_InputField playerName;

    [SerializeField]
    TMP_InputField email;

    [SerializeField]
    TMP_Dropdown dayDOB;

    [SerializeField]
    TMP_Dropdown monthDOB;

    [SerializeField]
    TMP_Dropdown yearDOB;

    [SerializeField]
    TMP_Dropdown playerTeam;

    [SerializeField]
    TMP_Dropdown enemyTeam;


    string pairingCodeStr;

    string playerNameStr;
    string emailStr;

    string DOB;

    int playerTeamStr;
    int enemyTeamStr;
}
