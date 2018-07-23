using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class BindCSharpType
{
    [LuaCallCSharp]
    public static List<Type> mymodule_lua_call_cs_list = new List<Type>()
    {
        typeof(GameObject),
        typeof(Dictionary<string, int>),
        //todo 扩展Lua调用C#类型
    };
}