using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _restartDelay = 2f;

    private Player _player;

    private void Update()
    {
        if (!_player)
        {
            Time.timeScale = 0f;
            StartCoroutine(RestartScene());
        }
    }

    private IEnumerator RestartScene()
    {
        yield return new WaitForSecondsRealtime(_restartDelay);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Init()
    {
        _player = FindAnyObjectByType<Player>();
    }
}
