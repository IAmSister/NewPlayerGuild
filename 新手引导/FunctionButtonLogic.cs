using UnityEngine;
using System.Collections;
using GCGame;
using System.Collections.Generic;
using Games.LogicObj;
using Games.GlobeDefine;
using GCGame.Table;


public class FunctionButtonLogic : MonoBehaviour
{
    private static FunctionButtonLogic m_Instance = null;
    public static FunctionButtonLogic Instance()
    {
        return m_Instance;
    }

    private static bool m_bShowDetailButtons = false;

    public GameObject m_FirstChild;
    public GameObject m_FunctionButtonOffset;
    public UILabel LabelDoubleExpTimer;      // 双倍经验倒计时
    public GameObject m_PKNormalBt;
    public GameObject m_PKKillBt;
    public GameObject m_FunctionButtonGrid;
    public GameObject m_BtnOpenDetail;      // 打开详细界面
    public GameObject m_BtnCloseDetail;
    //public GameObject m_BtnCloseDetail;      // 关闭详细界面
    public GameObject m_BtnAutoBegin;
    public GameObject m_BtnAutoStop;
    public GameObject m_ExitFB;
    public GameObject m_CanHideButtonRoot;
    public GameObject m_RadarWindow;
    //public GameObject m_BtnOpenRadar;
    //public GameObject m_BtnCloseRadar;
    //public TweenPosition m_FunctionWindowTween;

    public GameObject m_TipsObject;

    public UILabel m_LabelTotalAimTips;
    public UILabel m_LabelTotalAimTips2;

    public UILabel m_LabelDaliyLuckyTip;
    public UIImageButton m_ButtonDailyLuckyDraw;

    // 活动相关
    public UILabel AwardTipsText;
    //=领取奖励tips
    public GameObject RewardTips;
    //======领取占星奖励tips
    public GameObject ZhanxingTips;
    private float m_updateTimer = 1.0f;

    // 新手引导
    private int m_NewPlayerGuide_Step = -1;
    public int NewPlayerGuide_Step
    {
        get { return m_NewPlayerGuide_Step; }
        set { m_NewPlayerGuide_Step = value; }
    }
    private bool m_Direction = false;
    public GameObject m_ButtonAct;
    public UISprite m_ButtonActTip;

    public UIGrid m_TipsGrid;
    public GameObject m_ButtonMailTip;
    // world boss
    public GameObject m_ButtonWorldBoss;
    public GameObject m_ButtonZhenQiAssit;
    public UILabel m_ZhenQiAssistCount;
    //摇钱树按钮
    public GameObject m_MoneyTreeButton;
    private GameObject m_NewButton = null;

    private string countNameStr = "ui_pub_029";

    //====tipsCount;
    private int tipsCount = 0;

    private int m_nExitTime = -1;
    public int ExitTime
    {
        get { return m_nExitTime; }
        set { m_nExitTime = value; }
    }

    public UILabel m_ExitTime;
    public GameObject m_AuotTeamCue;

    void Awake()
    {
     
      

        m_Instance = this;
        ShowFunctionButtons(false);
      
    }
    void FunctionSelfBtnTry()
    {
      
     
    }
    void Start()
    {
      
        InitUITweenerWhenChangeScene();
        initAwardActivityTips();
        UpdateMoneyTreeButton();
        UpdateAutoFightBtnState();
        UpdateDaliyLuckNum();
        m_FirstChild.SetActive(true);
        m_ExitTime.text = "";
       
       
       

        InitButtonActive();
        UpdateActionButtonTip();
        UpdateAutoTeamCue();
        UpdateRewardButtonTip();
        m_TipsGrid.repositionNow = true;
    }

    // Update is called once per frame
    void OnClickVIP(GameObject go)
    {
        //��VIP
       // UIManager.ShowUI(UIInfo.VipRoot);
    }
    void FixedUpdate()
    {
       
        UpdateRewardButtonTip();
        UpdateExitTime();
    }

    void OnDestroy()
    {
        //MailData.delMailUpdate -= OnMailUpdate;
        m_Instance = null;
    }

