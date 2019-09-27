using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    GameObject ScoreText;
    Score sc;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        ScoreText = GameObject.FindGameObjectWithTag("ScoreText");
        sc = ScoreText.GetComponent<Score>();
        score = point();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay(Collision col) {
        if (col.gameObject.tag == "Player") {
            sc.ScoreChange(score);
            Destroy(gameObject);
        }
    }

    int point() {
        int returnScore = 10;
        if (this.gameObject.name == "Item_1") returnScore = 20;
        if (this.gameObject.name == "Item_2") returnScore = 30;
        if (this.gameObject.name == "Item_3") returnScore = 40;
        if (this.gameObject.name == "Item_4") returnScore = -50;

        return returnScore;
    }

}
