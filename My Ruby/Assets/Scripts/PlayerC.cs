using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制角色移动、生命、动画等
/// </summary>
public class PlayerC : MonoBehaviour
{
    public float speed = 10f;//移动速度


    private int maxHealth = 5;//最大生命值

    private int currentHealth;//当前生命值

    public int MyMaxHealth { get { return maxHealth; } }

    public int MyCurrentHealth { get { return currentHealth; } }

    private float invincibleTime = 2f;//无敌时间2秒

    private float invincibleTimer;//无敌计时器

    private bool isInvincible;//是否处于无敌状态

    public GameObject bulletPrefab;//子弹

    //=========玩家音效=========

    public AudioClip hitClip;//受伤音效

    public AudioClip launchClip;//发射齿轮音效



    //===========玩家的朝向===============
    private Vector2 lookDirection =new Vector2(1, 0);//默认朝右

    //===========玩家的子弹数量===========
    [SerializeField]
    private int maxBulletCount = 99;//最大子弹数量
    private int currentBulletCount = 0;//当前子弹数量

    public int MyBulletCount { get {  return maxBulletCount; } }
    public int CurrentBulletCount { get { return currentBulletCount; } }


    Rigidbody2D rbody;//刚体组件
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {   
        rbody = GetComponent<Rigidbody2D>();//获取刚体
        anim = GetComponent<Animator>();//获取动作

        currentHealth = 2;//当前生命值
        currentBulletCount = 2;//当前子弹数量
        invincibleTimer = 0;
        UImanager.instance.UpdateHealthBar(currentHealth, maxHealth);//设置初始血条
        UImanager.instance.UpdateBulletCount(currentBulletCount, maxBulletCount);//设置初始子弹数量
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");//控制水平移动
        float moveY = Input.GetAxisRaw("Vertical");//垂直

        Vector2 moveVector = new Vector2(moveX, moveY);
        if(moveVector.x != 0|| moveVector.y != 0)
        {
            lookDirection = moveVector;
        }
        anim.SetFloat("Look X",lookDirection.x);
        anim.SetFloat("Look Y", lookDirection.y);//朝向
        anim.SetFloat("Speed",moveVector.magnitude);//导入移动动画


        //========================移动==================================================
        Vector2 position = rbody.position;
        position += moveVector * speed * Time.deltaTime;


        rbody.MovePosition(position);
        // transform.Translate(transform.right * speed * Time.deltaTime);
        //=======================无敌计时================================================

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0)
            {
                isInvincible = false;//倒计时结束后（2秒），取消无敌状态
            }
        }
        //======按下J键并且子弹数量大于0，进行攻击
        if(Input.GetKeyDown(KeyCode.J) && currentBulletCount >0)
        {
            ChangeBulletCount(-1);//每次攻击子弹数量减一
            anim.SetTrigger("Launch");//攻击动画
            AudioManager.instance.AudioPlay(launchClip);//播放发射齿轮音效
            GameObject bullet = Instantiate(bulletPrefab,rbody.position + Vector2.up * 0.5f,Quaternion.identity);
            BulletC bc = bullet.GetComponent<BulletC>();
            if(bc != null)
            {
                bc.Move(lookDirection, 300);
            }
        }
        //=======按下E键与npc交互
        if (Input.GetKeyDown(KeyCode. E))
        {
            RaycastHit2D hit = Physics2D.Raycast(rbody.position, lookDirection, 2f, LayerMask.GetMask("NPC"));
            //给玩家朝向方向发射一条2米的射线
            if(hit.collider != null)
            {
                NPCManager npc = hit.collider.GetComponent<NPCManager>();
                if(npc != null)
                {
                    npc.ShowDialog();//显示对话框
                }
            }
        }
    }
    //无敌时间判断
    public void ChangeHealth(int amount)
    {   
        //如果玩家受到伤害
        if(amount < 0)
        {
            if(isInvincible == true)
            {
                return;
            }
            isInvincible = true;
            anim.SetTrigger("Hit");
            AudioManager.instance.AudioPlay(hitClip);//播放受伤音效
            invincibleTimer = invincibleTime;
        }


        
        Debug.Log(currentHealth + "/" + maxHealth);
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);//约束生命值在0和最大值之间
        UImanager.instance.UpdateHealthBar(currentHealth,maxHealth);//更新血条
        
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    public void ChangeBulletCount(int amount)
    {
        currentBulletCount = Mathf.Clamp(currentBulletCount + amount, 0, maxBulletCount);
        //限制子弹数量在0-99之间
        UImanager.instance.UpdateBulletCount(currentBulletCount,maxBulletCount);
    }

}
