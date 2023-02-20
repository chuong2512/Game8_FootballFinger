using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberOfDestructibleLevelObjects : MonoBehaviour {
    
    void Start() {
        Vars.numberOfLevelObjects = transform.childCount;
    }

}
