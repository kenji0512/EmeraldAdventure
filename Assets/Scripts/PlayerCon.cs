using System;
using System.Collections;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    CharacterController con;
    Animator anim;
    AttackSword _attackSwordanim;

    [SerializeField]
    float _normalSpeed = 3f; // 通常時の移動速度
    [SerializeField]
    float _sprintSpeed = 5f; // ダッシュ時の移動速度
    [SerializeField]
    float _jump = 4f;        // ジャンプ力
    [SerializeField]
    float _gravity = 9.87f;    // 重力の大きさ
    //[SerializeField] string _attackButton = "Fire1";    //攻撃（パンチ）ボタン
    //[SerializeField] float _freezeSecondsOnAttack = 0.3f;   //攻撃時に移動不可にする秒数
    //[SerializeField] Collider _attackTrigger;   //攻撃判定となるトリガー
    //[SerializeField] float _attackPower = 20f;  //攻撃が当たった時に加える力



    Vector3 _moveDirection = Vector3.zero;
    private int _jumpCount = 0;
    /// <summary>接地フラグ</summary>
    bool _isGrounded = false;


    void Start()
    {
        con = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        _attackSwordanim = GetComponentInChildren<AttackSword>();
        // マウスカーソルを非表示にし、位置を固定
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {

        if (_attackSwordanim == null || !_attackSwordanim.IsAttacking())   // Move は指定したベクトルと場合（Attack中）だけ移動させる命令
        {
            Move();
        }

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetBool("Jump", true);
            Vector3 walkSpeed = con.velocity;
            walkSpeed.y = 0;
            anim.SetFloat("Speed", walkSpeed.magnitude);
        }
        //if (Input.GetButtonDown("Fire1")) // Fire1 は通常、マウスの左クリックやコントローラーのボタンに割り当てられています
        //{
        //    _attackSwordanim.Attack();
        //}


    }
    private void LateUpdate()
    {
        // ジャンプアニメーションが終了したらjumpパラメータをfalseにする
        if (Input.GetButtonDown("Jump")) // Jumpアニメーションが再生中でない場合
        {
            anim.SetBool("Jump", false);
        }
    
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            //カウント生成、制限させる
            //boolのGroundに設置し時にカウントを初期化させて
            _jumpCount++;
            _isGrounded = true;
            if (_jumpCount > 0)
            {
                _jumpCount = 0;
            }
        }
    }
    private void Move()
    {
        // 移動速度を取得
        float speed = Input.GetKey(KeyCode.LeftShift) ? _sprintSpeed : _normalSpeed;

        // カメラの向きを基準にした正面方向のベクトル
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 前後左右の入力（WASDキー）から、移動のためのベクトルを計算
        Vector3 moveZ = cameraForward * Input.GetAxis("Vertical") * speed;  //　前後（カメラ基準）　 
        Vector3 moveX = Camera.main.transform.right * Input.GetAxis("Horizontal") * speed; // 左右（カメラ基準）

        // isGrounded は地面にいるかどうかを判定します
        // 地面にいるときはジャンプを可能に
        if (con.isGrounded)
        {
            _moveDirection = moveZ + moveX;
            if (Input.GetButtonDown("Jump"))
            {
                _moveDirection.y = _jump;
            }
        }
        else
        {
            // 重力を効かせる
            _moveDirection = moveZ + moveX + new Vector3(0, _moveDirection.y, 0);
            _moveDirection.y -= _gravity * Time.deltaTime;
        }
        // 移動のアニメーション
        anim.SetFloat("Speed", (moveZ + moveX).magnitude);

        // プレイヤーの向きを入力の向きに変更　
        transform.LookAt(transform.position + moveZ + moveX);


        con.Move(_moveDirection * Time.deltaTime);
    }
}