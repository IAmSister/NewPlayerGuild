
using Games.GlobeDefine;
using Games.LogicObj;
using UnityEngine;
using System.Collections;
using GCGame.Table;
using System.Collections.Generic;
using GCGame;
using System;

public class PlayerFrameLogic : MonoBehaviour
{

    private static PlayerFrameLogic m_Instance = null;
    public static PlayerFrameLogic Instance()
    {
        return m_Instance;
    }

    private bool m_bFold = true;           // 菜单折叠状态
    public bool Fold
    {
        get { return m_bFold; }
    }

    public GameObject m_EffectNvqi;

    public GameObject m_FirstChild;
    public UIButtonMessage m_PlayerHeadButton;
    public UILabel m_PlayerLevel;
    public UISprite m_PlayerHPSprite;
    public UISprite m_PlayerHPBakSprite;
    public UISprite m_PlayerMPSprite;
    public UISprite m_PlayerMPBakSprite;
    public UILabel m_PlayerMPText;
    public UILabel m_PlayerHPText;
    public UISprite m_PlayerHeadSprite;
    public UILabel m_PlayerNameLabel;
    public GameObject m_PlayerFrameInfo;
    //public List<TweenAlpha> m_FoldTween;
    public UISprite m_SkillXPEnergy;

    public UILabel m_ItemDesc;
    public UISprite m_TeamCaptain;      //组队队长标志
    private bool m_IsShowItemDesc = false;


    public GameObject m_RemindNum;
    public UILabel m_RemindNumLabel;
    private int m_CurRemindNum = 0;

    private int m_nLastHp = 0;
    private int m_nLastMp = 0;

    public enum BUFFICON
    {
        MAX_BUFFICONUM
    }
    public GameObject m_PlayerFrameWhole;
    public UISprite[] m_BuffShowIcon = new UISprite[(int)BUFFICON.MAX_BUFFICONUM];

    public UILabel m_VipImage;
    public GameObject m_VipRoot;

    public UILabel CombatValue;
    void Awake()
    {
        m_Instance = this;
        m_EffectNvqi.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {

        InitUI();

    }

    //=========
    public void InitUI()
    {
        
        m_FirstChild.SetActive(true);

        m_PlayerLevel.text = "0";
        m_PlayerHPText.text = "0/0";
        m_PlayerHPSprite.fillAmount = 0;

        m_PlayerMPSprite.fillAmount = 0;
        m_TeamCaptain.gameObject.SetActive(false);

        InitXPEnergySlot();
        UpdateData();
        InitUITweenerWhenChangeScene();

        UpdateBuffIcon();
       // CombatValue.text = GameManager.PlayerDataPool.PoolCombatValue.ToString();
        InvokeRepeating("UpdateFunctionCD", 0, 5);
    }


    void OnEnable()
    {
       // CombatValue.text = GameManager.gameManager.PlayerDataPool.PoolCombatValue.ToString();
    }

    void OnDestroy()
    {
        m_Instance = null;
    }

    public void OnCombatValueChange(int newValue)
    {
      //  CombatValue.text = GameManager.gameManager.PlayerDataPool.PoolCombatValue.ToString();
    }
    public void PlayerFrameHeadOnClick()
    {
        //判断当前场景 
        //朱进度血条管理 
      
        PlayTween();
        SwitchAllWhenPopUIShow(!m_bFold);
        //如果不折叠
       
    }

    void PlayTween()
    {
        m_PlayerFrameWhole.SetActive(!m_bFold);
    }

    public void ChangeHP(int nCurHp, int nMaxHp)
    {
        ///更新hp 计算是否死亡死亡显示死亡面板 死亡后隐藏
        //位置设置
    }

    public void ChangeMP(int nCurMp, int nMaxMp)
    {
        //找玩家
        //计算界限
    }

    public void ChangeLev(int nLev)
    {
        m_PlayerLevel.text = nLev.ToString();
    }

    public void ChangeHeadPic(string strPic)
    {
        m_PlayerHeadSprite.spriteName = strPic;
    }



    public void ChangeName(string strName)
    {

        m_PlayerNameLabel.text = strName;
    }
    public void UpdateData()
    {
       //更新所需要计算的数据
       //并调用对应方法计算
    }

    /// <summary>
    /// Tween动画播放完毕后改变头像按钮图片
    /// </summary>
    public void AfterPlayTweenFold()
    {
    }

    /// <summary>
    /// 应对切换场景时UI异常消失 重新初始化Tween动画
    /// </summary>
    void InitUITweenerWhenChangeScene()
    {
        BoxCollider[] box = gameObject.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < box.Length; ++i)
        {
            if (null != box[i])
                box[i].enabled = true;
        }
    }

    public void NewPlayerGuide(int nIndex)
    {
        // 判断折叠状态
       
    }

   
    private static float m_fCDTimeSecond = Time.realtimeSinceStartup;
    private static float m_fItemDescSecound = 0;

    public void OnItemClick()
    {
        if (m_IsShowItemDesc == false)
        {
            m_ItemDesc.gameObject.SetActive(true);
            m_IsShowItemDesc = true;
        }
    }

    void InitXPEnergySlot()
    {
        m_SkillXPEnergy.fillAmount = 0;

    }

    public void ChangeXPEnergy(int nValue, int maxXP)
    {
        // m_SkillXPEnergy.UpdateEnergy(nValue, maxXP);
        //float nFillAmount = (float)nValue / (float)maxXP * 0.65f + 0.3f;//不清楚系数作用暂时屏蔽掉
        float nFillAmount = (float)nValue / (float)maxXP;
        m_SkillXPEnergy.fillAmount = nFillAmount;
        if (nFillAmount >= 1.0f)
        {
            m_EffectNvqi.SetActive(true);
        }
        else
        {
            m_EffectNvqi.SetActive(false);
        }
    }

    static void PlayXPEffect(bool showEffect)
    {
       
    }

    public void OnVipCostChange(int nCost)
    {
        if (nCost > 0)
        {
            m_VipRoot.gameObject.SetActive(true);
            int nLevel = 0;
            int nLeft = 0;
          //  VipData.GetVipLevel(nCost, ref nLevel, ref nLeft);
            m_VipImage.text = nLevel.ToString();
        }
        else
        {
            m_VipRoot.gameObject.SetActive(false);
        }
    }

    public void AddRemindNum()
    {
        m_CurRemindNum += 1;
        UpdateRemainNum();
    }

    int GetPartnerTipCount()
    {
        return 0;
    }

    int GetMasterAndGuildTipCount()
    {
      
        return 0;
    }

    public void UpdateRemainNum()
    {
       
    }

    //更新头像的BUFF位
    public void UpdateBuffIcon()
    {
        for (int i = 0; i < (int)BUFFICON.MAX_BUFFICONUM; i++)
        {
            m_BuffShowIcon[i].gameObject.SetActive(false);
        }


       
    }

    public void SwitchAllWhenPopUIShow(bool isShow)
    {
    }

    public void SetTeamCaptain(bool bActive)
    {
        if (null != m_TeamCaptain)
        {
            m_TeamCaptain.gameObject.SetActive(bActive);
        }
    }

    // 更新功能CD提醒图标，5秒一更新
    public void UpdateFunctionCD()
    {
        UpdateRemainNum();
       
    }
}