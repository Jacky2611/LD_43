using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

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
        if(res.width != Screen.currentResolution.width)
        {
            res = Screen.currentResolution;
            UpdateCameraScale();
        }
        camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, camera.transform.position.z);
	}

    void UpdateCameraScale()
    {
        camera.orthographicSize= ((res.width/6) / 32 / 2);
    }
}
