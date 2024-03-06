using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProfileManager : MonoBehaviour
{
    private ProfileDataHandler.PlayerProfileData personalData;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SaveProfileChanges", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        ProfileDataHandler.DataLoadedEvent += FillPlayerProfile;
    }

    private void OnDisable()
    {
        ProfileDataHandler.DataLoadedEvent -= FillPlayerProfile;
    }

    void FillPlayerProfile()
    {
        personalData = ProfileDataHandler.instance.GetProfilePersonalData();
    }

    void SaveProfileChanges()
    {
        personalData.lastname = "Qwerty";
        ProfileDataHandler.instance.UpdateProfileData(personalData);
    }
}
