using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JoystickController : MonoBehaviour, IMoveable
{
    [SerializeField] private FixedJoystick _joystick;

    private float _moveSpeed = 3.0f;

    private Rigidbody2D _rb;

    private float _horizontalInput;
    private float _verticalInput;

    private bool _facingRight = true;

    public float moveSpeed => _moveSpeed;

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        _horizontalInput = _joystick.Horizontal * _moveSpeed;
        _verticalInput = _joystick.Vertical * _moveSpeed;

        _rb.velocity = new Vector3(_horizontalInput, _verticalInput, 0);

        Flip();
    }

    private void Flip()
    {
        if (_horizontalInput > 0 && !_facingRight || _horizontalInput < 0 && _facingRight)
        {
            _facingRight = !_facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    public void Init()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.freezeRotation = true;
        _rb.gravityScale = 0.0f;
    }
}
