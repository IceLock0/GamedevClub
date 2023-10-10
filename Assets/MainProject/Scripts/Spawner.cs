using UnityEngine;


public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _monsterPrefabArray;
    [SerializeField] private int _monstersCount;
    [SerializeField] private float _spawnDistance;


    void Start()
    {
        for (int i = 0; i < _monstersCount; i++)
        {
            int randomPrefabIndex = Random.Range(0, _monsterPrefabArray.Length);
            Vector2 randomOffset = Random.insideUnitSphere * _spawnDistance;
            Vector2 spawnPosition = (Vector2)transform.position + randomOffset;
            Instantiate(_monsterPrefabArray[randomPrefabIndex], spawnPosition, Quaternion.identity);
        }
    }
}
