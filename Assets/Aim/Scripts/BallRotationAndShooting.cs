using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRotationAndShooting : MonoBehaviour {
    
    public GameObject arrow;
    public Rigidbody2D rb;
    private bool invertControls;

    void Start() {
        if(PlayerPrefs.GetInt("InvertControls") == 0) {
            invertControls = false;
        }else {
            invertControls = true;
        }
    }

    void Update() {
       if (Input.GetMouseButton (0)) {
            Vars.isBallActive = false;
            GetComponent<CircleCollider2D> ().radius = 0.22f;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			float px = transform.position.x - mousePosition.x;
			float py = transform.position.y - mousePosition.y;
            if(invertControls) {
                transform.localRotation = Quaternion.Euler (0, 0, Mathf.Rad2Deg * Mathf.Atan2 (py, px));
            }else {
                transform.localRotation = Quaternion.Euler (0, 0, Mathf.Rad2Deg * Mathf.Atan2 (-py, -px));
            }
		}

        if (Input.GetMouseButtonUp(0)) {
            if(arrow == null) return;
            if(!arrow.activeSelf) Destroy(this.gameObject);
            Vars.isBallActive = true;
            GetComponent<CircleCollider2D> ().radius = 0.128f;
            rb.AddForce (transform.right * 700);
            Destroy(arrow);
        }

        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if(hit.collider == null) {
            EnableArrowObject();
            return;
        }

        if (hit.collider.name == "Ball") {
            if(arrow == null) return;
            arrow.SetActive (false);
        }else {
            EnableArrowObject();
        }
    }

    private void EnableArrowObject() {
        if(arrow == null) return;
        arrow.SetActive (true);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.name.Contains("Border")) {
            ObstacleHit[] obstacleHit = Object.FindObjectsOfType<ObstacleHit>();
            foreach (ObstacleHit hit in obstacleHit) {
                hit.RestartObstacle();
            }
            Destroy(this.gameObject);
        }
    }
}
