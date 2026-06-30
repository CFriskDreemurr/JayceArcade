using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] private List<GameObject> neutral;
    [SerializeField] private List<GameObject> buy;

    private bool state;

    void Awake()
    {
        Events.OnItemBought += ItemBoughtFromShop;
    }
    

    private void ItemBoughtFromShop()
    {
        StartCoroutine(WaitAfterBlush(2));
    }

    private void SetupBuy()
    {
        foreach(GameObject go in neutral)
        {
            go.SetActive(false);
        }

        foreach(GameObject go in buy)
        {
            go.SetActive(true);
        }
    }

    private void SetupNeutral()
    {
        foreach (GameObject go in neutral)
        {
            go.SetActive(true);
        }

        foreach (GameObject go in buy)
        {
            go.SetActive(false);
        }
    }
    
    IEnumerator WaitAfterBlush(float seconds)
    {
        SetupBuy();
        yield return new WaitForSeconds(seconds);
        SetupNeutral();
    }

    private void OnDisable()
    {
        Events.OnItemBought += ItemBoughtFromShop;
    }
}
