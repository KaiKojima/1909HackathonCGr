using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

    public GameObject player;

    //スコア引き継ぎように取得
    GameObject score;
    Score sc;

    //フェードアウト用の画像取得
    GameObject fead;
    Image feadImage;

    //フェードアウト用のトリガー
    bool isGoal;
    float alpha = 0;

    public static int finalScore;

    // Start is called before the first frame update
    void Start() {
        score = GameObject.FindGameObjectWithTag("ScoreText");
        sc = score.GetComponent<Score>();


        fead = GameObject.FindGameObjectWithTag("Fead");
        feadImage = fead.GetComponent<Image>();
        feadImage.color = new Color(255,255,255,0);
        fead.SetActive(false);
        isGoal = false;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (isGoal) {
            alpha = alpha + 0.01f;
            feadImage.color = new Color(1,1,1,alpha);

            if(alpha > 1)SceneManager.LoadScene("result");

        }
    }

    void OnTriggerEnter(Collider goal) {
        if (goal.gameObject.tag == "Player") {
            finalScore = sc.getScore();
            fead.SetActive(true);
            feadImage.enabled = true;
            isGoal = true;
        }
    }

    public static int getFinalScore() {
        return finalScore;
    }

}
