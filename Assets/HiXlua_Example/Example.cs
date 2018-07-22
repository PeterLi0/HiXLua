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
        LuaManager.Instance.LuaEnv.DoString("require'main'");
        LuaManager.Instance.BindLuaFunction();

    }

    void InitLuaFile()
    {
        var mainLua = GetBytes("main.lua");
        LuaManager.Instance.AddLuaFileBytes("main", mainLua);
        var uiLua = GetBytes("ui.lua");
        LuaManager.Instance.AddLuaFileBytes("ui", uiLua);
        var scoreLua = GetBytes("score.lua");
        LuaManager.Instance.AddLuaFileBytes("score", scoreLua);
    }

    /// <summary>
    /// you can read lua bytes from local or web
    /// or you can do some encryption or decryption
    /// </summary>
    /// <returns></returns>
    byte[] GetBytes(string name)
    {
        string path = Application.dataPath + "/HiXLua/Example/Lua/" + name;
        return File.ReadAllBytes(path);
    }
}
