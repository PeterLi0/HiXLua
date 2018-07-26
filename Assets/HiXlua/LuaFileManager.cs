/*
 * lua源文件管理
 * 编辑器模式下直接加载使用lua源码，方便开发
 * 发布时先加密lua文件为二进制字节，然后解密为内存中的lua源码（非解压到手机目录，这样用户也会获得源文件）
 */

using System;
using System.Collections.Generic;

/// <summary>
/// Lua源码管理
/// </summary>
public class LuaFileManager
{
    /// <summary>
    /// 缓存lua代码（已解密）
    /// </summary>
    private Dictionary<string, byte[]> luaFiles = new Dictionary<string, byte[]>();

    /// <summary>
    /// 获取lua
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public byte[] GetLuaFile(string name)
    {
        if (luaFiles.ContainsKey(name))
            return luaFiles[name];
        throw new Exception(string.Format("Can't find lua file with name[{0}]", name));
    }


    /// <summary>
    /// 此接口是lua文件维护的最基础接口
    /// 1.如果用户主动下载加密的lua文件，解密，然后调用该接口
    /// 2.如果放在其他文件中的lua（比如lua在asserbundle中），可以读取后调用该接口
    /// 3.InitLuaFile（）最终也会调用该接口
    /// </summary>
    /// <param name="name"></param>
    /// <param name="luaBytes"></param>
    public void InitLuaFile(string name, byte[] luaBytes)
    {
        if (luaFiles.ContainsKey(name))
        {
            throw new Exception(string.Format("Already contain this lua file with name[{0}]", name));
        }
        luaFiles.Add(name, luaBytes);
    }

    /// <summary>
    /// 遍历文件夹下的所有lua文件并添加
    /// 此接口适用：
    /// 1.编辑器模式下使用，传入lua存放的文件夹根目录
    /// 2.服务器下载lua代码，并本地明文存放（比如在persistent文件夹下存放lua明文文件），下载完成后传入lua根目录
    /// 不适用加密的文件！！！
    /// 不适用直接放在AssertBundle包中的文件！！！
    /// </summary>
    /// <param name="path"></param>
    public void InitLuaFile(string path)
    {

    }
}