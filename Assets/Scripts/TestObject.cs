using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    [Serializable]
    public class PlayerProfileData
    {
        public string firstname;
        public string lastname;
        public string nic;
        public string phoneNumber;
        public string email;
        public string profilePictureUrl;
    }
    [Serializable]
    public class UserData 
    {
        public PlayerProfileData user;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckStatus());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GetAndUpdatePlayer(string response) {
        UserData profile = JsonUtility.FromJson<UserData>(response);
        Debug.Log(profile.user.lastname);
        profile.user.lastname = "Gamer_1";
        string updateBody = JsonUtility.ToJson(profile.user);
        StartCoroutine(APIRequestHandler.instance.PutRequest("http://20.15.114.131:8080/api/user/profile/update", updateBody));
    }

    IEnumerator CheckStatus() 
    {
        StartCoroutine(APIRequestHandler.instance.GetRequest("http://20.15.114.131:8080/api/user/profile/view", GetAndUpdatePlayer));
        yield return null;
    }
}
