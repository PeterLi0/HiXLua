/***************************************************************
 * Description:
 *
 * Documents: https://github.com/hiramtan/HiXLua
 * Author: hiramtan@live.com
***************************************************************/

using HiXlua;
using UnityEngine;

public class Example : MonoBehaviour
{
    void Awake()
    {
        new GameObject("LuaManager").AddComponent<LuaManager>();
    }

    // Use this for initialization
    void Start()
    {
        LuaManager.Instance.InitLuaFile(Application.dataPath + "/HiXlua_Example/Lua");
        LuaManager.Instance.LuaEnv.DoString("require'Main'");
        LuaManager.Instance.BindLuaFunction();
    }
}