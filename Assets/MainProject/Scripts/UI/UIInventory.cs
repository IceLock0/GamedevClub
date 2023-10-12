using System.Collections;
using UnityEngine;


public class UIInventory : MonoBehaviour
{
    [SerializeField] private ItemInfo _bulletproofCloakInfo;
    [SerializeField] private ItemInfo _banditPantsInfo;

    public Inventory inventory => _uiIInventoryHandler.inventory;

    private UIInventoryHandler _uiIInventoryHandler;

    private void Start()
    {
        var uiSlots = GetComponentsInChildren<UIInventorySlot>();
        _uiIInventoryHandler = new UIInventoryHandler(_bulletproofCloakInfo, _banditPantsInfo, uiSlots);
        _uiIInventoryHandler.FillSlots();
    }
}
