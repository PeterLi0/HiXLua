/*
 * lua源文件管理
 * 编辑器模式下直接加载使用lua源码，方便开发
 * 发布时先加密lua文件为二进制字节，然后解密为内存中的lua源码（非解压到手机目录，这样用户也会获得源文件）
 */

using System.Collections.Generic;

/// <summary>
/// Lua源码管理
/// </summary>
public class LuaFileManager
{
    public enum RunMode
    {
        Editor,//编辑器模式下
        Build//正式发布环境
    }

    private Dictionary<string, byte[]> luaFiles = new Dictionary<string, byte[]>();



}