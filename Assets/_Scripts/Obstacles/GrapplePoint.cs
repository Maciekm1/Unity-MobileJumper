using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    public static List<GameObject> GrappleList = new List<GameObject>();

    public float RangeSq { get; set; }

    private void OnEnable()
    {
        RangeSq = 9f;

        float size = Mathf.Sqrt(RangeSq);
        transform.GetChild(0).transform.localScale = new Vector3(size * 2, size * 2, 0);

        GrappleList.Add(gameObject);
    }

    private void OnDisable()
    {
        GrappleList.Remove(gameObject);
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine("Deactivate");
        }
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    void Update()
    {

    }
}
