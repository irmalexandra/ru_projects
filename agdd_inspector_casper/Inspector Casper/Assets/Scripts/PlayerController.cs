using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
	[HideInInspector] public bool hunted;
	[SerializeField] private float jumpForce = 400f; // Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField]
	private float crouchSpeed = .36f; // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f; // How much to smooth out the movement
	[SerializeField] private bool airControl; // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask whatIsGround; // A mask determining what is ground to the character
	[SerializeField] private Transform groundCheck; // A position marking where to check if the player is grounded.
	[SerializeField] private Transform ceilingCheck; // A position marking where to check for ceilings
	[SerializeField] private Collider2D crouchDisableCollider; // A collider that will be disabled when crouching
	private float mayJump;
	
	private const float GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	
	
	
	public GameObject interactiveButton;
	private GameObject speechBubble;
	private SpriteRenderer speechRenderer;
	public TextMeshPro bubbleTextBox;
	public List<string> deathTags;
	public GameObject personalBlood;
	public bool grounded; // Whether or not the player is grounded.


	public PhysicsMaterial2D glue;
	public PhysicsMaterial2D slippery;
	[HideInInspector]
	public FlashController flashController;

	private const float CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private new Rigidbody2D rigidbody2D;
	private bool facingRight = true; // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;
	private Collider2D[] _colliders;
	
	private bool _fallingThroughGround;
	private Collider2D _ceiling;
	private Collider2D _ground;
	private BloodSplatter _bloodScript;
	
	private bool _fixing = false;
	private PlatformEffector2D _effector;
	
	public bool _alive = true;
	public bool _nervous = false;
	public bool insideSafeZone = false;
	private bool firstDeath = true;

	public GameObject inventoryHUD;
	public bool hasFinalKey = false;
	private int keyImageOffsetX = -340;
	private int keyImageOffsetY = -150;
	private Dictionary<string, bool> keysHeld = new Dictionary<string, bool>();

	[Header("Events")] [Space] public UnityEvent onLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool>
	{
	}

	public BoolEvent onCrouchEvent;
	private bool wasCrouching;

	private void Awake()
	{
		_fallingThroughGround = false;
		_colliders = GetComponentsInChildren<Collider2D>();
		_bloodScript = personalBlood.GetComponent<BloodSplatter>();
		
		rigidbody2D = GetComponent<Rigidbody2D>();
		if (onLandEvent == null)
			onLandEvent = new UnityEvent();

		if (onCrouchEvent == null)
			onCrouchEvent = new BoolEvent();
		
		
	}

	private void Start()
	{
		flashController = GetComponentInChildren<FlashController>();
	}

	private void Update()
	{
		if (Input.GetButtonDown("Fire1") && _alive)
		{
			flashController.CameraFlash();
		}
		if (Input.GetKey(KeyCode.R) && !_alive && GameManager.instance.deathCanvas.activeSelf)
		{
			GameManager.instance.DisplayDeathCanvas(false);
			GameManager.instance.Reset();
		}
	}

	private void FixedUpdate()
	{


		bool wasGrounded = grounded;
		grounded = false;
		

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, GroundedRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{

				if (!colliders[i].gameObject.CompareTag("Enemy"))
				{
					if (!wasGrounded)
						onLandEvent.Invoke();
				}
				grounded = true;
			}
		}
	}

	private void Flip()
	{
		Vector3 button = interactiveButton.transform.localScale;
		button.x *= -1;
		interactiveButton.transform.localScale = button;
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		if (bubbleTextBox != null)
		{
			var quaternion = bubbleTextBox.GetComponentInChildren<RectTransform>().localScale;
			quaternion.x *= -1;
			bubbleTextBox.GetComponentInChildren<RectTransform>().localScale = quaternion;
		}
		

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		
		
	}


	public void Move(float move, bool crouch, bool jump)
	{
		if (Physics2D.OverlapCircle(ceilingCheck.position, CeilingRadius, whatIsGround))
		{
			_ceiling = Physics2D.OverlapCircle(ceilingCheck.position, CeilingRadius, whatIsGround);
		}
		else
		{
			_ceiling = null;
		}

		if (Physics2D.OverlapCircle(groundCheck.position, GroundedRadius, whatIsGround))
		{
			_ground = Physics2D.OverlapCircle(groundCheck.position, GroundedRadius, whatIsGround);
		}
		else
		{
			_ground = null;
		}

		/*if (_ceiling && _ceiling.CompareTag("Stairs"))
		{

			foreach (Collider2D collider in _colliders)
			{
				Physics2D.IgnoreCollision(collider, _ceiling, true);
			}
		}*/

		/*
		if (!_fallingThroughGround && _ground && _ground.CompareTag("Stairs") && !grounded )
		{
			Debug.Log("what?");
			Debug.Log(groundCheck.position.y > _ground.transform.position.y);
			
			foreach (Collider2D collider in _colliders)
			{
				Physics2D.IgnoreCollision(collider, _ground, false);
			}
		}*/





		if (_ground && _ground.CompareTag("Stairs") && move == 0f)
		{
			//rigidbody2D.mass = 0;
			//rigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;
			var thing = rigidbody2D.sharedMaterial;
			thing.friction = 10000f;
			rigidbody2D.sharedMaterial = thing;
		}
		else
		{
			/*Debug.Log("grounded: ");
			Debug.Log(grounded);*/
			/*Debug.Log("falling through ground:");
			Debug.Log(_fallingThroughGround);*/
			/*Debug.Log("crouch: ");
			Debug.Log(crouch);
			Debug.Log("jumpo");
			Debug.Log(jump);
			Debug.Log("move");
			Debug.Log(move);*/
			/*rigidbody2D.constraints = RigidbodyConstraints2D.None;
			rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;*/
			var thing = rigidbody2D.sharedMaterial;
			thing.friction = 0f;
			rigidbody2D.sharedMaterial = thing;
		}
		
		// If crouching, check to see if the character can stand up
		/*if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (_ceiling && !_ceiling.CompareTag("Stairs"))
			{
				crouch = true;
			}
		}*/


		//only control the player if grounded or airControl is turned on
		if (grounded || airControl)
		{

			// If crouching
			if (crouch)
			{
				if (!wasCrouching)
				{
					wasCrouching = true;
					onCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= crouchSpeed;

				// Disable one of the colliders when crouching
				if (crouchDisableCollider != null)
					crouchDisableCollider.enabled = false;
			}
			else
			{
				// Enable the collider when not crouching
				if (crouchDisableCollider != null)
					crouchDisableCollider.enabled = true;

				if (wasCrouching)
				{
					wasCrouching = false;
					onCrouchEvent.Invoke(false);
				}
			}

			if (Input.GetKey(KeyCode.S))
			{
				//_fallingThroughGround = true;
				/*foreach (var collider in _colliders)
				{
					Physics2D.IgnoreCollision(_ground, collider, true);
				}*/
				/*_effector = _ground.GetComponent<PlatformEffector2D>();
				if (_effector)
				{
					_effector.rotationalOffset += 180;
				}*/
				// Debug.Log(_ground+"this is ground");
				if (_ground)
				{
					StairsController stairsController = _ground.GetComponent<StairsController>();
					// Debug.Log(stairsController+ "this is stairscontroller");
					
					if (stairsController)
					{
						// Debug.Log("Stairs Controller is not null");
						stairsController.flip_effector();
					}
				}
			
			}

			/*if (_effector && _effector.rotationalOffset > 0)
			{
				if (!_fixing)
				{
					StartCoroutine(fix_platform(_effector));
				}
			}*/
			
			
		
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			rigidbody2D.velocity =
				Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !facingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && facingRight)
			{
				// ... flip the player, like a burger.
				Flip();
			}


			// If the player should jump...

			if (grounded)
			{
				mayJump = 0.2f;
			}

			mayJump -= Time.deltaTime;
			if (mayJump > 0 && jump)
			{
				// Add a vertical force to the player.
				mayJump = 0;
				grounded = false;
				Vector2 newVelocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
				rigidbody2D.velocity = newVelocity;
				//rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			}
		}
	}
	/*if (_ground && _ground.CompareTag("Stairs") && !_fallingThroughGround)
	{
		foreach (Collider2D collider in _colliders)
		{
			Physics2D.IgnoreCollision(collider, _ground, false);
		}
	}*/

	private IEnumerator fix_platform(PlatformEffector2D effector)
	{
		_fixing = true;
		Debug.Log(_fixing);
		yield return new WaitForSeconds(0.5f);
		effector.rotationalOffset -= 180;
		_fixing = false;
		Debug.Log(_fixing);

	}
	
	public void showInteractiveButton(bool set)
	{
		interactiveButton.SetActive(set);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		//  Debug.Log("player box collider:" + other.transform.name);
		if (!deathTags.Contains(other.gameObject.tag) || !_alive) return;
		
		GameManager.instance.getPlayerGhost().SetActive(false);
		
		SoundManager.PlaySoundEffect("Death");
		_alive = false;
		_bloodScript.spawnBlood();
		transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		StartCoroutine(Wait());
	}


	public void Revive()
	{
		Vector3 spawnPosition = GameManager.instance.getCheckpointPosition();
		transform.position = spawnPosition;
		_alive = true;
		
		speechBubble = GameObject.FindGameObjectWithTag("SpeechBubble");
		if (speechBubble)
		{
			speechRenderer = speechBubble.gameObject.GetComponent<SpriteRenderer>();
		}

		if (!speechRenderer.enabled)
		{
			if (firstDeath)
			{
				firstDeath = false;
				speechRenderer.enabled = true;
				bubbleTextBox.text = "I'm alive! How can this be? Though it feels as though a part of me is missing...";
				StartCoroutine(RespawnWait());

			}
			else
			{
				if (insideSafeZone)
				{
					speechRenderer.enabled = true;
					bubbleTextBox.text = "The blue flame seems to protect my soul.";
					StartCoroutine(RespawnWait());

				}
			}
		}
	}

	static IEnumerator WaitForScreamCoroutine(float duration)
	{
		yield return new WaitForSeconds(duration);
		SoundManager.PlaySoundEffect("Scream");
	}
	
	private IEnumerator Wait()
	{
		yield return new WaitForSeconds(1);
		
		GameManager.instance.DisplayDeathCanvas(true);
	}

	private IEnumerator RespawnWait()
	{
		yield return new WaitForSeconds(7);
		speechRenderer.enabled = false;
		bubbleTextBox.text = "";
	}

	public void takeKey(string keyName, Sprite keySprite, Color keyColor)
	{
		keysHeld.Add(keyName, true);
		SoundManager.PlaySoundEffect("KeyPickup");
		if (keyName == "FinalKey")
		{
			hasFinalKey = true;
			MusicManager.Instance.StopMusic();
			StartCoroutine(WaitForScreamCoroutine(2));
		}
		addKeyToHUD(keyName, keySprite, keyColor);
	}

	private void addKeyToHUD(string keyName, Sprite keySprite, Color keyColor)
	{
		GameObject newObj = new GameObject();
		newObj.SetActive(true);
		Image newImage = newObj.AddComponent<Image>();

		newObj.transform.name = keyName;
		var transfrom = newObj.GetComponent<RectTransform>();
		transfrom.SetParent(inventoryHUD.transform);
		var scale = transfrom.localScale;
		scale.x = 0.25f;
		scale.y = 0.25f;
		scale.z = 0.25f;
		transfrom.localScale = scale;
		
		newImage.sprite = keySprite;
		newImage.color = keyColor;
		
		var newPos = new Vector3(keyImageOffsetX, keyImageOffsetY, 0);
		transfrom.localPosition = newPos;
		
		keyImageOffsetX += 40;
	}

	public Dictionary<string, bool> GetKeys()
	{
		return keysHeld;
	}
}

	

	