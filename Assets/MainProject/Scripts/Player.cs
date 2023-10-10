using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
    private Health _hp;
    public void ApplyDamage(float damage)
    {
        _hp.ChangeHealth(damage);
    }

    private void Update()
    {
        if (!gameObject)
        {
            Debug.Log("reload");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }

    }

    public void Init()
    {
        _hp = GetComponent<Health>();
    }
}
