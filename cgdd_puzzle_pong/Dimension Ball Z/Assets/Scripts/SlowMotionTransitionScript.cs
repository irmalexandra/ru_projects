using UnityEngine;

public class SlowMotionTransitionScript : MonoBehaviour
{
    private Rigidbody2D _ballBody;

    public float slowMotionSpeed;

    private Vector2 _originalVelocity;
    private Vector2 _slowMotionVelocity;

    public float timer;

    private float _timer;

    private bool _active;
    // Start is called before the first frame update
    void Start()
    {
        _ballBody = GameObject.FindWithTag("Ball").GetComponent<Rigidbody2D>();
        _timer = 0;
    }
    

    private void FixedUpdate()
    {
        if (!_active) return;
        if (_timer < 0)
        {
            _ballBody.velocity = _originalVelocity;
            _active = false;
        }
        else
        {
            _timer -= Time.deltaTime;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Debug.Log("Triggering SlowMotion");
            SlowMotionTrigger();
            GameManager.Instance.SwitchPaddle(transform.gameObject.GetComponentsInChildren<PaddleController>());
        }
    }

    public void SlowMotionTrigger()
    {
        _originalVelocity = _ballBody.velocity;
        _slowMotionVelocity = _originalVelocity.normalized * slowMotionSpeed;
        _ballBody.velocity = _slowMotionVelocity;

        _timer = timer;
        _active = true;
        SoundManager.PlaySoundEffect("SlowMotion");
    }
}
