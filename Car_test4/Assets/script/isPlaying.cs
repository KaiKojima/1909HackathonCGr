using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isPlaying : MonoBehaviour {

    public static bool isPlay = false;
    static bool start;
    // Start is called before the first frame update
    void Start() {
          isPlay = false;
          start  = false;
    }

    // Update is called once per frame
    void Update() {
        if (!start) isPlay = false;
    }

    public static void setPlay(bool i) {
        isPlay = true;
    }

    public static bool getPlay() {
        return isPlay;
    }

    public static void GameStart() {
        start = true;
    }

}
