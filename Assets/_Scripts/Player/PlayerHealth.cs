using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //  Settings
    [SerializeField] private float startingHealth;
    public float Health { get; set; }

    //  References
    private Rigidbody2D rb;

    void Start()
    {
        Health = startingHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Health <= 0)
        {
            playerDeath();
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += resetPlayerHealth;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= resetPlayerHealth;
    }

    void playerDeath()
    {
        GameManager.Instance.OnGameEnd.Invoke();
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Health--;
            UIManager.Instance.RemoveHealth();
            Camera.main.GetComponent<CameraMove>().StartMoving();
            GameManager.Instance.OnHealthLoss?.Invoke();
        }
    }

    void resetPlayerHealth()
    {
        Health = startingHealth;
    }
}
