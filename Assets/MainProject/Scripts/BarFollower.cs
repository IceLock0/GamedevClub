using System.Collections;
using UnityEngine;


public class BarFollower : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - _target.transform.position;
    }

    private void Update()
    {
        if(_target.activeSelf == false)
        {
            gameObject.SetActive(false);
        }    
        else transform.position = _target.transform.position + _offset;
    }
}
