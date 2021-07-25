using UnityEngine;

public class BallCopyController : MonoBehaviour
{
    private Vector3 _originalPosition;

    public float timer;
    private bool _collided;

    private float _timer;

    private Rigidbody2D _rigidbody2D;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _timer = timer;
        _originalPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (_collided)
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;

            }
            else
            {
                _timer = timer;
                _collided = false;
                transform.position = _originalPosition;
                _rigidbody2D.velocity = new Vector2(0, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _collided = true;
    }
}
