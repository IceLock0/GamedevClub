using UnityEngine;
using UnityEngine.UI;

public class TargetSystem : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;

    public static Transform _closestEnemy { get; private set; }

    private Transform _player;
    private Camera _playerCamera;
    private Collider2D[] _enemyColliders = new Collider2D[0];

    private void Update()
    {
        FindClosestEnemy();
    }

    private void FindClosestEnemy()
    {
        Vector2 size = CalculateCameraSize();
        _enemyColliders = Physics2D.OverlapBoxAll(_player.position, size, 0f, _enemyLayer);

        if (_enemyColliders.Length > 0)
        {
            _closestEnemy = _enemyColliders[0].transform;
            float closestDistance = Vector3.Distance(_player.position, _closestEnemy.position);
            foreach (Collider2D enemy in _enemyColliders)
            {
                float distance = Vector3.Distance(_player.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    _closestEnemy = enemy.transform;
                }
            }
        }
        else _closestEnemy = null;
    }

    private Vector2 CalculateCameraSize()
    {
        Vector2 bottomLeft = _playerCamera.ViewportToWorldPoint(new Vector3(0, 0, _playerCamera.nearClipPlane));
        Vector2 topRight = _playerCamera.ViewportToWorldPoint(new Vector3(1, 1, _playerCamera.nearClipPlane));
        Vector2 size = topRight - bottomLeft;
        return size;
    }

    public void Init()
    {
        _playerCamera = Camera.main;
        _player = GameObject.Find("Player").GetComponent<Transform>();
    }

}