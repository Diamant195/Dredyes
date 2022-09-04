using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    private Camera cam;
    private float targetZoom;
    private float zoomFactor = 30f;
    [SerializeField] private float zoomLerpSpeed = 10;
    GameObject player;
    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, transform.position.z);
        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= scrollData * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, 8f, 50f);
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            if (cam.orthographicSize < 25) targetZoom = 50;
            else targetZoom = 8;
        }
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);        
    }
}
