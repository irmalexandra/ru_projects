using System;
using System.Collections;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject deathCanvas;

    public GameObject player;
    public GameObject playerGhost;
    public GameObject[] enemies;
    [HideInInspector]
    public bool hasDied;
    [Serializable]
    public struct SpawnPoints {
        public string name;
        public Transform spawnPoint;
    }
    public SpawnPoints[] spawnPoints;
    
    private Vector3 _checkPointPosition;
    private SpriteRenderer _playerSpriteRenderer;
    private PlayerController _playerController;
    private PlayerMovement _playerMovement;
    private Rigidbody2D _playerRigidBody;

    private bool _isDead = false;


    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
        String playerSpawnPointKey = (PlayerPrefs.GetString("door"));
        if (playerSpawnPointKey != "")
        {
            foreach (var point in spawnPoints)
            {
                if (point.name == playerSpawnPointKey)
                {
                    player.transform.position = point.spawnPoint.position;
                    break;
                }
            }
        }
        
        _checkPointPosition = player.transform.position;
    }


    void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 7); // Ceiling check layer and Enemy layer
        Physics2D.IgnoreLayerCollision(9, 7); // Grid layer and Enemy layer
        Physics2D.IgnoreLayerCollision(10, 10); // Ignore self layer and Ignore self layer
        Physics2D.IgnoreLayerCollision(7, 10); // Enemy layer and Ignore self layer
        Physics2D.IgnoreLayerCollision(0, 7); // Default layer and Enemy layer
        Physics2D.IgnoreLayerCollision(8, 7); // Stairs layer and Enemy layer

        _playerRigidBody = player.gameObject.GetComponent<Rigidbody2D>();
        _playerSpriteRenderer = player.gameObject.GetComponent<SpriteRenderer>();
        _playerController = player.gameObject.GetComponent<PlayerController>();
        _playerMovement = player.gameObject.GetComponent<PlayerMovement>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        PlayerPrefs.SetString("hunted", "false");
  
    }

    public void flipGhostType()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
            enemy.transform.parent.GetChild(1).gameObject.SetActive(true);
            enemy.transform.parent.GetChild(1).GetComponent<BaseGhostAI>().hunting = true;
            playerGhost.GetComponent<BaseGhostAI>().hunting = true;
        }
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public PlayerController getPlayerController()
    {
        return _playerController;
    }

    public GameObject getPlayer()
    {
        return player;
    }    
    public GameObject getPlayerGhost()
    {
        return playerGhost;
    }

    public void respawnGhost(float ghostRespawnTimer, GameObject target)
    {
        StartCoroutine(RespawnTimerCoroutine(ghostRespawnTimer, target));
    }
    
    private IEnumerator RespawnTimerCoroutine(float duration, GameObject target)
    {
        float startTime = Time.time;
        bool done = false;
        while(!done)
        {
            float perc;
            perc = Time.time - startTime;
            if(perc > duration)
            {
                done = true;
            }
            yield return null;
        }
        target.SetActive(true);
    }
    
    public void Reset()
    {
        hasDied = true;
        playerGhost.GetComponent<BaseGhostAI>().ResetPlayerGhost();
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<BaseGhostAI>().Reset();
        }
        _playerController.Revive();
    }

    public void DisplayDeathCanvas(bool show)
    {
        if (show)
        {
            deathCanvas.SetActive(true);
        }
        else
        {
            deathCanvas.SetActive(false);
        }
       
    }

    public void setCheckpoint(Vector3 newPos)
    {
        _checkPointPosition = newPos;
    }

    public Vector3 getCheckpointPosition()
    {
        return _checkPointPosition;
    }
}
