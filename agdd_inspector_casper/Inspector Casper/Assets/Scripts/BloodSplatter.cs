using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BloodSplatter : MonoBehaviour
{
    public GameObject bloodDrop;
    public bool splash = false;
    public int amount;
    public float  destroyAfter = 3.0f;
    public bool dripper = false;
    public float dripPerSec = 1.0f;
    public List<Collider2D> ignorables;
    public float velocityHigh;
    public float velocityLow;
    

    private float _timer = 0f;
    private List<GameObject> _bloodList;
    


    


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            spawnBlood();
        }
        else if (dripper)
        {
            if (_timer <= 0)
            {
                spawnBlood();
                _timer = dripPerSec;
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }
    }

    private Vector2 get_direction()
    {
        float randomx = Random.Range(velocityLow, velocityHigh);
        float randomy = Random.Range(velocityLow, velocityHigh);
        return new Vector2(randomx, randomy);
    }
    

    public void spawnBlood()
    {
        for (int i = 0; i < amount; i++)
        {
            
            GameObject cell = Instantiate(bloodDrop);
            foreach (Collider2D ignorable in ignorables)
            {
                Physics2D.IgnoreCollision(cell.GetComponent<Collider2D>(), ignorable);
            }
            cell.transform.position = transform.position;
            if (splash)
            {
                cell.GetComponent<Rigidbody2D>().velocity = (get_direction());
            }
            StartCoroutine(destroyCell(cell));
        }
    }

    private IEnumerator destroyCell(GameObject cell)
    {
        yield return new WaitForSeconds(destroyAfter);
        Destroy(cell);
    }
}
