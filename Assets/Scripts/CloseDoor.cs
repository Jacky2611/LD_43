using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour {
    bool triggered = false;
    public void OnTriggerEnter2D(Collider2D col){
        if (!triggered)
            GameObject.Find("WaveManger").GetComponent<WaveManager>().CloseGate();
    }
}
