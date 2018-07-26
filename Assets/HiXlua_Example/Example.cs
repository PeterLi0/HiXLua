/***************************************************************
 * Description:
 *
 * Documents: https://github.com/hiramtan/HiXLua
 * Author: hiramtan@live.com
***************************************************************/

using HiXlua;
using System.IO;
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
        InitLuaFile();
        LuaManager.Instance.LuaEnv.DoString("require'Main'");
        LuaManager.Instance.BindLuaFunction();
    }


    void InitLuaFile()
    {
      LuaManager.Instance.InitLuaFile(Application.dataPath+ "/HiXlua_Example/Lua");
    }
}
