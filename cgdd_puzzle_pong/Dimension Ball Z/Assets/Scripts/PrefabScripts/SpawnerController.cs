using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject toSpawn;
    public int allowedAmount;
    public bool selfSpawn;
    public float selfSpawnTimer;
    private float _timer;
    private List<GameObject> _currentSpawns;


    private void Start()
    {
        _currentSpawns = new List<GameObject>();
        _timer = selfSpawnTimer;
    }

    public void Spawn()
    {
        GameObject spawn = Instantiate(toSpawn, transform);
        _currentSpawns.Add(spawn);
        if (_currentSpawns.Count > allowedAmount)
        {
            Destroy(_currentSpawns[0]);
            _currentSpawns.RemoveAt(0);
        }
    }


    private void Update()
    {
        if (selfSpawn)
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
            else
            {
                _timer = selfSpawnTimer;
                Spawn();
            }
        }
    }
}
