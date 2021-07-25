using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class ButtonController : MonoBehaviour
{
    public GameObject[] toActivate;
    public bool hold;
    public bool flash;
    private bool _open;
    public GameObject buttonFace;
    private Color _buttonFaceInvertedColor;
    private Color _buttonFaceOriginalColor;
    private Color _buttonFaceLightOriginalColor;
    private Color _buttonFaceLightInvertedColor;

    public bool pressed;

    private void Start()
    {
        _buttonFaceOriginalColor = buttonFace.GetComponent<SpriteRenderer>().color;
        _buttonFaceInvertedColor = Invertcolor(_buttonFaceOriginalColor);
        _buttonFaceLightOriginalColor = buttonFace.GetComponentInChildren<Light2D>().color;
        _buttonFaceLightInvertedColor = Invertcolor(_buttonFaceLightOriginalColor);
    }


    private Color Invertcolor(Color originalColor)
    {
        
        Color newColor = originalColor;
        Color.RGBToHSV(newColor, out float H, out float S, out float V);
        float negativeH = (H + 0.5f) % 1f;
        Color negativeColor = Color.HSVToRGB(negativeH, S, V);
        return negativeColor;
    }

    private void invertButtonFaceColors(Collision2D other)
    {
        if (buttonFace.GetComponent<SpriteRenderer>().color == _buttonFaceInvertedColor)
        {
            buttonFace.GetComponent<SpriteRenderer>().color = _buttonFaceOriginalColor;
            buttonFace.GetComponentInChildren<Light2D>().color = _buttonFaceLightOriginalColor;
        }
        else
        {
            buttonFace.gameObject.GetComponent<SpriteRenderer>().color = _buttonFaceInvertedColor;
            buttonFace.GetComponentInChildren<Light2D>().color = _buttonFaceLightInvertedColor;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "ButtonFace")
        {
            pressed = true;

            invertButtonFaceColors(other);
 
            foreach (var interactableObject in toActivate)
            {
                interactableObject.GetComponent<InteractablesController>().Signal();
            }

            if (flash)
            {
                StartCoroutine(Flash(other));
            }
        }
    }

    private IEnumerator Flash(Collision2D other)
    {
        yield return new WaitForSeconds(0.15f);
        invertButtonFaceColors(other);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (hold && other.gameObject.name == "ButtonFace")
        {
            pressed = false;
            
            invertButtonFaceColors(other);
            
            foreach (var interactableObject in toActivate)
            {
                interactableObject.GetComponent<InteractablesController>().Signal();
            }
        }

    }
}