using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBack : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;

    //  Particles
    [SerializeField] private ParticleSystem blast;

    private void OnEnable()
    {
        GameManager.Instance.OnHealthLoss += Deactivate;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameEnd += Destroy;
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.OnHealthLoss -= Deactivate;
        GameManager.Instance.OnGameEnd -= Destroy;
    }

    void Update()
    {
        if (GameManager.Instance.GamePlaying)
        {
            transform.position += new Vector3(-scrollSpeed * Time.deltaTime, 0, 0);
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void PlayAnimation()
    {
        if (blast != null) { blast.Play(); }
        //LeanTween.moveLocalX(gameObject, Camera.main.transform.position.x - (Camera.main.orthographicSize * Camera.main.aspect) - gameObject.transform.localScale.y, 1f).setEaseOutExpo().setDelay(0.1f);
    }
}
