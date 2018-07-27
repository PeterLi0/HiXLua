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
    public enum Mode
    {
        Editor, //编辑器模式
        Release //发布模式
    }

    public Mode CurrentMode = Mode.Editor;

    // Use this for initialization
    void Start()
    {
        new GameObject("LuaManager").AddComponent<LuaManager>();
        if (CurrentMode == Mode.Editor)
            LuaManager.Instance.InitLuaFile(Application.dataPath + "/HiXlua_Example/Lua");
        else
            InitLuaFileRealeaseMode();
        LuaManager.Instance.LuaEnv.DoString("require'Main'");
        LuaManager.Instance.BindLuaFunction();
    }

    void InitLuaFileRealeaseMode()
    {
        //1.get lua bytes
        //2.add to lua dictionary
        //LuaManager.Instance.InitLuaFile("luaName",luaBytes);
    }
}