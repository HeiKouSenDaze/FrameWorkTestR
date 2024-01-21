using System;
using GameFramework;
using GameFramework.DataTable;
using GameFramework.Event;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public partial class Demo_ProcedureChangeScene : ProcedureBase {
    private bool m_IsChangeSceneComplete = false;

    protected override void OnEnter (ProcedureOwner procedureOwner) {
        base.OnEnter (procedureOwner);
        Log.Debug("Enter Scene Changing Procedure");

        m_IsChangeSceneComplete = false;

        Demo_GameEntry.Event.Subscribe (LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);

        // 卸载所有场景
        string[] loadedSceneAssetNames = Demo_GameEntry.Scene.GetLoadedSceneAssetNames ();
        for (int i = 0; i < loadedSceneAssetNames.Length; i++) {
            Demo_GameEntry.Scene.UnloadScene (loadedSceneAssetNames[i]);
        }

        string nextSceneName = procedureOwner.GetData<VarString> ("NextSceneName").Value;

        Demo_GameEntry.Scene.LoadScene (nextSceneName, this);
    }

    protected override void OnLeave (ProcedureOwner procedureOwner, bool isShutdown) {
        Demo_GameEntry.Event.Unsubscribe (LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);

        base.OnLeave (procedureOwner, isShutdown);
    }

    protected override void OnUpdate (ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds) {
        base.OnUpdate (procedureOwner, elapseSeconds, realElapseSeconds);

        if (!m_IsChangeSceneComplete) {
            return;
        }

        string nextSceneName = procedureOwner.GetData<VarString> ("NextSceneName").Value;
        switch(nextSceneName)
        {
            case "Assets/Unity/Scenes/" + "Demo_Menu" + ".unity":
                ChangeState<Demo_ProcedureMenu> (procedureOwner);
            break;
            case "Assets/Unity/Scenes/" + "Demo_Game" + ".unity":
                //ChangeState<Demo_ProcedureGame> (procedureOwner);
            break;
        }
    }

    private void OnLoadSceneSuccess (object sender, GameEventArgs e) {
        LoadSceneSuccessEventArgs ne = (LoadSceneSuccessEventArgs) e;
        if (ne.UserData != this) {
            return;
        }

        m_IsChangeSceneComplete = true;
    }

}