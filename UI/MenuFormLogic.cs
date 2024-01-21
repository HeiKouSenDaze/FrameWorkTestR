using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

public class MenuFormLogic : UIFormLogic
{
    private Demo_ProcedureMenu m_ProcedureMenu;
    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        m_ProcedureMenu = (Demo_ProcedureMenu)userData;
        if(m_ProcedureMenu == null )
        {
            return;
        }
    }
    /// <summary>
    /// 点击开始按钮
    /// </summary>
    public void OnButtonClick()
    {
        Log.Debug("OnButtonClick");
    }
}
