using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// UI管理相关
/// </summary>
public class UImanager : MonoBehaviour
{
    
    /// <summary>
    /// 单例模式
    /// </summary>
    public static UImanager instance { get; private set; }
    void Awake()
        {
        instance = this;
        }

    public Image healthbar;//角色的血条

    public Text bulletC;//子弹数量

    /// <summary>
    /// 更新血条
    /// </summary>
    /// <param name="curAmount"></param>
    /// <param name="maxAmount"></param>
    public void UpdateHealthBar(int curAmount, int maxAmount)
    {
        healthbar.fillAmount = (float)curAmount / (float)maxAmount;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 更新子弹数量文本显示
    /// </summary>
    /// <param name="curAmount"></param>
    /// <param name="maxAmount"></param>
    public void UpdateBulletCount(int curAmount, int maxAmount)
    {
        bulletC.text = curAmount.ToString()+" / "+maxAmount.ToString();
    }
}
