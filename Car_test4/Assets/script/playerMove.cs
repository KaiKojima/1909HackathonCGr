using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class playerMove : MonoBehaviour {
    //自身のRigidbody
    Rigidbody rb;

    //タッチUIのイメージ画像
    public GameObject start;
    public GameObject now;
    public Image st;
    public Image no;

    //プレイヤーが前を向く角度
    float vec;

    //タッチされてるかタッチされてないかの判定用
    bool isTouch;

    //地面の着地判定用
    bool isGround;

    //カメラ
    public GameObject came;

    // カメラ外に出た際の判定用
    MeshRenderer mesh;

    //スコア変更用オブジェクト
    GameObject ScoreText;
    Score sc;

    //  アニメーションするための変数
    private Animator animCon;
    string animState;


    // Start is called before the first frame update
    void Start() {

        //自身にRigidbodyを入れ使用可能に
        rb = GetComponent<Rigidbody>();

        //タッチした際の画像の座標を変更出来るように
        st.GetComponent<RectTransform>();
        no.GetComponent<RectTransform>();

        //MeshRendererの取得
        mesh = GetComponent<MeshRenderer>();

        //スコアのスクリプト取得
        ScoreText = GameObject.FindGameObjectWithTag("ScoreText");
        sc = ScoreText.GetComponent<Score>();

        animCon = GetComponent<Animator>();


        FlickStop();
    }


    // Update is called once per frame
    void FixedUpdate() {
        if (isPlaying.getPlay() == true) {
                if (!isGround) rb.AddForce(Vector3.down * 10, ForceMode.Acceleration);
                fallDown();

            //フリックして移動
            if (!ButtonTouch()) {
                Flick();
            }
                //自身の角度をタッチに沿うように
                transform.rotation = Quaternion.Euler(0, -vec, 0);

                if (Input.GetKeyDown(KeyCode.Space)) Jump();
            
                animChange(animReturn());
            }
        notVisible();
    }

    //タッチする時にボタンに触れてるかを返すメソッド
    List<RaycastResult> result = new List<RaycastResult>();
    PointerEventData pointer = new PointerEventData(EventSystem.current);
    bool isButtonTouch;
    public bool ButtonTouch() {
        //タッチしてる時のUIの名前を取得/////////////////////////////////////
        EventSystem.current.RaycastAll(pointer, result);
        pointer.position = Input.mousePosition;
        foreach (RaycastResult raycastResult in result) {
            if (raycastResult.gameObject.name == "Button") {
                FlickStop();
                isButtonTouch = true;
            } else {
                isButtonTouch = false;
            }
        }
        return isButtonTouch;
    }


    //フリックメソッド
    private Vector3 touchStartPos;
    private Vector3 touchPos;
    private Vector3 touchEndPos;
    void Flick() {

        //タッチの押し始め
        if (Input.GetKeyDown(KeyCode.Mouse0)) {

            isTouch = true;
            touchStartPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            st.transform.position = touchStartPos;

            start.SetActive(true);
            now.SetActive(true);
        }

        //タッチしている間
        if (Input.GetKey(KeyCode.Mouse0)) {

            touchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            no.transform.position = touchPos;

            var locVel = transform.InverseTransformDirection(rb.velocity);//ワールド速度からローカル速度へ変換
            locVel.z = Mathf.Abs(Vector3.Distance(touchStartPos, touchPos)) / 15;//タッチ2点の位置を小さくして前方向に入れる


            if (locVel.z > 10) locVel.z = 10;
            rb.velocity = transform.TransformDirection(locVel);//ローカル速度からワールド速度へ変換
        }

        //離した瞬間
        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            touchEndPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            touchPos = new Vector3(0, 0, 0);
            touchStartPos = new Vector3(0, 0, 0);

            //離した時に力をリセットする
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            start.SetActive(false);
            now.SetActive(false);
            isTouch = false;
        }

        if (isTouch && Vector3.Distance(touchStartPos, touchPos) != 0) {
            float dx = touchStartPos.x - touchPos.x;
            float dy = touchStartPos.y - touchPos.y;
            float rad = Mathf.Atan2(dy, dx);
            vec = rad * Mathf.Rad2Deg;
            if (rad * Mathf.Rad2Deg < 0) {
               vec = 360 + (rad * Mathf.Rad2Deg);
            }

            if (Vector3.Distance(touchStartPos, touchPos) > 100) {

                float c = touchStartPos.x + (100 * Mathf.Cos(rad+3.14f));
                float b = touchStartPos.y + (100 * Mathf.Sin(rad + 3.14f));
                Debug.Log(rad);
                touchPos = new Vector3(c,b, Input.mousePosition.z);
                no.transform.position = touchPos;
            }



        }
    }

    //フリックの強制終了メソッド
    void FlickStop() {
        isTouch = false;
        start.SetActive(false);
        now.SetActive(false);

        touchPos = new Vector3(0,0,0);
        no.transform.position = touchPos;

        touchStartPos = new Vector3(0,0,0);
        st.transform.position = touchStartPos;

        rb.velocity = new Vector3(0,rb.velocity.y,0);

    }

    //ジャンプ処理メソッド
    void Jump() {
        if (isGround) {
            rb.velocity = new Vector3(0, 15, 0);
        }
    }
    //落ちた時の処理メソッド
    void fallDown() {
        if (this.transform.position.y < -20) {
            this.transform.position = new Vector3(0, came.transform.position.y - 35, came.transform.position.z - 10);
            rb.velocity = new Vector3(0,0,0);
            sc.ScoreChange(-100);
        }
    }

    //カメラ外処理メソッド
    float notVisbleCount = 0;
    void notVisible() {
        if (!mesh.isVisible) {
            notVisbleCount = notVisbleCount + Time.deltaTime;
            if (notVisbleCount > 0.5f) {
                this.transform.position = new Vector3(0, came.transform.position.y - 35, came.transform.position.z - 10);
                rb.velocity = new Vector3(0, 0, 0);
                sc.ScoreChange(-100);
            }
        } else {
            notVisbleCount = 0;
        }
    }

    //アニメーションの状態変数の変更
    string animReturn() {
        if (isGround) {
            if (Mathf.Abs(Vector3.Distance(touchStartPos, touchPos)) / 25 == 0) {
                animState = "idle";
            } else if (Mathf.Abs(Vector3.Distance(touchStartPos, touchPos)) / 15 < 5) {
                animState = "walk";
            } else if (Mathf.Abs(Vector3.Distance(touchStartPos, touchPos)) / 15 > 4) {
                animState = "run";
            }
        }
        if (rb.velocity.y > 0 && isGround == false) {
            animState = "up";
        } else if (rb.velocity.y < 0 && isGround == false) {
            animState = "float";
        }
        return animState;
    }

    //アニメーションのモーションを変更
    void animChange(string state) {
        if (state == "idle") animCon.SetInteger("anim" , 0);
        if (state == "walk") animCon.SetInteger("anim", 1);
        if (state == "run") animCon.SetInteger("anim", 2);
        if (state == "up") animCon.SetInteger("anim", 3);
        if (state == "float") animCon.SetInteger("anim", 4);
    }

    //当たり判定
    void OnTriggerStay(Collider colStay) {
        if (colStay.gameObject.tag == "Ground") {
            isGround = true;
        }
    }
    void OnTriggerExit(Collider colExit) {
        if (colExit.gameObject.tag == "Ground") {
            isGround = false;
        }
    }
    void OnTriggerEnter(Collider colEnter) {
        if (colEnter.gameObject.tag == "Ground") {
            animCon.SetTrigger("Down");
        }
    }

}