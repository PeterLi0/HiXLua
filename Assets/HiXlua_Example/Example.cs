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
        var mainLua = GetBytes("Main.lua");
        LuaManager.Instance.AddLuaFileBytes("Main", mainLua);
        var joystick = GetBytes("Joystick.lua");
        LuaManager.Instance.AddLuaFileBytes("Joystick", joystick);
        //var scoreLua = GetBytes("score.lua");
        //LuaManager.Instance.AddLuaFileBytes("score", scoreLua);
    }

    /// <summary>
    /// you can read lua bytes from local or web
    /// or you can do some encryption or decryption
    /// </summary>
    /// <returns></returns>
    byte[] GetBytes(string name)
    {
        string path = Application.dataPath + "/HiXLua_Example/Lua/" + name;
        return File.ReadAllBytes(path);
    }
}
