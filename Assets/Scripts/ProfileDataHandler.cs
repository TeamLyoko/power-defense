using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileDataHandler : MonoBehaviour
{
    public static ProfileDataHandler instance;

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

    private PlayerProfileData personalData;
    public delegate void DataLoaded();
    public static event DataLoaded DataLoadedEvent;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetProfilePersonalData(string response)
    {
        UserData profile = JsonUtility.FromJson<UserData>(response);
        personalData = profile.user;

        if (DataLoadedEvent != null) {
            DataLoadedEvent();  // Notify that profile data has been loaded
        }
    }

    public PlayerProfileData GetProfilePersonalData()
    {
        Debug.Log("Name: " + personalData.firstname + " " + personalData.lastname);
        Debug.Log("NIC: " + personalData.nic);
        Debug.Log("Phone Number: " + personalData.phoneNumber);
        Debug.Log("Email: " + personalData.email);
        Debug.Log("Profile Picture: " + personalData.profilePictureUrl);

        return personalData;
    }

    public void LoadProfileData()
    {
        StartCoroutine(APIRequestHandler.instance.GetRequest("http://20.15.114.131:8080/api/user/profile/view", SetProfilePersonalData));
        SceneManager.LoadScene("PlayerProfile");
    }

    public void UpdateProfileData(PlayerProfileData data) 
    {
        personalData = data;
        string updateBody = JsonUtility.ToJson(personalData);
        StartCoroutine(APIRequestHandler.instance.PutRequest("http://20.15.114.131:8080/api/user/profile/update", updateBody));
    }
}
