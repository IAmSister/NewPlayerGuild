
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

    private bool m_bFold = true;           // �˵��۵�״̬
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
    public UISprite m_TeamCaptain;      //��Ӷӳ���־
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
        //�жϵ�ǰ���� 
        //�����Ѫ������ 
      
        PlayTween();
        SwitchAllWhenPopUIShow(!m_bFold);
        //������۵�
       
    }

    void PlayTween()
    {
        m_PlayerFrameWhole.SetActive(!m_bFold);
    }

    public void ChangeHP(int nCurHp, int nMaxHp)
    {
        ///����hp �����Ƿ�����������ʾ������� ����������
        //λ������
    }

    public void ChangeMP(int nCurMp, int nMaxMp)
    {
        //�����
        //�������
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
       //��������Ҫ���������
       //�����ö�Ӧ��������
    }

    /// <summary>
    /// Tween����������Ϻ�ı�ͷ��ťͼƬ
    /// </summary>
    public void AfterPlayTweenFold()
    {
    }

    /// <summary>
    /// Ӧ���л�����ʱUI�쳣��ʧ ���³�ʼ��Tween����
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
        // �ж��۵�״̬
       
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
        //float nFillAmount = (float)nValue / (float)maxXP * 0.65f + 0.3f;//�����ϵ��������ʱ���ε�
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

    //����ͷ���BUFFλ
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

    // ���¹���CD����ͼ�꣬5��һ����
    public void UpdateFunctionCD()
    {
        UpdateRemainNum();
       
    }
}