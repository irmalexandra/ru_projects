using System;
using UnityEngine;

namespace AGDDPlatformer
{
    public class KinematicObject : MonoBehaviour
    {
        [Header("Settings")]
        public float minGroundNormalY = 0.65f;
        public float gravityModifier = 1;

        [Header("Info")]
        public Vector2 velocity;
        public bool isGrounded;

        protected Vector2 groundNormal;
        protected Rigidbody2D body;
        protected ContactFilter2D contactFilter;
        protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];

        protected const float minMoveDistance = 0.001f;
        protected const float shellRadius = 0.01f;

        public bool isFrozen;

        protected void OnEnable()
        {
            body = GetComponent<Rigidbody2D>();
            body.isKinematic = true;
        }

        protected void OnDisable()
        {
            body.isKinematic = false;
        }

        protected void Start()
        {
            contactFilter.useTriggers = false;
            contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
            contactFilter.useLayerMask = true;
        }

        protected void FixedUpdate()
        {
            if (isFrozen)
                return;

            velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;

            isGrounded = false;

            Vector2 deltaPosition = velocity * Time.deltaTime;
            Vector2 groundVector = new Vector2(groundNormal.y, -groundNormal.x);
            Vector2 groundMove = groundVector * deltaPosition.x;
            PerformMovement(groundMove, false);

            Vector2 airMove = Vector2.up * deltaPosition.y;
            PerformMovement(airMove, true);
        }

        void PerformMovement(Vector2 move, bool yMovement)
        {
            float distance = move.magnitude;

            if (distance > minMoveDistance)
            {
                //check if we hit anything in current direction of travel
                int count = body.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
                for (int i = 0; i < count; i++)
                {
                    Vector2 currentNormal = hitBuffer[i].normal;

                    //is this surface flat enough to land on?
                    if ((gravityModifier >= 0 && currentNormal.y > minGroundNormalY) ||
                        (gravityModifier < 0 && currentNormal.y < -minGroundNormalY))
                    {
                        isGrounded = true;
                        // if moving up, change the groundNormal to new surface normal.
                        if (yMovement)
                        {
                            groundNormal = currentNormal;
                            currentNormal.x = 0;
                        }
                    }

                    if (isGrounded)
                    {
                        //how much of our velocity aligns with surface normal?
                        var projection = Vector2.Dot(velocity, currentNormal);
                        if (projection < 0)
                        {
                            //slower velocity if moving against the normal (up a hill).
                            velocity -= projection * currentNormal;
                        }
                    }
                    else
                    {
                        //We are airborne, but hit something, so cancel vertical up and horizontal velocity.
                        if (gravityModifier >= 0 && currentNormal.y < -0.01f)
                        {
                            velocity.y = Mathf.Min(velocity.y, 0);
                        }

                        if (gravityModifier < 0 && currentNormal.y > 0.01f)
                        {
                            velocity.y = Mathf.Max(velocity.y, 0);
                        }

                        if (Mathf.Sign(currentNormal.x) != Mathf.Sign(velocity.x))
                        {
                            velocity.x = 0;
                        }
                    }

                    //remove shellDistance from actual move distance.
                    var modifiedDistance = hitBuffer[i].distance - shellRadius;
                    distance = modifiedDistance < distance ? modifiedDistance : distance;
                }
            }

            body.position += move.normalized * distance;
        }
    }
}
