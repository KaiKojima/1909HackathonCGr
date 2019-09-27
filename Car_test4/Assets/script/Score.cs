using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    int score;
    public Text textScore;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (score < 0) score = 0;
        textScore.text = "POINT:" + score.ToString();
     
    }

    public void ScoreChange(int score) {
        this.score += score;
    }

    public int getScore() {
        return score;
    }


}