    public static float m_fTimeSecond = Time.realtimeSinceStartup;
    public void UpdateExitTime()
    {
        float ftimeSec = Time.realtimeSinceStartup;
        int nTimeData = (int)(ftimeSec - m_fTimeSecond);
        if (nTimeData > 0)
        {
            if (m_nExitTime > 0)
            {
                m_nExitTime = m_nExitTime - nTimeData;
                //m_ExitTime.text = "���븱������ʱ��:" + m_nExitTime.ToString() + "��";
                m_ExitTime.text = StrDictionary.GetClientDictionaryString("#{2835}", m_nExitTime);
                if (m_nExitTime <= 0)
                {
                    //                     CG_LEAVE_COPYSCENE packet = (CG_LEAVE_COPYSCENE)PacketDistributed.CreatePacket(MessageID.PACKET_CG_LEAVE_COPYSCENE);
                    //                     packet.NoParam = 1;
                    //                     packet.SendPacket();
                    m_ExitTime.text = "";
                    m_nExitTime = 0;
                }

            }
            m_fTimeSecond = ftimeSec;
        }
    }
    public void PlayTween(bool nDirection)
    {
        //BeforeClickPlayerFrame(nDirection);
        gameObject.SetActive(!nDirection);
        //foreach(TweenAlpha tween in m_FoldTween)
        //{
        //    tween.Play(nDirection);
        //}

        m_Direction = nDirection;
    }

    /// <summary>
    /// 应对切换场景时UI异常消失 重新初始化Tween动画
    /// </summary>
    void InitUITweenerWhenChangeScene()
    {
        //curTween.Reset();
        //curTween.alpha = 1;
        //foreach(TweenAlpha tween in m_FoldTween)
        //{
        //    tween.Reset();
        //    tween.alpha = 1;
        //}
    }

    //public void BeforeClickPlayerFrame(bool nDirection)
    //{
    //    foreach (BoxCollider box in gameObject.GetComponentsInChildren<BoxCollider>())
    //    {
    //        UIImageButton button = box.gameObject.GetComponent<UIImageButton>();
    //        if (button != null)
    //        {
    //            button.isEnabled = !nDirection;
    //        }
    //        else
    //        {
    //            box.enabled = !nDirection;
    //        }
    //    }
    //}

    void ShowSceneMap()
    {
        if (MainUILogic.Instance() != null)
        {
            NewPlayerGuidLogic.CloseWindow();
            //打开SceneMapRoot
            //  UIManager.ShowUI(UIInfo.SceneMapRoot);
        }
    }

    //==显示可领奖励提示
    public void UpdateRewardButtonTip()
    {
       


    }


