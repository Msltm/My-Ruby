﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public ParticleSystem collectEffect;//拾取特效

    public AudioClip collectClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    ///碰撞检测
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerC pc = other.GetComponent<PlayerC>();

        if (pc != null)
        {
            if(pc.MyCurrentHealth < pc.MyMaxHealth)
            {

                pc.ChangeHealth(1);
                Instantiate(collectEffect,transform.position,Quaternion.identity);//生成特效
                AudioManager.instance.AudioPlay(collectClip);
                Destroy(this.gameObject);
            }
            
        }
    }
}
