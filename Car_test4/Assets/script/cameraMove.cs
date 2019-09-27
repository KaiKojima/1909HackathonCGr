using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cameraMove : MonoBehaviour
{

    int count = 0;

// Start is called before the first frame update
void Start()
    {
       
    }
    // Update is called once per frame
    void FixedUpdate() {
        


        if (isPlaying.getPlay() == true) {
            count++;
          //  if (count > 60) iTween.MoveUpdate(this.gameObject, iTween.Hash("x",-105,"y",191,"z", -836f,"time" , 60f));
        }
    }
}
