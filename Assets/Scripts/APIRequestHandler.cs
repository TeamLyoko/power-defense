using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIRequestHandler : MonoBehaviour
{
    private string apiKey = "NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmM2OjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJiYw";
    private string jwtToken;
    private static bool authenticated = false;
    public static APIRequestHandler instance;

    private string LOGIN_URL = "http://20.15.114.131:8080/api/login";

    // Structure for reading JWT token
    [Serializable]
    public class JWTTokenResponse
    {
        public string token;
    }

    void Awake()
    {   // Make sure that this script is accessible across all scenes
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Authenticate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool IsAuthenticated() {
        return authenticated;
    }

    private static void SetAuthenticated(bool value) {
        authenticated = value;
    }

    public IEnumerator Authenticate()
    {
        string body = String.Format("{{ \"apiKey\": \"{0}\"}}", apiKey);
        
        using (UnityWebRequest www = UnityWebRequest.Post(LOGIN_URL, body, "application/json"))
        {
            // Send the request
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                JWTTokenResponse response = JsonUtility.FromJson<JWTTokenResponse>(www.downloadHandler.text);
                Debug.Log("Successfully Authenticated: " + response.token);
                jwtToken = response.token;
                APIRequestHandler.SetAuthenticated(true);
            }
            else
            {
                Debug.Log("Authentication Error: " + www.error);
            }
        }
    }

    public IEnumerator GetRequest(string uri, System.Action<string> callback)
    {
        if (!APIRequestHandler.IsAuthenticated()) {
            Debug.Log("Not authenticated, unable to send GET request");
            yield break;
        }

        using (UnityWebRequest www = UnityWebRequest.Get(uri))
        {
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + jwtToken);
            // Send the request
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("GET Response: " + www.downloadHandler.text);
                callback(www.downloadHandler.text);
            }
            else
            {
                Debug.Log("GET Error: " + www.error);
            }
        }
    }

    public IEnumerator PutRequest(string uri, string jsonBody)
    {
        if (!APIRequestHandler.IsAuthenticated()) {
            Debug.Log("Not authenticated, unable to send PUT request");
            yield break;
        }

        using (UnityWebRequest www = UnityWebRequest.Put(uri, jsonBody))
        {
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + jwtToken);
            www.SetRequestHeader("Content-Type", "application/json");
            // Send the request
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("PUT Response: " + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("PUT Error: " + www.error);
            }
        }
    }
}
