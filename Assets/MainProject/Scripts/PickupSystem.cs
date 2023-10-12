using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    [SerializeField] private ItemInfo _pantsInfo;
    [SerializeField] private ItemInfo _proofInfo;

    private UIInventoryHandler _uiIInventoryHandler;
    public Inventory inventory => Inventory.instance;

    private int _dropLayerMask = 6;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _dropLayerMask)
        {
            AddToInventory(collision);
            Destroy(collision.gameObject);
        }
    }

    private void AddToInventory(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Pants":
                {
                    var pants = new BanditPants(_pantsInfo);
                    pants.state.amount = 1;
                    inventory.TryToAdd(pants);
                    break;
                }
            case "Proof":
                {
                    var proof = new BulletproofCloak(_proofInfo);
                    proof.state.amount = 1;
                    inventory.TryToAdd(proof);
                    break;
                }
        }
    }

}
