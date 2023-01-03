
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewPlayerGuide
{
    private static Dictionary<string, UIPathData> m_NewPlayerGuideUI;

    // �ر�2�����棬���ز˵���
    static void CloseUI()
    {
        //UI���� �ر�������������
       
        // ���ز˵���
     
    }

    // ��� ��ʾ����ָ����UI����
    static void InitGuide()
    {
        //�洢���е� ��Ҫִ�е����·��
        //����ҳ���йؼ��ڵ�����
        m_NewPlayerGuideUI = new Dictionary<string, UIPathData>();
      
    }
    /// <summary>
    /// ������������
    /// </summary>
    /// <param name="strUIName"></param>
    /// <param name="nIndex"></param>
    public static void NewPlayerGuideOpt(string strUIName, int nIndex)
    {
        //��������һЩ��������ж�

      

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

    // ������������ �������Ľ�ѧ
    static void PlayerFrameUI(int nIndex)
    {
       
    }

    // ���ܰ�ť
    static void SkillBarUI(int nIndex)
    {
        //if (SkillBarLogic.Instance())
        //{
        //    SkillBarLogic.Instance().NewPlayerGuide(nIndex);
        //}
    }

    // ���Ͻǰ�ť FunctionButton
    static void FunctionButtonUI(int nIndex)
    {
      
    }
    //ң������ָ��  //ÿһ����������ִ�в�ͬ�����������߼�
    static void JoyStickUI(int nIndex)
    {
       
    }
    //װ��
    static void EquipRemindUI(int nIndex)
    {
       
    }
    //����
    static void NewItemGetUI(int nIndex)
    {
        //if (NewItemGetLogic.Instance())
        //{
        //    NewItemGetLogic.Instance().NewPlayerGuider(nIndex);
        //}
    }
}
