using UnityEngine;

public class Player : MonoBehaviour
{
	private Vector3 direction;
	private SpriteRenderer spriteRenderer;
	public Sprite[] sprites;
	private int spriteIndex;
	public float gravity = -9.8f; // Allows us to customize the gravity on the bird in the inspecter
	public float strength = 5f;


	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Start()
	{
		InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
	}

	private void OnEnable()
	{
		Vector3 position = transform.position;
		position.y = 0;
		transform.position = position;
		direction = Vector3.zero;
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
		{
			direction = Vector3.up * strength;
		}

		// Handle mobile touch input
		if(Input.touchCount > 0)
		{
			Touch touch  = Input.GetTouch(0);

			if(touch.phase == TouchPhase.Began) // If user JUST touched the screen
			{
				direction = Vector3.up * strength;
			}
		}

		direction.y += gravity * Time.deltaTime;
		transform.position += direction * Time.deltaTime; // Update position
	}

	// Function created to animate the sprite of the bird
	private void AnimateSprite()
	{
		spriteIndex++;

		if(spriteIndex >= sprites.Length)
		{
			spriteIndex = 0;
		}

		spriteRenderer.sprite = sprites[spriteIndex];
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Obstacle")
		{
			FindObjectOfType<GameManager>().GameOver(); // Expensive Function to call for game optimziation
		}

		if(other.gameObject.tag == "Scoring")
		{
			FindObjectOfType<GameManager>().IncreaseScore();
		}
	}
}
