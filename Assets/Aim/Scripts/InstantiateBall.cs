using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBall : MonoBehaviour{
    void Update() {
        if (Input.GetMouseButtonDown (0)) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            if(GameObject.Find("Ball") != null) {
                Destroy(GameObject.Find("Ball"));
                ObstacleHit[] obstacleHit = Object.FindObjectsOfType<ObstacleHit>();
                foreach (ObstacleHit hit in obstacleHit) {
                    hit.RestartObstacle();
                }
            }
            Vars.isBallActive = false;
            GameObject ball = Instantiate(Resources.Load("Ball"), new Vector2(mousePosition.x, mousePosition.y), Quaternion.identity) as GameObject;
            ball.name = "Ball";
        }
    }
}
