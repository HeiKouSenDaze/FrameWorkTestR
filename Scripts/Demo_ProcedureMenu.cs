using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class Demo_ProcedureMenu : ProcedureBase {
    private bool m_StartGame = false;
    private MenuFormLogic m_MenuForm = null;
    /// <summary>
    /// 进入流程
    /// </summary>
    /// <param name="procedureOwner"></param>
    protected override void OnEnter (ProcedureOwner procedureOwner) {
        base.OnEnter (procedureOwner);
        Log.Debug("Enter Menu Procedure");
        Demo_GameEntry.Event.Subscribe (OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
        //加载该位置的ui预制体
        Demo_GameEntry.UI.OpenUIForm ("Assets/Unity/UI/UIForms/MenuForm.prefab", "DefaultGroup", this);
    }
    /// <summary>
    /// 每帧
    /// </summary>
    /// <param name="procedureOwner"></param>
    /// <param name="elapseSeconds"></param>
    /// <param name="realElapseSeconds"></param>
    protected override void OnUpdate (ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds) {
        base.OnUpdate (procedureOwner, elapseSeconds, realElapseSeconds);

        if (m_StartGame) {
            m_StartGame = false;

            // 切换到游戏场景
            procedureOwner.SetData<VarString> ("NextSceneName", "Assets/DemoStarForce/DemoSF_Game.unity");
            //切换到changescene流程
            ChangeState<Demo_ProcedureChangeScene> (procedureOwner);
        }
    }
    /// <summary>
    /// 离开流程
    /// </summary>
    /// <param name="procedureOwner"></param>
    /// <param name="isShutdown"></param>
    protected override void OnLeave (ProcedureOwner procedureOwner, bool isShutdown) {
        base.OnLeave (procedureOwner, isShutdown);

        // 离开时取消事件订阅
        Demo_GameEntry.Event.Unsubscribe (OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

        // 离开时关闭UI
        if (m_MenuForm != null) {
            Demo_GameEntry.UI.CloseUIForm (m_MenuForm.UIForm);
            m_MenuForm = null;
        }
    }

    private void OnOpenUIFormSuccess (object sender, GameEventArgs e) {
        OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs) e;

        // 判断userData是否为自己
        if (ne.UserData != this) {
            return;
        }

        Log.Debug ("OnOpenUIFormSuccess");

        m_MenuForm = (MenuFormLogic) ne.UIForm.Logic;
    }
    /// <summary>
    /// 开始游戏
    /// 可在按下开始按钮后被调用
    /// </summary>
    public void StartGame () {
        m_StartGame = true;
    }
}