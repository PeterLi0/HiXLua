/***************************************************************
 * Description:Lua调用C#类需要提前在此绑定,生成代码
 *
 * Documents: https://github.com/hiramtan/HiXLua
 * Author: hiramtan@live.com
***************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class HiXluaGenWrap
{
    [LuaCallCSharp]
    public static List<Type> mymodule_lua_call_cs_list = new List<Type>()
    {
        typeof(GameObject),
        typeof(Dictionary<string, int>),


        //todo 扩展Lua调用C#类型
    };
}