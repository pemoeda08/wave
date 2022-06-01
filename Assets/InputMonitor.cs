using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputMonitor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var image = GetComponent<Image>();
        var playerAgent = GameObject.Find("Player")?.GetComponent<PlayerAgent>();
        if (playerAgent != null && image != null)
        {
            playerAgent.OnDiscreteActionReceived += (action) =>
            {
                if (action == 0) image.color = Color.red;
                else if (action == 1) image.color = Color.green;
                else image.color = Color.grey;
            };
        }
    }

}
