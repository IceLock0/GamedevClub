using System.Text.RegularExpressions;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;
using UnityEngine.Windows;

public class UIInventoryDeleteButtonHandler : MonoBehaviour
{
    private Inventory _inventory => Inventory.instance;
    [SerializeField] private UIInventoryItem _uiInventoryItem;

    public void OnClick()
    {
        string parentName = transform.parent.name;
        string pattern = @"\d+";
        Match match = Regex.Match(parentName, pattern);
        int slotNumber; 
        var res = int.TryParse(match.Value, out slotNumber);
        slotNumber = res ? slotNumber : 0;
        _inventory.Remove(slotNumber);
        _uiInventoryItem.Refresh(_inventory.GetItem(slotNumber));
    }
  
}