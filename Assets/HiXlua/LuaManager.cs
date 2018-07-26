/***************************************************************
 * Description: lua管理类
 *
 * Documents: https://github.com/hiramtan/HiXLua
 * Author: hiramtan@live.com
***************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using HiFramework;
using UnityEngine;
using XLua;

namespace HiXlua
{
    /// <summary>
    /// 
    /// </summary>
    public class LuaManager : MonoBehaviour
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static LuaManager Instance;

        /// <summary>
        /// Lua环境
        /// </summary>
        public LuaEnv LuaEnv { get; private set; }

        /// <summary>
        /// 全局唯一一个LuaEnv
        /// </summary>
        private static bool isLuaEvnExist = false;

        /// <summary>
        /// 
        /// </summary>
        [CSharpCallLua]
        public delegate void LuaFunction_NoParam();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [CSharpCallLua]
        public delegate void LuaFunction_int(int value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [CSharpCallLua]
        public delegate void LuaFunction_float(float value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [CSharpCallLua]
        public delegate void LuaFunction_string(string value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        [CSharpCallLua]
        public delegate void LuaFunction_int2(int value1, int value2);
        //其他类型扩展
        //...

        /// <summary>
        /// 绑定Update,附带参数deltaTime
        /// </summary>
        private LuaFunction_float luaUpdate;

        /// <summary>
        /// 绑定FixedUpdate,附带参数fixedDeltaTime
        /// </summary>
        private LuaFunction_float luaFixedUpdate;

        /// <summary>
        ///  绑定LaterUpdate,附带参数deltaTime
        /// </summary>
        private LuaFunction_float luaLateUpdate;

        /// <summary>
        /// 缓存lua代码（已解密）
        /// </summary>
        public Dictionary<string, byte[]> luaFiles = new Dictionary<string, byte[]>();

        /// <summary>
        /// 初始化
        /// </summary>
        void Awake()
        {
            if (isLuaEvnExist)
            {
                AssertThat.Fail("Make sure there is only one LuaManager in project");
            }
            else
            {
                isLuaEvnExist = true;
                DontDestroyOnLoad(gameObject);
                Instance = this;
                LuaEnv = new LuaEnv();
                InitLoader();
            }
        }

        /// <summary>
        /// 帧更新
        /// </summary>
        // Update is called once per frame
        void Update()
        {
            AssertThat.IsNotNull(LuaEnv, "Lua env is null");
            LuaEnv.Tick();
            if (luaUpdate != null)
            {
                luaUpdate(Time.deltaTime);
            }
        }

        /// <summary>
        /// 物理帧更新
        /// </summary>
        void FixedUpdate()
        {
            if (luaFixedUpdate != null)
            {
                luaFixedUpdate(Time.fixedDeltaTime);
            }
        }

        /// <summary>
        /// 延迟更新
        /// </summary>
        void LateUpdate()
        {
            if (luaLateUpdate != null)
            {
                luaLateUpdate(Time.deltaTime);
            }
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Destory()
        {
            luaFiles.Clear();
            luaFiles = null;
            luaUpdate = null;
            luaFixedUpdate = null;
            luaLateUpdate = null;
            if (LuaEnv != null)
            {
                LuaEnv.Dispose();
                LuaEnv = null;
            }
        }

        /// <summary>
        /// lua虚拟机加载相关联的lua文件
        /// </summary>
        void InitLoader()
        {
            LuaEnv.AddLoader((ref string luaFileNameFromXLuaPopOut) =>
            {
                return GetLuaFile(luaFileNameFromXLuaPopOut);
            });
        }

        /// <summary>
        /// 绑定lua方法
        /// </summary>
        public void BindLuaFunction()
        {
            luaUpdate = LuaEnv.Global.Get<LuaFunction_float>("Update");
            luaFixedUpdate = LuaEnv.Global.Get<LuaFunction_float>("FixedUpdate");
            luaLateUpdate = LuaEnv.Global.Get<LuaFunction_float>("LateUpdate");
        }

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
        /// 3...其他获取途径，总之传入lua文件名和lua源码
        /// 3.InitLuaFile（）最终也会调用该接口
        /// </summary>
        /// lua文件名和对应的二进制文件,当加载关联lua文件时使用
        /// lua文件一般不会以源文件存在发布的项目中
        /// 可以在调用该接口前执行lua文件解密,然后传入二进制文件
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
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                fileName = fileName.Substring(0, fileName.LastIndexOf("."));//取出后缀
                using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    var bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, (int)fs.Length);
                    fs.Close();
                    InitLuaFile(fileName, bytes);
                }
            }
        }
    }
}