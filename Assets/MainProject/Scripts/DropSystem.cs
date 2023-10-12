using System.Collections.Generic;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
    [SerializeField] private List <GameObject> _itemPrefabList;

    private void OnDisable()
    {
        Drop();
    }

    public void Drop()
    {
        var rPrefab = Random.Range(0, _itemPrefabList.Count);
        Instantiate(_itemPrefabList[rPrefab], transform.position, Quaternion.identity);
    }

}
