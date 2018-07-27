/***************************************************************
 * Description: 绑定CSharp调用lua方法
 * 统一绑定lua方法
 * Documents: https://github.com/hiramtan/HiXLua
 * Author: hiramtan@live.com
***************************************************************/
using XLua;

namespace HiXlua
{
    /// <summary>
    /// 无参数委托
    /// </summary>
    [CSharpCallLua]
    public delegate void LuaFunction_NoParam();

    /// <summary>
    /// int参数委托
    /// </summary>
    /// <param name="args"></param>
    [CSharpCallLua]
    public delegate void LuaFunction_Int(int args);

    /// <summary>
    /// float参数委托
    /// </summary>
    /// <param name="args"></param>
    [CSharpCallLua]
    public delegate void LuaFunction_float(float args);

    /// <summary>
    /// string参数委托
    /// </summary>
    /// <param name="args"></param>
    [CSharpCallLua]
    public delegate void LuaFunction_String(string args);

    [CSharpCallLua]
    public delegate void LuaFunction_Int2(int args1, int args2);

    //扩展
    //...
}