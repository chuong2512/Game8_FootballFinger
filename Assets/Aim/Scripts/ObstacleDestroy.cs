using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestroy : MonoBehaviour {
    
    public Vector2 startScale;
    private float alpha = 1;
    private SpriteRenderer sp;

    void Start() {
        sp = GetComponent<SpriteRenderer> ();
    }

    void Update() {
        transform.localScale = new Vector2(transform.localScale.x + Time.deltaTime / 5, transform.localScale.y + Time.deltaTime / 5);
        alpha -= Time.deltaTime * 5;
        sp.color =  new Color(sp.color.r, sp.color.g, sp.color.b, alpha);
        if(alpha <= 0) {
            alpha = 1;
            this.enabled = false;
        }
    }
}
