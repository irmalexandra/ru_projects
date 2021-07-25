using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class InteractablesController : MonoBehaviour
{

    public bool toggle;
    public bool selfClose;
    public float timeToClose;
    
    public GameObject[] ignorables;
    private bool _toggled;

    public GameObject[] multiButtons;
    private bool _activated;

    private void Start()
    {
        
        foreach (var item in ignorables)
        {
            Physics2D.IgnoreCollision(item.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        
    }
    
    public void Signal()
    {
        if (toggle)
        {
            Toggle();
        }
        else
        {
            NotToggle();
        }
  
    }


    private void Toggle()
    {
        if (!_toggled)
        {
            _toggled = true;
            Activate();
        }
    }

    private void NotToggle()
    {
        Activate();
    }


    private void Activate()
    {
           
        SoundManager.PlaySoundEffect("PositiveFeedback");
        if (CompareTag("Door"))
        {
            StartCoroutine(OpenDoor());
        }
        if (CompareTag("Pear"))
        {
            StartCoroutine(FlashGreen());
        }
        if (CompareTag("Pusher"))
        {
            StartCoroutine(Push());
        }

        if (CompareTag("Spawner"))
        {
            GetComponent<SpawnerController>().Spawn();
        }
        
        if (CompareTag("MultiDoor"))
        {
            var open = true;
            foreach (var button in multiButtons)
            {
                if (!button.GetComponent<ButtonController>().pressed)
                {
                    open = false;
                }
            }

            if (open)
            {
                OpenDoor();
                _activated = true;
            }

            if (_activated && !open)
            {
                OpenDoor();
                _activated = false;
            }
        }

        if (CompareTag("WinPortal"))
        {
            ToggleActive();
        }

    }

    private void ToggleActive()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        
    }

    private IEnumerator Push()
    {
        JointMotor2D sliderMotor = gameObject.GetComponent<SliderJoint2D>().motor;
        sliderMotor.motorSpeed *= -1;
        gameObject.GetComponent<SliderJoint2D>().motor = sliderMotor;

        if (!toggle)
        {
            yield return new WaitForSeconds(0.5f);
        
            sliderMotor = gameObject.GetComponent<SliderJoint2D>().motor;
            sliderMotor.motorSpeed *= -1;
            gameObject.GetComponent<SliderJoint2D>().motor = sliderMotor;
        }
    }
    
    private IEnumerator OpenDoor()
    {
        HingeJoint2D hinge = gameObject.GetComponent<HingeJoint2D>();
        JointMotor2D direction = hinge.motor;
        Rigidbody2D door = gameObject.GetComponent<Rigidbody2D>();
        door.constraints = RigidbodyConstraints2D.None;
        direction.motorSpeed *= -1;
        hinge.motor = direction;

        if (selfClose)
        {
            yield return new WaitForSeconds(timeToClose);
            hinge = gameObject.GetComponent<HingeJoint2D>();
            direction = hinge.motor;
            door = gameObject.GetComponent<Rigidbody2D>();
            door.constraints = RigidbodyConstraints2D.None;
            direction.motorSpeed *= -1;
            hinge.motor = direction;
        }
    }

    private IEnumerator FlashGreen()
    {
        Light2D light = GetComponentInChildren<Light2D>();
        Color originalColor = light.color;
        light.color = Color.green;
        yield return new WaitForSeconds(0.3f);
        light.color = originalColor;
    }
}




