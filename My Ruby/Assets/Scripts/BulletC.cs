using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制子弹的移动、碰撞
/// </summary>
public class BulletC : MonoBehaviour
{
    Rigidbody2D rbody;

    public AudioClip hitClip;
    // Start is called before the first frame update
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 子弹的移动
    /// </summary>
    public void Move(Vector2 moveDirection,float moveForce)
    {
        rbody.AddForce(moveDirection*moveForce);
    }
    ///碰撞检测
    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyC ec= other.gameObject.GetComponent<EnemyC>();
        if (ec != null)
        {
            ec.Fixed();//修复敌人
        }
        AudioManager.instance.AudioPlay(hitClip);//播放命中音效


        Destroy(this.gameObject);
    }
}
