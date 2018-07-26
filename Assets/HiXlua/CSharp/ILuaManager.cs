/***************************************************************
 * Description: lua管理类接口
 *
 * Documents: https://github.com/hiramtan/HiXLua
 * Author: hiramtan@live.com
***************************************************************/
using XLua;

namespace HiXlua
{
    /// <summary>
    /// 
    /// </summary>
    interface ILuaManager
    {
        /// <summary>
        /// Lua环境
        /// </summary>
        LuaEnv LuaEnv { get; }

        /// <summary>
        /// 遍历文件夹下的所有lua文件并添加
        /// 此接口适用：
        /// 1.编辑器模式下使用，传入lua存放的文件夹根目录
        /// 2.服务器下载lua代码，并本地明文存放（比如在persistent文件夹下存放lua明文文件），下载完成后传入lua根目录
        /// 不适用加密的文件！！！
        /// 不适用直接放在AssertBundle包中的文件！！！
        /// </summary>
        /// <param name="path"></param>
        void InitLuaFile(string path);

        /// <summary>
        /// 此接口是lua文件维护的最基础接口
        /// 1.如果用户主动下载加密的lua文件，解密，然后调用该接口
        /// 2.如果放在其他文件中的lua（比如lua在asserbundle中），可以读取后调用该接口
        /// 3...其他获取途径，总之传入lua文件名和lua源码
        /// 3.InitLuaFile（）最终也会调用该接口
        /// </summary>
        /// lua文件名和对应的二进制文件,当加载关联lua文件时使用
        /// lua文件一般不会以源文件存在发布的项目中
        /// 可以在调用该接口前执行lua文件解密,然后传入二进制文件
        /// <param name="name"></param>
        /// <param name="luaBytes"></param>
        void InitLuaFile(string name, byte[] luaBytes);

        /// <summary>
        /// 绑定lua方法
        /// </summary>
        void BindLuaFunction();

        /// <summary>
        /// 销毁
        /// </summary>
        void Destory();
    }
}
