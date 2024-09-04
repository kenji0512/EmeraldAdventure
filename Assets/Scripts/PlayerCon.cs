using System;
using System.Collections;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    /// <summary>
    /// https://qiita.com/mkgask/items/e660fc802b2ec994fb5f
    /// </summary>
    /// https://aluminum-pepe.hatenablog.com/entry/2019/01/07/064816
    /// </summary>
    CharacterController con;
    Animator anim;
    AttackSword _attackSword;

    [SerializeField]
    float _normalSpeed = 3f; // �ʏ펞�̈ړ����x
    [SerializeField]
    float _sprintSpeed = 5f; // �_�b�V�����̈ړ����x
    [SerializeField]
    float _jump = 4f;        // �W�����v��
    [SerializeField]
    float _gravity = 9.87f;    // �d�͂̑傫��
    [SerializeField] string _attackButton = "Fire1";    //�U���i�p���`�j�{�^��
    [SerializeField] float _freezeSecondsOnAttack = 0.3f;   //�U�����Ɉړ��s�ɂ���b��
    [SerializeField] Collider _attackTrigger;   //�U������ƂȂ�g���K�[
    [SerializeField] float _attackPower = 20f;  //�U���������������ɉ������



    Vector3 _moveDirection = Vector3.zero;
    private int _jumpCount = 0;
    /// <summary>�ڒn�t���O</summary>
    bool _isGrounded = false;
    /// <summary>�t���[�Y�t���O. ���ꂪ true �̎��͓������~�܂�</summary>
    bool _freeze = false;


    void Start()
    {
        con = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        // �}�E�X�J�[�\�����\���ɂ��A�ʒu���Œ�
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
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

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetBool("Jump", true);
            Vector3 walkSpeed = con.velocity;
            walkSpeed.y = 0;
            anim.SetFloat("Speed", walkSpeed.magnitude);
        }
        if (Input.GetButtonDown("Fire1")) // Fire1 �͒ʏ�A�}�E�X�̍��N���b�N��R���g���[���[�̃{�^���Ɋ��蓖�Ă��Ă��܂�
        {
            _attackSword.Attack();
        }
        if (Input.GetButtonDown(_attackButton))
        {
            //Freeze(_freezeSecondsOnAttack, () => anim.SetBool("AttackTrigger", false));
            anim?.SetBool("AttackTrigger", true);
            anim?.SetBool("AttackTrigger", false);
            //�U���A�j���[�V�����������̎d�����l����
        }
        // �ړ��̃A�j���[�V����
        anim.SetFloat("Speed", (moveZ + moveX).magnitude);

        // �v���C���[�̌�������͂̌����ɕύX�@
        transform.LookAt(transform.position + moveZ + moveX);

        //con.Move(_moveDirection * Time.deltaTime);
        if (!_freeze)   // Move �͎w�肵���x�N�g���Əꍇ�iAttack���j�����ړ������閽��

        {
            con.Move(_moveDirection * Time.deltaTime);
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
    public void Freeze(float duration)
    {
        StartCoroutine(FreezeRoutine(duration, null));
    }
    public void Freeze(float duration, Action callback)
    {
        StartCoroutine(FreezeRoutine(duration, callback));
    }
    IEnumerator FreezeRoutine(float duration, Action callback)
    {
        // �t���[�Y�t���O�𗧂ĂāA�������~�߂�
        _freeze = true;
        yield return new WaitForSeconds(duration);  // �҂�
        // �t���[�Y����������
        _freeze = false;
        callback();
    }
}