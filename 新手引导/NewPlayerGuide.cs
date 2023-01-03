
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewPlayerGuide
{
    private static Dictionary<string, UIPathData> m_NewPlayerGuideUI;

    // 关闭2级界面，隐藏菜单栏
    static void CloseUI()
    {
        //UI管理 关闭新手引导界面
       
        // 隐藏菜单栏
     
    }

    // 添加 显示新手指引的UI如下
    static void InitGuide()
    {
        //存储所有的 需要执行的面板路径
        //缓存页面中关键节点做键
        m_NewPlayerGuideUI = new Dictionary<string, UIPathData>();
      
    }
    /// <summary>
    /// 新手引导操作
    /// </summary>
    /// <param name="strUIName"></param>
    /// <param name="nIndex"></param>
    public static void NewPlayerGuideOpt(string strUIName, int nIndex)
    {
        //上来进行一些特殊操作判断

      

        switch (strUIName)
        {
            case "PlayerFrame":
                PlayerFrameUI(nIndex);
                break;
            case "SkillBar":
                SkillBarUI(nIndex);
                break;
            case "FunctionButton":
                FunctionButtonUI(nIndex);
                break;
            //case "JoyStick":
            //    JoyStickUI(nIndex);
            //    break;
            case "EquipRemindRoot":
                EquipRemindUI(nIndex);
                break;
            case "NewItemGet":
                NewItemGetUI(nIndex);
                break;
            case "PlayerAimBar":
                //PlayerAimBarUI(nIndex);
                break;
            default:
                break;
        }

    }

    // 人物点击，包括 点击进入的教学
    static void PlayerFrameUI(int nIndex)
    {
       
    }

    // 技能按钮
    static void SkillBarUI(int nIndex)
    {
        //if (SkillBarLogic.Instance())
        //{
        //    SkillBarLogic.Instance().NewPlayerGuide(nIndex);
        //}
    }

    // 右上角按钮 FunctionButton
    static void FunctionButtonUI(int nIndex)
    {
      
    }
    //遥感新手指导  //每一个管理类中执行不同的新手引导逻辑
    static void JoyStickUI(int nIndex)
    {
       
    }
    //装备
    static void EquipRemindUI(int nIndex)
    {
       
    }
    //标题
    static void NewItemGetUI(int nIndex)
    {
        //if (NewItemGetLogic.Instance())
        //{
        //    NewItemGetLogic.Instance().NewPlayerGuider(nIndex);
        //}
    }
}
