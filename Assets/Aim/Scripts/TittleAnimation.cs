using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TittleAnimation : MonoBehaviour {

	float scale = 1;
	bool up = true;

	void Update () {
		if(up) {
			scale += Time.deltaTime / 20;
			if(scale >= 1.05f){
				up = false;
			}
		}else {
			scale -= Time.deltaTime / 20;
			if(scale <= 1f) {
				up = true;
			}
		}			
		GetComponent<RectTransform>().localScale = new Vector2(scale, scale);
	}
}
