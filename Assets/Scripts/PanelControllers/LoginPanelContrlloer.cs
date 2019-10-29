using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginPanelContrlloer : MonoBehaviour
{
   public void OnLoginButtonClick()
   {
        Debug.Log("OnLoginButtonClick");
        NetworkMgr.Instance.ConnectToServer();
    }

    private void Update()
    {
        if (NetworkMgr.Instance.isConnectedToServer)
        {
            Destroy(this.gameObject);
        }
    }
}
