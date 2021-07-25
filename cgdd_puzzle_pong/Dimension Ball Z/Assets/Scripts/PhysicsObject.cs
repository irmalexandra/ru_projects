using System.Linq;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public GameObject[] toAvoid;
    public float timer;
    public float divisor;
    private float _timer;
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    
    public Vector2 levelBounds;
    public Vector2 startPosition;
 

    private void Start()
    {
        _timer = timer;
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        BoundCheck();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (toAvoid.Contains(other.gameObject))
        {
            _spriteRenderer.color = _originalColor;
            _timer = timer;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (toAvoid.Contains(other.gameObject))
        {
            if (_timer > 0 && !TimeManager.Instance.GetPaused())
            {
                _timer -= Time.fixedDeltaTime;
                Color currentColor = _spriteRenderer.color;
                _spriteRenderer.color = new Color(currentColor.r,
                    currentColor.g,
                    currentColor.b,
                    currentColor.a- (Time.fixedDeltaTime/divisor));
            }
            else
            {
                transform.position = _originalPosition;
                transform.rotation = _originalRotation;
                _spriteRenderer.color = _originalColor;
            }
        }
    }
    
    private void BoundCheck()
    {
        if (!(transform.position.x < -levelBounds.x) && !(transform.position.x > levelBounds.x) &&
            !(transform.position.y < -levelBounds.y) && !(transform.position.y > levelBounds.y)) return;
        transform.position = startPosition;
    }
}
