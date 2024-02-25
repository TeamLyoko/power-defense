using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckStatus());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator CheckStatus() 
    {
        yield return new WaitForSeconds(5f);
        Debug.Log(APIRequestHandler.IsAuthenticated());
    }
}
