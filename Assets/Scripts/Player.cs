using UnityEngine;

/// <summary>
/// Rigidbody ���g���ăv���C���[�𓮂����R���|�[�l���g
/// ���͂��󂯎��A����ɏ]���ăI�u�W�F�N�g�𓮂����B
/// �J�����̌����ɉ����đ��ΓI�Ɉړ�����B
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    /// <summary>��������</summary>
    [SerializeField] float _movingSpeed = 5f;
    /// <summary>�W�����v��</summary>
    [SerializeField] float _jumpSpeed = 5f;
    /// <summary>�ڒn����̍ہA�R���C�_�[�̒��S (Center) ����ǂꂭ�炢�̋������u�ڒn���Ă���v�Ɣ��肷�邩�̒���</summary>
    [SerializeField] float _isGroundedLength = 1.1f;
    /// <summary>�n�ʂ�\�� Layer</summary>
    [SerializeField] LayerMask _groundLayer = ~0;
    /// <summary>����܂Őڒn�����ɃW�����v�ł��邩�B�Q�i�W�����v���鎞�� 2 �ɐݒ肷��</summary>
    [SerializeField] int _maxJumpCount = 1;
    /// <summary>�W�����v�������ɖ炷���ʉ�</summary>
    [SerializeField] AudioClip _jumpSfx = null;

    /// <summary>�W�����v�����񐔁B�ڒn��Ԃ���W�����v�������� 1 �ɂȂ�B</summary>
    public int _jumpCount = 0;

    /// <summary>�ڒn�t���O</summary>
    bool _isGrounded = false;
    Rigidbody _rb = null;
    CapsuleCollider _collider = null;
    Animator _anim = null;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        // �����̓��͂��擾���A���������߂�
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // ���͕����̃x�N�g����g�ݗ��Ă�
        Vector3 dir = Vector3.forward * v + Vector3.right * h;

        if (dir == Vector3.zero)
        {
            // �����̓��͂��j���[�g�����̎��́Ay �������̑��x��ێ����邾��
            _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);
        }
        else
        {
            // �J��������ɓ��͂��㉺=��/��O, ���E=���E�ɃL�����N�^�[��������
            dir = Camera.main.transform.TransformDirection(dir);    // ���C���J��������ɓ��͕����̃x�N�g����ϊ�����
            dir.y = 0;  // y �������̓[���ɂ��Đ��������̃x�N�g���ɂ���

            // ���͕����Ɋ��炩�ɉ�]������
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime);  // Slerp ���g���̂��|�C���g

            Vector3 velo = dir.normalized * _movingSpeed; // ���͂��������Ɉړ�����
            velo.y = _rb.velocity.y;   // �W�����v�������� y �������̑��x��ێ�����
            _rb.velocity = velo;   // �v�Z�������x�x�N�g�����Z�b�g����
        }

        // �W�����v�̓��͂��擾���A�ڒn���Ă���ꍇ�̓W�����v����
        if (Input.GetButtonDown("Jump"))
        {
            if (_jumpCount < _maxJumpCount)
            {
                Jump();
                if (!_isGrounded)
                {
                    _jumpCount++;
                    _anim.SetTrigger("Jump");
                }

            }
        }
    }

    void LateUpdate()
    {
        // �A�j���[�V�����𑀍삷��
        Vector3 velocity = _rb.velocity;
        velocity.y = 0; // �㉺�����̑��x�͖�������
        _anim.SetFloat("Speed", velocity.magnitude);
        _anim.SetBool("Jump", _isGrounded);
    }

    /// <summary>
    /// �W�����v���鎞�ɌĂяo��
    /// </summary>
    void Jump()
    {
        AudioSource.PlayClipAtPoint(_jumpSfx, this.transform.position);
        // AddForce �łȂ��X�s�[�h��ς���iAddForce ���ƁA�~�����ɃW�����v�������ɃW�����v���キ�Ȃ�j
        Vector3 velocity = _rb.velocity;
        velocity.y = _jumpSpeed;
        _rb.velocity = velocity;
        _jumpCount++;
    }

    void OnTriggerEnter(Collider other)
    {
        _isGrounded = true;
        _jumpCount = 0; // ���n�������ɃJ�E���^�����Z�b�g����
    }

    void OnTriggerExit(Collider other)
    {
        _isGrounded = false;
        _anim.SetTrigger("Jump");    // �W�����v�������⑫�ꂩ��~�肽���Ƀ��[�V�������Đ�����
    }
}