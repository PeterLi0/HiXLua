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
        var mainLua = GetBytes();
        LuaManager.Instance.AddLuaFileBytes("main", mainLua);
        LuaManager.Instance.LuaEnv.DoString("require'main'");
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// you can read lua bytes from local or web
    /// </summary>
    /// <returns></returns>
    byte[] GetBytes()
    {
        string path = Application.dataPath + "/HiXLua/Example/Lua/main.lua";
        return File.ReadAllBytes(path);
    }
}
