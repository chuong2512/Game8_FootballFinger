using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHit : MonoBehaviour {
    
    private Vector2 startScale;

    void Start() {
        startScale = transform.localScale; 
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(!Vars.isBallActive) return;

        if(GetComponent<BoxCollider2D> () != null)
            GetComponent<BoxCollider2D> ().enabled = false;

        if(GetComponent<CircleCollider2D> () != null)
            GetComponent<CircleCollider2D> ().enabled = false;

        if(GetComponent<PolygonCollider2D> () != null)
            GetComponent<PolygonCollider2D> ().enabled = false;

        GetComponent<ObstacleDestroy> ().enabled = true;
        GetComponent<ObstacleDestroy> ().startScale = startScale;
        GameObject.Find("HitSound").GetComponent<AudioSource> ().Play();
        Vars.numberOfLevelObjects--;

        if(Vars.numberOfLevelObjects == 0) {
            GameObject.Find("GameManager").GetComponent<InstantiateBall> ().enabled = false;
            GameObject ball = GameObject.Find("Ball");
            Destroy(ball.GetComponent<BallRotationAndShooting> ());
            Destroy(ball, 2f);
            GameObject.Find("GameManager").GetComponent<Menus> ().LevelComplete();
        }

        if(PlayerPrefs.GetInt("Vibration") == 1) {
            Handheld.Vibrate();
        }
    }

    public void RestartObstacle() {
        if(new Vector2(transform.localScale.x, transform.localScale.y) != startScale) Vars.numberOfLevelObjects++;
        transform.localScale = startScale;

        if(GetComponent<BoxCollider2D> () != null)
            GetComponent<BoxCollider2D> ().enabled = true;
            
        if(GetComponent<CircleCollider2D> () != null)
            GetComponent<CircleCollider2D> ().enabled = true;

        if(GetComponent<PolygonCollider2D> () != null)
            GetComponent<PolygonCollider2D> ().enabled = true;

        GetComponent<ObstacleDestroy> ().enabled = false;
        GetComponent<ObstacleAlphaReset> ().enabled = true;
    }
}