    public void UpdateActionButtonTip()
    {
       // 默认不显示圆点，只要有一个条件满足的时候就显示圆点并返回。
        m_ButtonActTip.spriteName = "";

        //1日常


        //等级显示限制
        int nLevel = 0;
        //2江湖名人录

        //3聚贤庄11
        {


            if (nLevel < 20)
                return;

            //5藏经阁14
            {
                int nCount = 0;
                int nCur = 0;
                int nMax = 0;
                //  Utils.GetCopySceneCountsAll((int)GameDefine_Globe.SCENE_DEFINE.SCENE_HULAOGUAN, ref nCur, ref nMax);
                nCount += nCur;
                if (nCount > 0)
                {
                    m_ButtonActTip.spriteName = countNameStr;
                    return;
                }
            }
            //6燕子坞19
            {
                int nCount = 0;
                int nCur = 0;
                int nMax = 0;
                //  Utils.GetCopySceneCounts((int)GameDefine_Globe.SCENE_DEFINE.SCENE_WUSHENTA, 1, 1, ref nCur, ref nMax);
                nCount += nCur;
                // Utils.GetSweepCounts(ref nCur, ref nMax);
                nCount += nCur;

                if (nCount > 0)
                {
                    m_ButtonActTip.spriteName = countNameStr;
                    return;
                }
            }
            ////6燕子坞19
            {
                int nCount = 0;
                int nCur = 0;
                int nMax = 0;
                //  Utils.GetCopySceneCountsAll((int)GameDefine_Globe.SCENE_DEFINE.SCENE_HUSONGMEIREN, ref nCur, ref nMax);
                nCount += nCur;

                if (nCount > 0)
                {
                    m_ButtonActTip.spriteName = countNameStr;
                    return;
                }
            }
            //7珍珑棋局28
            {
                int nCount = 0;
                int nCur = 0;
                int nMax = 0;
                // Utils.GetCopySceneCountsAll((int)GameDefine_Globe.SCENE_DEFINE.SCENE_QIXINGXUANZHEN, ref nCur, ref nMax);
                nCount += nCur;

                if (nCount > 0)
                {
                    m_ButtonActTip.spriteName = countNameStr;
                    return;
                }
            }
            //8燕王古墓27
            {
                int nCount = 0;
                int nCur = 0;
                int nMax = 0;
                Utils.GetCopySceneCountsAll((int)GameDefine_Globe.SCENE_DEFINE.SCENE_YEXIDAYING, ref nCur, ref nMax);
                nCount += nCur;

                if (nCount > 0)
                {
                    m_ButtonActTip.spriteName = countNameStr;
                    return;
                }
            }
            //9 天降奇珍
            {

            }
            ///13怒海锄奸7
            {
                int nCount = 0;
                int nCur = 0;
                int nMax = 0;
                Utils.GetCopySceneCounts((int)GameDefine_Globe.SCENE_DEFINE.SCENE_GUOGUANZHANJIANG, 1, 1, ref nCur, ref nMax);
                nCount += nCur;

                if (nCount > 0)
                {
                    m_ButtonActTip.spriteName = countNameStr;
                    return;
                }
            }
            //10少室山31
            {
                //            // 少室山等级要求
                //            if (Singleton<ObjManager>.Instance.MainPlayer.BaseAttr.Level >= 70)
                //            {
                int nCount = 0;
                int nCur = 0;
                int nMax = 0;
                Utils.GetCopySceneCounts((int)GameDefine_Globe.SCENE_DEFINE.SCENE_FENGHUOLIANTIAN, 2, 1, ref nCur, ref nMax);
                nCount += nCur;

                if (nCount > 0)
                {
                    m_ButtonActTip.spriteName = countNameStr;
                    return;
                }
                //            }
            }

        }
        void OnShopClick()
        {
            //新手指引关掉
            NewPlayerGuidLogic.CloseWindow();

            //UIManager.ShowUI(UIInfo.SysShop);
            //UIManager.ShowUI(UIInfo.YuanBaoShop);
            // 需要看元宝商店是否开启

        }
        void OnChongZhiClick()
        {
        }


        void UpdateDoubleExpTimer()
        {
            if (null == LabelDoubleExpTimer)
            {
                // label��С�ı�ɾʱ����ֹ���쳣
                return;
            }
            m_updateTimer -= Time.deltaTime;
            if (m_updateTimer <= 0)
            {
                m_updateTimer = 1.0f;

            }
        }

    }

    public void initAwardActivityTips()
    {
        CleanUpAwardActivityTips();
        UpdateButtonAwardTips();
    }

    public void CleanUpAwardActivityTips()
    {
        if (AwardTipsText == null)
        {
         
            return;
        }
        AwardTipsText.text = "";
        AwardTipsText.gameObject.SetActive(false);
    }

    public void UpdateButtonAwardTips()
    {
        if (AwardTipsText == null)
        {
            return;
        }
        if (m_LabelTotalAimTips == null)
        {
            return;
        }
        if (m_LabelTotalAimTips2 == null)
        {
            return;
        }
        // �߻�Ҫ�� �ӵȼ����� 5��
        //if ( nLevel >= 5)
        // ÿ�ս���
        // δ��ȡ
        // ���߽���
        // ���ܽ���
        // ����7�콱��
      


    }

    void ShowAwardUI()
    {
        if (MainUILogic.Instance() != null)
        {
           // UIManager.ShowUI(UIInfo.AwardRoot);
        }
    }

    void OnLingJiangBtnFun()
    {
        if (MainUILogic.Instance() != null)
        {
            ///TYPE_POP
            //UIManager.ShowUI(UIInfo.RewardRoot);
        }
    }

    void OnActivityClick(GameObject value)
    {
        if (m_NewButton != null && m_NewButton == value)
        {
            StopNewButtonEffect();
        }

       // UIManager.ShowUI(UIInfo.Activity);
    }

