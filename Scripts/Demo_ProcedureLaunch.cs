using System.Collections;
using System.Collections.Generic;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class Demo_ProcedureLaunch : ProcedureBase {
	protected override void OnEnter (ProcedureOwner procedureOwner) {
		base.OnEnter (procedureOwner);
        Log.Debug("Enter Launch Procedure");

        // 切换到菜单场景
        procedureOwner.SetData<VarString>("NextSceneName", "Assets/Unity/Scenes/Demo_Menu.unity");
		ChangeState<Demo_ProcedureChangeScene> (procedureOwner);
	}
}