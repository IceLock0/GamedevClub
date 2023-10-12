using System.Collections;
using UnityEngine;


public class UIInventoryButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject _grid;

    public GameObject Grid => _grid;

    public void ChangeActiveState()
    {
        _grid.SetActive(!_grid.activeSelf);

    }
}
