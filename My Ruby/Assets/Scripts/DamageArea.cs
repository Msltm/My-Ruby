using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// 伤害陷阱相关
/// </summary>
public class DamageArea : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerC pc = other.GetComponent<PlayerC>();
        if(pc != null)
        {
            pc.ChangeHealth(-1);
        }
    }
}
