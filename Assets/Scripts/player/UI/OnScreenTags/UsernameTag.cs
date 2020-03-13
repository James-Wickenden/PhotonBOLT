using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UsernameTag : MonoBehaviour
{
    Username username;

    [SerializeField]
    private float positionOffset;

    // Update is called once per frame
    public void setUsername(Username username)
    {
        Debug.Log("tag set user name");
        this.username = username;
        username.OnUsernameSet += updateTagText;
    }

    public void updateTagText(string text)
    {
        GetComponent<Text>().text = text;
        Debug.Log("text updated: " + text);
    }

    private void LateUpdate()
    {
        transform.position = Camera.main.WorldToScreenPoint(this.username.transform.position + Vector3.up * positionOffset);
    }
}
