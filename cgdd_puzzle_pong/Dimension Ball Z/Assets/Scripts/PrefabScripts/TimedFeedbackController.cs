using UnityEngine;

public class TimedFeedbackController : MonoBehaviour
{

    public GameObject text;
    public float timer;
    public float _timer;
    void Start()
    {
        _timer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.W))
        {
            _timer = timer;
        }
        else if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }

        else
        {
            text.SetActive(true);
        }
    }
}
