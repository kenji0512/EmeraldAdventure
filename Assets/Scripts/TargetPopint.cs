using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint: MonoBehaviour
{

    private float RotateSpeed = 100f;
    private float Radian = 0f;
    private float HeadPoint = 1.2f;

    private SpriteRenderer AttentionSprite;
    private Enemy parentEnemy;

    public Sprite Attention;
    public Sprite UnAttention;

    // Use this for initialization
    void Start()
    {
        AttentionSprite = GetComponent<SpriteRenderer>();
        AttentionSprite.sprite = UnAttention;

        parentEnemy = transform.parent.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        Radian += Time.deltaTime;
        transform.Rotate(Vector3.up * Time.deltaTime * RotateSpeed);
        transform.localPosition = Vector3.up * HeadPoint + Vector3.up * (Mathf.Sin(Radian) / 10f);
        print(Mathf.Sin(Radian));

        if (parentEnemy.isDrawingAttention)
        {
            AttentionSprite.sprite = Attention;
        }
        else
        {
            AttentionSprite.sprite = UnAttention;
        }
    }
}