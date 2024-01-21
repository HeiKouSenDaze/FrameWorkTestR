using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 游戏入口。
/// </summary>
public partial class Demo_GameEntry : MonoBehaviour {
    private void Start () {
        InitBuiltinComponents ();
        InitCustomComponents ();
    }
}