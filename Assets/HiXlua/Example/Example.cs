﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using HiXlua;
using UnityEngine;
using XLua;

public class Example : MonoBehaviour
{
    void Awake()
    {
        new GameObject("LuaManager").AddComponent<LuaManager>();
    }

    [CSharpCallLua]
    public delegate void LuaFunctionCallBack();


    public LuaFunctionCallBack luaUpdate;
    // Use this for initialization
    void Start()
    {
        AddLuaFile();
        LuaManager.Instance.LuaEnv.DoString("require'main'");


        luaUpdate = LuaManager.Instance.LuaEnv.Global.Get<LuaFunctionCallBack>("Update");
    }

    // Update is called once per frame
    void Update()
    {
        luaUpdate();
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
