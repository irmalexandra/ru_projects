using UnityEngine;

public class Portal : MonoBehaviour
{
    private GameObject ball;
    private Rigidbody2D[] portals;
    public float timer;

    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        portals = transform.parent.gameObject.GetComponentsInChildren<Rigidbody2D>();
        if (!PlayerPrefs.HasKey("timer"))
        {
            PlayerPrefs.SetFloat("timer", 0);
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetFloat("timer") > 0)
        {
            PlayerPrefs.SetFloat("timer", PlayerPrefs.GetFloat("timer") - Time.deltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Ball") && !other.CompareTag("PhysicsObject")) return;
        if (!(PlayerPrefs.GetFloat("timer") < 0.0001f)) return;
        SoundManager.PlaySoundEffect("PortalSoundEffect");
        
        if (other.CompareTag("Ball"))
        {
            if (other.GetComponent<TrailRenderer>().time > 0)
            {
                StartCoroutine(other.GetComponent<BallController>().ResetTrailRenderer());
            }
        }
        if (portals[0] == GetComponent<Rigidbody2D>())
        {
            other.transform.position = portals[1].transform.position;
        }
        else if (portals[1] == GetComponent<Rigidbody2D>())
        {
            other.transform.position = portals[0].transform.position;
        }
        PlayerPrefs.SetFloat("timer", timer);
    }
}
