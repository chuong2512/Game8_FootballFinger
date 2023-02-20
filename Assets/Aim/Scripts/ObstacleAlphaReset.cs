using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAlphaReset : MonoBehaviour {
    
    private SpriteRenderer sp;
    private float alpha;
    
    void OnEnable() {
        sp = GetComponent<SpriteRenderer> ();
        if(sp.color.a >= 1) this.enabled = false;
        alpha = 0;
    }

    void Update() {
        alpha += Time.deltaTime * 5;
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, alpha);
        if(alpha >= 1) {
            this.enabled = false;
        }
    }
}
