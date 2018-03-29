using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        AddLuaFile();
        LuaManager.Instance.LuaEnv.DoString("require'main'");
    }

    // Update is called once per frame
    void Update()
    {
    }


    void AddLuaFile()
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
