using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    public delegate void ItemMissEvent();

    private GameObject player;
    private PlayerAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        agent = player.GetComponent<PlayerAgent>();
    }

    private void Update()
    {
        Vector2 itemScreenPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        if (itemScreenPosition.y < 0)
        {
            Destroy(gameObject);
        }
    }


}
