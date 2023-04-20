using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    //  Settings
    private float timer;

    float PlayerToGrappleMagSqr;
    Vector2 newPosition;
    //  References
    private Rigidbody2D rb;
    public bool StartedGrapple { get; set; }
    GameObject closestGrapple;

    void Start()
    {
        StartedGrapple = false;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        InputManager.Instance.OnScreenHold += StartGrapple;
        InputManager.Instance.OnScreenRelease += StopGrapple;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnScreenHold -= StartGrapple;
        InputManager.Instance.OnScreenRelease -= StopGrapple;
    }

    void Update()
    {
        if (StartedGrapple)
        {
            GameObject closestGrapple = FindClosestGrapple();

            if (closestGrapple != null)
            {
                transform.RotateAround(closestGrapple.transform.position, new Vector3(0, 0, 1), 360 * Time.deltaTime);
            }
        }

    }

    void StartGrapple()
    {
        closestGrapple = FindClosestGrapple();
        if (closestGrapple != null)
        {
            PlayerToGrappleMagSqr = (-transform.position + closestGrapple.transform.position).sqrMagnitude;
            if (PlayerToGrappleMagSqr < closestGrapple.GetComponent<GrapplePoint>().RangeSq + 2f)
            {
                rb.velocity = Vector2.zero;
                StartedGrapple = true;
                rb.gravityScale = 0f;
            }
        }

    }

    GameObject FindClosestGrapple()
    {
        GameObject nothing = null;
        if (GrapplePoint.GrappleList.Count == 0)
        {
            return nothing;
        }
        GameObject closest = GrapplePoint.GrappleList[0];
        for (int i = 0; i < GrapplePoint.GrappleList.Count; i++)
        {
            if ((-transform.position + GrapplePoint.GrappleList[i].transform.position).sqrMagnitude < (-transform.position + closest.transform.position).sqrMagnitude)
            {
                closest = GrapplePoint.GrappleList[i];
            }
        }

        return closest;
    }

    void StopGrapple()
    {
        PlayerToGrappleMagSqr = (-transform.position + closestGrapple.transform.position).sqrMagnitude;
        closestGrapple = null;

        Camera.main.GetComponent<CameraMove>().StartMoving();
        rb.velocity = Vector2.zero;
        StartedGrapple = false;

        //  CHANGE THIS
        rb.gravityScale = 2.5f;

        // Reset Z rotation

        if (transform.rotation.z != 0)
        {
            transform.rotation = Quaternion.identity;
        }
    }

}
