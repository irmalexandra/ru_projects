using System;
using UnityEngine;

namespace AGDDPlatformer
{
    public class PlayerGoal : MonoBehaviour
    {
        public string playerTag;
        public bool isSatisfied;

        public GameObject satisfactionIndicator;
        public AudioSource source;

        void Start()
        {
            satisfactionIndicator.SetActive(false);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(playerTag))
            {
                isSatisfied = true;
                satisfactionIndicator.SetActive(true);
                source.Play();
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(playerTag))
            {
                isSatisfied = false;
                satisfactionIndicator.SetActive(false);
            }
        }
    }
}
