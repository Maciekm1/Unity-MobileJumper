using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour
{
    //  References
    private Rigidbody2D rb;
    private Grapple grapple;
    private Animator animator;
    [SerializeField] private Transform raycastPosition;

    //  Variables
    [SerializeField] private Vector2 jumpForce;
    [SerializeField] private float trailDuration = 0.15f;
    [SerializeField] private LayerMask layerMask;
    private bool isGrounded;

    private const string JUMP_ANIMATION = "PlayerJump";

    //  Particles
    [SerializeField] private ParticleSystem jumpPS;

    [SerializeField] private TrailRenderer jumpTrail;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        grapple = GetComponent<Grapple>();
        isGrounded = false;
        jumpTrail.emitting = false;
    }

    private void OnEnable()
    {
        InputManager.Instance.OnScreenSwipe += Jump;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnScreenSwipe -= Jump;
    }

    void FixedUpdate()
    {
        RaycastCheck();
    }

    private void RaycastCheck()
    {
        RaycastHit2D raycast = Physics2D.Raycast(raycastPosition.position, Vector2.down, 1f, layerMask);
        if (raycast.collider != null)
        {
            if (raycast.distance <= 0.2f)
            {
                isGrounded = true;
                //Debug.DrawRay(raycastPosition.position, Vector2.down * raycast.distance, Color.red);
            }
            else
            {
                isGrounded = false;
            }
        }
    }

    void Jump()
    {

        if (!grapple.StartedGrapple)
        {
            StopCoroutine("StopTrail");
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(jumpForce, ForceMode2D.Impulse);
            jumpPS.Play();
            jumpTrail.emitting = true;
            StartCoroutine("StopTrail");

            animator.Play(JUMP_ANIMATION);

            //  Particles
            if (isGrounded)
            {
                isGrounded = false;
            }
        }
    }

    IEnumerator StopTrail()
    {
        yield return new WaitForSeconds(trailDuration);
        jumpTrail.emitting = false;
    }
}
