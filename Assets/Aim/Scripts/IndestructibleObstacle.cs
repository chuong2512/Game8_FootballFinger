using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndestructibleObstacle : MonoBehaviour {
    
    void OnCollisionEnter2D(Collision2D col) {
        if(!Vars.isBallActive) return;
        GameObject.Find("IndestructibleObstacleHit").GetComponent<AudioSource> ().Play();
        GetComponent<IndestructibleObstacleAnimation> ().enabled = true;
    }
}
