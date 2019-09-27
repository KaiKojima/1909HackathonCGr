using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDown : MonoBehaviour
{

    public Sprite[] num = new Sprite[4];
    Image img;

    // Start is called before the first frame update
    void Start()
    {
        isPlaying.setPlay(false);
        this.GetComponent<RectTransform>();
            img = this.GetComponent<Image>();
            img.sprite = num[3];
        transform.position = new Vector3(-30,150,0);
        StartCoroutine("start");


        isPlaying.GameStart();
        isPlaying.setPlay(true);

    }

    // Update is called once per frame
    void Update()
    {
    
    }

    IEnumerator start() {

        yield return new WaitForSeconds(1f);

        for (int i = 3; i > -1; i --) {

            img.sprite = num[i];

            iTween.MoveTo(this.gameObject, iTween.Hash("x", 300f, "time", 0.5f));
            yield return new WaitForSeconds(0.4f);

            iTween.PunchRotation(gameObject, iTween.Hash("z", 360f,"time",0.5));
//            iTween.ScaleTo(gameObject, iTween.Hash("x", 3f, "y", 3f, "time", 0.4f));
            yield return new WaitForSeconds(0.6f);

            transform.position = new Vector3(-30, 150, 0);
            if (i == 0) {
                isPlaying.GameStart();
                isPlaying.setPlay(true);
                this.gameObject.SetActive(false);
            }
        }

        
    }


}
