using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人控制相关
/// </summary>
public class EnemyC : MonoBehaviour
{
    public float speed = 3;//移动速度

    public float changeDirectionTime = 2f;//改变方向的时间

    public ParticleSystem fixEffect;//修复特效

    public bool isVeryical;//判断是否垂直方向移动

    private float changeTimer;//改变方向计时器

    private Vector2 moveDIrection;//移动方向

    public ParticleSystem brokenEffect;//损坏特效

    public AudioClip fixedClip;//被修复的音效

    private bool isFixed;//是否被修复了
    
    private Rigidbody2D rbody;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        moveDIrection = isVeryical ? Vector2.up : Vector2.right;//如果是垂直移动，方向就朝上，否则方向朝右

        changeTimer = changeDirectionTime;

        isFixed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFixed)return;//被修复不执行以下代码

        changeTimer -= Time.deltaTime;
        if (changeTimer < 0)
        {
            moveDIrection *= -1;
            changeTimer = changeDirectionTime;
        }
        Vector2 position = rbody.position;
        position.x += moveDIrection.x * speed * Time.deltaTime;
        position.y += moveDIrection.y * speed * Time.deltaTime;
        rbody.MovePosition(position);

        anim.SetFloat("MoveX",moveDIrection.x);
        anim.SetFloat("MoveY", moveDIrection.y);//移动动画控制
    }
    /// <summary>
    /// 与玩家的碰撞检测
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerC pc = other.gameObject.GetComponent<PlayerC>();
        if(pc != null)
        {
            pc.ChangeHealth(-1);
        }
    }

    ///敌人被修复
    public void Fixed()
    {
        isFixed = true;
        if(brokenEffect.isPlaying == true)
        {
            Instantiate(fixEffect, transform.position, Quaternion.identity);//生成修复特效
            brokenEffect.Stop();
        }
        AudioManager.instance.AudioPlay(fixedClip);//播放被修复的音效

        rbody.simulated = false;//禁用物理
        anim.SetTrigger("fix");//播放被修复的动画
    }
}
