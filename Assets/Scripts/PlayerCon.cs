using System;
using System.Collections;
using UnityEngine;

public class PlayerCon : CurrentHP
{
    CharacterController con;
    Animator anim;
    AttackSword _attackSwordanim;

    [SerializeField]
    protected float _normalSpeed = 3f; // �ʏ펞�̈ړ����x
    [SerializeField]
    protected float _sprintSpeed = 5f; // �_�b�V�����̈ړ����x
    [SerializeField]
    protected float _jump = 4f;        // �W�����v��
    [SerializeField]
    protected float _gravity = 9.87f;    // �d�͂̑傫��



    Vector3 _moveDirection = Vector3.zero;
    private int _jumpCount = 0;
    /// <summary>�ڒn�t���O</summary>
    protected bool _isGrounded = false;


    void Start()
    {
        con = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        _attackSwordanim = GetComponentInChildren<AttackSword>();
        // �}�E�X�J�[�\�����\���ɂ��A�ʒu���Œ�
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {

        if (_attackSwordanim == null || !_attackSwordanim.IsAttacking())   // Move �͎w�肵���x�N�g���Əꍇ�iAttack���j�����ړ������閽��
        {
            Move();
        }
        //else
        //{
        //    con.Move(_moveDirection * 0);
        //}Bool��Animation�𔻒肳���邱�Ƃ�����Ή���

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetBool("Jump", true);
            Vector3 walkSpeed = con.velocity;
            walkSpeed.y = 0;
            anim.SetFloat("Speed", walkSpeed.magnitude);
        }
        if (_currentHP <= 0)
        {
            return;
        }

    }
    private void LateUpdate()
    {
        // �W�����v�A�j���[�V�������I��������jump�p�����[�^��false�ɂ���
        if (Input.GetButtonDown("Jump")) // Jump�A�j���[�V�������Đ����łȂ��ꍇ
        {
            anim.SetBool("Jump", false);
        }
    
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            //�J�E���g�����A����������
            //bool��Ground�ɐݒu�����ɃJ�E���g��������������
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
        // �ړ����x���擾
        float speed = Input.GetKey(KeyCode.LeftShift) ? _sprintSpeed : _normalSpeed;

        // �J�����̌�������ɂ������ʕ����̃x�N�g��
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �O�㍶�E�̓��́iWASD�L�[�j����A�ړ��̂��߂̃x�N�g�����v�Z
        Vector3 moveZ = cameraForward * Input.GetAxis("Vertical") * speed;  //�@�O��i�J������j�@ 
        Vector3 moveX = Camera.main.transform.right * Input.GetAxis("Horizontal") * speed; // ���E�i�J������j

        // isGrounded �͒n�ʂɂ��邩�ǂ����𔻒肵�܂�
        // �n�ʂɂ���Ƃ��̓W�����v���\��
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
            // �d�͂���������
            _moveDirection = moveZ + moveX + new Vector3(0, _moveDirection.y, 0);
            _moveDirection.y -= _gravity * Time.deltaTime;
        }
        // �ړ��̃A�j���[�V����
        anim.SetFloat("Speed", (moveZ + moveX).magnitude);

        // �v���C���[�̌�������͂̌����ɕύX�@
        transform.LookAt(transform.position + moveZ + moveX);


        con.Move(_moveDirection * Time.deltaTime);
    }
}