using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class result : MonoBehaviour
{
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "結果：" +Goal.getFinalScore().ToString() + "点";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
