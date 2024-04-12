using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// NPC交互相关
/// </summary>
public class NPCManager : MonoBehaviour
{
    public GameObject TipImage;//提升按键对话框

    public GameObject dialogImage;//对话

    public float showTime = 4;//对话框显示时间4秒

    private float showtimer;//对话框显示计算器

    // Start is called before the first frame update
    void Start()
    {
        TipImage.SetActive(true);//初始显示提示
        dialogImage.SetActive(false);//初始隐藏对话框
        showtimer = -1;
    }

    // Update is called once per frame
    void Update()
    {
        showtimer-=Time.deltaTime;

        if(showtimer < 0)
        {
            TipImage.SetActive(true );
            dialogImage.SetActive(false );
        }
    }
    /// <summary>
    /// 显示对话框
    /// </summary>
    public void ShowDialog()
    {
        showtimer = showTime;
        TipImage.SetActive(false);
        dialogImage.SetActive(true);
    }
}
