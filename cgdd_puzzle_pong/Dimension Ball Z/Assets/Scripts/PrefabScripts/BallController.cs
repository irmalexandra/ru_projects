using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class BallController : MonoBehaviour
{
    public Rigidbody2D body;
    public Vector2 direction;
    public float speed;
    public Vector2 levelBounds;
    public Vector2 startPosition;
    public float defaultTrailTime;

    private bool _thrustOnCooldown;
    private bool _thrustSoundCd;
    private float _thrustStaminaCost = 0.0115f;
    public TrailRenderer trail;

    public float thrustPower;
    public float thrustStaminaCost = 1f;

    public Light2D pointLight;
    private float _pointLightIntensity;
    
    public Light2D paraLight;
    private float _paraLightIntensity;

    public Light2D flash;
    private Color _originalColor;

    private float _horizontal;
    private float _vertical;
    private bool _flashGreen;
    private bool _flashing;

    private bool _gameOver;

    public bool trailDisabled;

    void Start()
    {
        trail = GetComponent<TrailRenderer>();
        trail.time = 0;
        startPosition = transform.position;
        body.velocity = direction.normalized * speed;
        _originalColor = paraLight.color;
        _pointLightIntensity = pointLight.intensity;
        _paraLightIntensity = paraLight.intensity;
    }

    private void Update()
    {
        CooldownTrigger();
        ChangeLights();

        if (!GameManager.Instance.IsPaused())
        {
            if (!trailDisabled) ProcessInputs();
        }
        BoundCheck();
        if (_gameOver)
        {
            GameManager.Instance.TriggerGameOverMenu();
            _gameOver = false;
        }
    }

    private void BoundCheck()
    {
        if (!(transform.position.x < -levelBounds.x) && !(transform.position.x > levelBounds.x) &&
            !(transform.position.y < -levelBounds.y) && !(transform.position.y > levelBounds.y)) return;
        
        StartCoroutine(ResetTrailRenderer());
        if (GameManager.Instance.extraBalls > 0)
        {
            GameManager.Instance.extraBalls--;
            BallCounter.loseLife(GameManager.Instance.extraBalls);
        } else
        {
            _gameOver = true;
            transform.position = startPosition;
            
            return;
        }
        
        transform.position = startPosition;
        body.velocity = direction.normalized * speed;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 reDirection = GetComponent<Rigidbody2D>().velocity;

        StartCoroutine(Flash());
        if (other.gameObject.CompareTag("Paddle"))
        {
            SoundManager.PlaySoundEffect("PaddleHit");
        }
        else
        {
            SoundManager.PlaySoundEffect("BallHit");

            if (other.gameObject.CompareTag("ButtonFace"))
            {
                Vector3 otherBoost = other.transform.up * 2f;
                reDirection += new Vector2(otherBoost.x, otherBoost.y);
            }
        }
        reDirection = reDirection.normalized;
        reDirection *= speed;
        GetComponent<Rigidbody2D>().velocity = reDirection;
    }

    private void ProcessInputs()
    {
        _horizontal = 0;
        _vertical = 0;
        if (Input.GetKey(KeyCode.A))
        {
            _horizontal = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _horizontal = 1;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            _vertical = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _vertical = -1;
        }
        if (_thrustOnCooldown)
        {
            if (trail.time > 0f) trail.time -= 0.005f;
            if (Input.GetKeyDown(KeyCode.A) 
                || Input.GetKeyDown(KeyCode.S) 
                || Input.GetKeyDown(KeyCode.W) 
                || Input.GetKeyDown(KeyCode.D))
            {
                SoundManager.PlaySoundEffect("Cooldown");
            }
        }
        else
        {
            if (_vertical == 0 && _horizontal == 0)
            {
                if (trail.time > 0f) trail.time -= 0.005f;
                return;
            }
            if (!StaminaBar.instance.UseStamina(thrustStaminaCost)) return;
            if (!trailDisabled) trail.time = defaultTrailTime;
            Thrust2();
        }
    }

    private void Thrust2()
    {
        var bodyVelocity = body.velocity;
        Vector2 position = transform.position;
        
        Vector2 target = new Vector2(position.x+_horizontal, position.y+_vertical);
        
        var newDirection = Vector2.LerpUnclamped(bodyVelocity.normalized, (target-position).normalized, thrustPower);

        body.velocity = newDirection*speed;
    }

    private void Thrust()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 fromMouseToBall = mousePos - new Vector2(transform.position.x, transform.position.y);

        var newDirection = Vector2.LerpUnclamped(body.velocity.normalized, fromMouseToBall.normalized, _thrustStaminaCost);

        body.velocity = newDirection * speed;
    }

    private void CooldownTrigger()
    {
        if (StaminaBar.instance.GetStamPercentage() <= 0f)
        {
            _thrustOnCooldown = true;
        }
        else if (StaminaBar.instance.GetStamPercentage() <= 0.99f)
        {
            if (_flashGreen) return;
            _flashGreen = true;

        }
        else if (StaminaBar.instance.GetStamPercentage() >= 0.99f)
        {
            if (!_thrustOnCooldown) return;
            _thrustOnCooldown = false;
            _flashGreen = true;
        }
    }

    private void ChangeLights()
    {
        if(_flashing) return;
        pointLight.intensity = 1 * StaminaBar.instance.GetStamPercentage();
        if (StaminaBar.instance.GetStamPercentage() < 1f)
        {
            pointLight.color =
                Color.LerpUnclamped(Color.red, _originalColor, 1 * StaminaBar.instance.GetStamPercentage());
            paraLight.color =
                Color.LerpUnclamped(Color.red, _originalColor, 1 * StaminaBar.instance.GetStamPercentage());
            flash.color =
                Color.LerpUnclamped(Color.red, _originalColor, 1 * StaminaBar.instance.GetStamPercentage());

            trail.startColor = pointLight.color;
        }
        else if (_flashGreen)
        {
            StartCoroutine(ColorStatusUpdate());
            _flashGreen = false;
        }
    }
    
    private IEnumerator ColorStatusUpdate()
    {
        ChangeLightsColor(Color.green);
        SoundManager.PlaySoundEffect("ThrustReady");
        yield return new WaitForSeconds(0.3f);
        ChangeLightsColor(_originalColor);
    }
    
    private void ChangeLightsColor(Color color)
    {
        pointLight.color = color;
        paraLight.color = color;
        flash.color = color;
    }

    private IEnumerator Flash()
    {
        _flashing = true;
        paraLight.intensity += 2f;
        flash.intensity = 3f;
        yield return new WaitForSeconds(0.1f);
        _flashing = false;
        paraLight.intensity = _paraLightIntensity;
        pointLight.intensity = _pointLightIntensity;
        flash.intensity = 0f;
    }
    
    public IEnumerator ResetTrailRenderer()
    {
        trailDisabled = true;
        trail.time = 0;
        yield return new WaitForSeconds(0.05f);
        trailDisabled = false;
    }
}