    void OnPKClick()
    {
        NewPlayerGuidLogic.CloseWindow();

       // UIManager.ShowUI(UIInfo.PKSetInfo);
    }

    public void NewPlayerGuide(int nIndex)
    {
        if (m_Direction == true)
        {
            return;
        }

        if (nIndex < 0)
        {
            return;
        }

        NewPlayerGuidLogic.CloseWindow();

        m_NewPlayerGuide_Step = nIndex;

        switch (nIndex)
        {
            case (int)GameDefine_Globe.NEWOLAYERGUIDE.ACTIVITY_MISSION: // �ճ�����
            case (int)GameDefine_Globe.NEWOLAYERGUIDE.ACTIVITY_QUNXIONG:// ���֮��
            case (int)GameDefine_Globe.NEWOLAYERGUIDE.ACTIVITY_QIXINGXUANZHEN:// �ؾ���
            case (int)GameDefine_Globe.NEWOLAYERGUIDE.ACTIVITY_HULAOGUAN:// ������
            case (int)GameDefine_Globe.NEWOLAYERGUIDE.ACTIVITY_WUSHENTA:// ����ׯ
            case (int)GameDefine_Globe.NEWOLAYERGUIDE.ACTIVITY_HUSONGMEIREN:// �������
            case (int)GameDefine_Globe.NEWOLAYERGUIDE.ACTIVITY_GUOGUANZHANJIANG:// �������
            case (int)GameDefine_Globe.NEWOLAYERGUIDE.ACTIVITY_FENGHUOLIANTIAN:// �������
            case (int)GameDefine_Globe.NEWOLAYERGUIDE.ACTIVITY_YEXIDAYIN:// �������
                {
                    NewPlayerGuidLogic.OpenWindow(m_ButtonAct, 106, 106, "", "left", 2, true, true);
                }
                break;
            case (int)GameDefine_Globe.NEWOLAYERGUIDE.DIVI_MONEY:
            case (int)GameDefine_Globe.NEWOLAYERGUIDE.DIVI_DRAW:
                {
                    if (false == m_CanHideButtonRoot.activeSelf)
                    {
                        NewPlayerGuidLogic.OpenWindow(m_BtnOpenDetail.gameObject, 106, 106, "", "left", 2, true, true);
                        break;
                    }
                    NewPlayerGuidLogic.OpenWindow(m_ButtonDailyLuckyDraw.gameObject, 106, 106, "", "left", 2, true, true);
                    break;
                }
            case (int)GameDefine_Globe.NEWOLAYERGUIDE.EXITDUNGEON: // �뿪����
                if (null != m_ExitFB && m_ExitFB.activeSelf)
                {
                    NewPlayerGuidLogic.OpenWindow(m_ExitFB, 106, 106, "", "left", 2, true, true);
                }
                break;
        }
    }

    void OnMailUpdate()//MailData.MailUpdateType curUpdateType, System.UInt64 curKey
    {
        
    }

    void OnMailTipClick()
    {
        if (NewPlayerGuidLogic.Instance())
        {
            NewPlayerGuidLogic.CloseWindow();
        }
        m_ButtonMailTip.gameObject.SetActive(false);
      
    }

    //ÿ�����˳齱���
    void OnDailyLuckyDrawClick()
    {
       
    }

    // 
    void OnDivinationClick()
    {
        if (NewPlayerGuidLogic.Instance())
        {
            NewPlayerGuidLogic.CloseWindow();
        }
       
    }

    public void UpdateMoneyTreeButton()
    {
        

        if (m_MoneyTreeButton == null)
        {
            return;
        }
      
    }
    public void OnExitFBClick()
    {
        if ((int)GameDefine_Globe.NEWOLAYERGUIDE.EXITDUNGEON == m_NewPlayerGuide_Step)
        {
            NewPlayerGuidLogic.CloseWindow();
            m_NewPlayerGuide_Step = -1;
        }

        string str = StrDictionary.GetClientDictionaryString("#{1847}");
        
      
      

    }
    public void OnLeaveCopySceneOK()
    {
        
    }
    public void OnLEaveCopySceneNO()
    {

    }

    public void OpenFunction(int nType)
    {
      
    }

