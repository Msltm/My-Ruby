using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 子弹补给包
/// </summary>
public class BulletBag : MonoBehaviour
{
    public int bulletCount = 10;//包里含有的子弹数量10

    public ParticleSystem collectEffect;//拾取特效

    public AudioClip collectClip;//声音

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerC pc = other.GetComponent<PlayerC>();
        if (pc != null) {
            if (pc.CurrentBulletCount < pc.MyBulletCount)
            {
                pc.ChangeBulletCount(bulletCount);//增加玩家子弹数量
                Instantiate(collectEffect,transform.position, Quaternion.identity);//添加拾取特效
                
                AudioManager.instance.AudioPlay(collectClip);//拾取声音
                Destroy(this.gameObject);
                
            }
        }
    }
        
}
