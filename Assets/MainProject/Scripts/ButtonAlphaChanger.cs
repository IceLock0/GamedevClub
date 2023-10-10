using UnityEngine.UI;
using UnityEngine;

public class ButtonAlphaChanger : MonoBehaviour
{
    private Button _fireButton;
    private Transform _target;

    private void Update()
    {
        _target = TargetSystem._closestEnemy;
        ChangeFireButtonTransparencyAndInteractable();
    }

    private void ChangeFireButtonTransparencyAndInteractable()
    {
        Color currentColor = _fireButton.image.color;
        if (_target != null )
        {
            currentColor.a = 1f;
            _fireButton.interactable = true;
        }
        else
        {
            currentColor.a = 0.5f;
            _fireButton.interactable = false;
        }
    }

    public void Init()
    {
        _fireButton = GetComponent<Button>();
    }
}
