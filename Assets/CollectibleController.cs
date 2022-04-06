using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    public delegate void ItemMissEvent();

    private GameObject player;
    private PlayerAgent agent;

    public Renderer itemRenderer;

    private bool hasAppeared;

    public bool isDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        itemRenderer = GetComponent<Renderer>();
        agent = player.GetComponent<PlayerAgent>();
        hasAppeared = false;
    }

    private void Update()
    {
        Vector2 itemScreenPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        if (itemScreenPosition.y < 0)
        {
            OnItemMiss();
            Destroy(gameObject);
        }
    }

    public void OnItemMiss()
    {
        agent.OnItemMissed();
        Destroy(this.gameObject);
        isDestroyed = true;
    }

}
