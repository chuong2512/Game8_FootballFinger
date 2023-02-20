using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCurrentLevelNumber : MonoBehaviour {

    public TextMesh levelNumber;
    private bool up = true;
    private bool down = false;
    private float scale = 0;

    void Start() {
        levelNumber.text = "Level " + Vars.currentLevel;
    }

    void Update() {
        if(up) {
            scale += Time.deltaTime * 3;
            if(scale >= 1f) {
                up = false;
                scale = 1;
                Invoke("HideLevelNumber", 2f);
            }
        }

        if(down) {
            scale -= Time.deltaTime * 3;
            if(scale <= 0) {
                down = false;
                scale = 0;
                Destroy(this.gameObject);
            }
        }

        transform.localScale = new Vector2(scale, scale);
    }

    private void HideLevelNumber() {
        down = true;
    }
}