    public void LevelUpButtonActive()
    {
        return;
       
    }

    public void InitButtonActive()
    {

        if (m_ButtonAct == null)
        {
            return;
        }

      
    }

    public void StopNewButtonEffect()
    {
        if (m_NewButton == null)
        {
            return;
        }

        Transform effectTrans = m_NewButton.transform.Find("EffectPoint");
        if (effectTrans == null)
        {
            return;
        }

        Transform spriteAniTrans = effectTrans.Find("SpriteAni");
        if (spriteAniTrans == null)
        {
            return;
        }

        spriteAniTrans.gameObject.SetActive(false);

        m_NewButton = null;
    }

    public void PlayNewButtonEffect()
    {
        if (m_NewButton == null)
        {
            return;
        }

        Transform effectTrans = m_NewButton.transform.Find("EffectPoint");
        if (effectTrans == null)
        {
            return;
        }

        Transform spriteAniTrans = effectTrans.Find("SpriteAni");
        if (spriteAniTrans == null)
        {
            return;
        }

        spriteAniTrans.gameObject.SetActive(true);
    }

    public void OnWorldBossClick()
    {
       
    }

    public void OnWorldBossDead()
    {
        if (m_ButtonWorldBoss != null)
            m_ButtonWorldBoss.SetActive(false);
        //HuaShanPVPData.WorldBossOpen = 0;
    }

    public void OnWorldBossStateChange(int state)
    {
        if (state != 2)
        {
            if (m_ButtonWorldBoss != null)
                m_ButtonWorldBoss.SetActive(false);
            //HuaShanPVPData.WorldBossOpen = 0;
        }
        else
        {
            if (m_ButtonWorldBoss != null)
                m_ButtonWorldBoss.SetActive(true);
            //HuaShanPVPData.WorldBossOpen = 1;

            m_TipsGrid.repositionNow = true;
        }
    }

    public void ZhenQiAssistState(int state, int times)
    {
        if (m_ZhenQiAssistCount != null)
        {
            if (state == 0)
                m_ZhenQiAssistCount.text = "";
            else
            {
                m_ZhenQiAssistCount.text = StrDictionary.GetClientDictionaryString("#{2143}", times);
            }
        }
        if (m_ButtonZhenQiAssit != null)
        {
            m_ButtonZhenQiAssit.SetActive(state == 1 ? true : false);
        }
    }

    public void ZhenQiAssistButtonClick()
    {
       
    }



    // ֱ�ӵ����Զ�ս��
    void OnDoAutoFightClick()
    {
        if (m_NewPlayerGuide_Step == 1)
        {
            NewPlayerGuidLogic.CloseWindow();
            m_NewPlayerGuide_Step = -1;
        }

       
        UpdateAutoFightBtnState();
    }

    void OnDoAutoStopFightClick()
    {
        if (m_NewPlayerGuide_Step == 3)
        {
            NewPlayerGuidLogic.CloseWindow();
            m_NewPlayerGuide_Step = -1;
        }
        //停止自动战斗
        UpdateAutoFightBtnState();
    }

    void ShowFunctionButtons(bool bShow)
    {
       

        m_BtnCloseDetail.gameObject.SetActive(bShow);
        m_BtnOpenDetail.gameObject.SetActive(!bShow);
        m_CanHideButtonRoot.SetActive(bShow);
        m_RadarWindow.SetActive(!bShow);
        m_bShowDetailButtons = bShow;

        changeTipsObj(bShow);

        NewPlayerGuide(m_NewPlayerGuide_Step);
    }

    private void changeTipsObj(bool boo)
    {

        if (tipsCount > 0)
        {
            if (boo)
                m_TipsObject.gameObject.SetActive(false);
            else
                m_TipsObject.gameObject.SetActive(true);
        }
    }


    public void UpdateAutoFightBtnState()
    {
      
    }

    public void UpdateDaliyLuckNum()
    {
        
    }
    public void UpdateDailyLuckyButton()
    {
        
    }

    void OnShowDetailButtons()
    {
        ShowFunctionButtons(true);
    }

    void OnHideDetailButtions()
    {
        ShowFunctionButtons(false);
    }
    public void UpdateAutoTeamCue()
    {
      

    }
}
