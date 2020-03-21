using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsernameContoller : MonoBehaviour
{
    [SerializeField]
    private UsernameTag usernameTagPrefab;

    private Dictionary<Username, UsernameTag> usernameTags = new Dictionary<Username, UsernameTag>();

    private void Awake()
    {
        Username.OnUsernameAdded += AddUsername;
        Username.OnUsernameRemoved += RemoveUsername;
    }

    private void AddUsername(Username username)
    {
        if (!usernameTags.ContainsKey(username))
        {
            var usernameTag = Instantiate(usernameTagPrefab, transform);
            usernameTag.setUsername(username);
            usernameTags.Add(username, usernameTag);
        }
    }

    private void RemoveUsername(Username username)
    {
        if (usernameTags.ContainsKey(username))
        {
            Destroy(usernameTags[username].gameObject);
            usernameTags.Remove(username);
        }
    }
}
