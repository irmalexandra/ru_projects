using System.Collections.Generic;
using UnityEngine;

public class BallCounter : MonoBehaviour
{
    // Start is called before the first frame update
    
    static List<GameObject> _lives;
    public GameObject life1, life2, life3;
    

    public void Start()
    {
        _lives = new List<GameObject> {life1, life2, life3};
    }



    public static void loseLife(int extraBalls)
    {
        _lives[extraBalls].gameObject.SetActive(false);
    }
}
