using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> backgrounds = new List<GameObject>();
    private List<GameObject> bgReferences = new List<GameObject>();

    //  Settings
    [SerializeField] private int bgWidth;
    public float scrollSpeed { get; set; }

    void Start()
    {
        for (int i = 0; i < backgrounds.Count; i++)
        {
            // Sets starting position of backgrounds, uses camera.aspect to find x size and align left position of bg to left position of camera
            GameObject newBG = Instantiate(backgrounds[i], new Vector3(((i * bgWidth) + (bgWidth / 2)) - (Camera.main.orthographicSize * Camera.main.aspect), 0, 0), Quaternion.identity);
            newBG.transform.parent = Camera.main.transform;
            bgReferences.Add(newBG);
        }
    }


    void Update()
    {
        ScrollBackgrounds();
    }

    private void ScrollBackgrounds()
    {
        for (int i = 0; i < bgReferences.Count; i++)
        {
            // Resets position back to front
            if ((bgReferences[i].transform.position.x + (Camera.main.orthographicSize * Camera.main.aspect) + bgWidth / 2) - Camera.main.transform.position.x <= 0)
            {
                bgReferences[i].transform.position += new Vector3((bgWidth * 2), 0, 0);
            }

            // Scrolls bg back by scroll speed
            bgReferences[i].transform.position += new Vector3(-scrollSpeed * Time.deltaTime, 0, 0);
        }
    }
}
