using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public int zoomLevel = 4;

    Camera camera;
    GameObject player;
    Resolution res;


    // Use this for initialization
    void Start () {
        camera = gameObject.GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        res = Screen.currentResolution;
        UpdateCameraScale();

    }
	
	// Update is called once per frame
	void LateUpdate () {

        camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, camera.transform.position.z);
	}

    private void OnGUI()
    {
        if (res.width != Screen.currentResolution.width)
        {
            res = Screen.currentResolution;
            UpdateCameraScale();
        }
    }

    void UpdateCameraScale()
    {

        camera.orthographicSize= ((res.height/zoomLevel) / 32 / 2);


    }
}
