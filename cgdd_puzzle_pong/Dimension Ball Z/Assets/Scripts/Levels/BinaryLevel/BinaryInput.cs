using UnityEngine;

public class BinaryInput : MonoBehaviour
{
    // Start is called before the first frame update
    public int input;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "ButtonFace")
        {
            BinaryDisplay.Instance.Signal(input);
        }
    }
}
