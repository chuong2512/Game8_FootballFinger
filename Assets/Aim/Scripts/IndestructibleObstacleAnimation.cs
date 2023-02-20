using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndestructibleObstacleAnimation : MonoBehaviour {
    
    private SpriteRenderer sp;
    private float alpha = 1;
    private bool up = false;

    void Start() {
        sp = GetComponent<SpriteRenderer> ();
    }

    void Update() {
        if(up) {
            alpha += Time.deltaTime * 5;
            if(alpha >= 1) {
                up = false;
                alpha = 1;
                sp.color = new Color(sp.color.r, sp.color.b, sp.color.g, alpha);
                this.enabled = false;
            }
        }else {
            alpha -= Time.deltaTime * 5;
            if(alpha <= 0.8f) up = true;
        }
        sp.color = new Color(sp.color.r, sp.color.b, sp.color.g, alpha);
    }
}
