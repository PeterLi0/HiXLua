/***************************************************************
 * Description: Lua管理类
 * 
 * Author: hiramtan@live.com
***************************************************************/

using System.Collections.Generic;
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
        //todo 其他类型自己扩展

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
        private LuaFunction_float luaLaterUpdate;

        /// <summary>
        /// Lua文件名和对应的二进制
        /// </summary>
        private readonly Dictionary<string, byte[]> luaFileNameAndBytes = new Dictionary<string, byte[]>();

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
        void LaterUpdate()
        {
            if (luaLaterUpdate != null)
            {
                luaLaterUpdate(Time.deltaTime);
            }
        }

        /// <summary>
        /// lua文件名和对应的二进制文件,当加载关联lua文件时使用
        /// lua文件一般不会以源文件存在发布的项目中
        /// 可以在调用该接口前执行lua文件解密,然后传入二进制文件
        /// </summary>

        public void AddLuaFileBytes(string luaFileName, byte[] bytes)
        {
            if (luaFileNameAndBytes.ContainsKey(luaFileName))
                AssertThat.Fail("already contain this lua file");
            luaFileNameAndBytes.Add(luaFileName, bytes);
        }

        /// <summary>
        /// lua虚拟机加载相关联的lua文件
        /// </summary>
        void InitLoader()
        {
            LuaEnv.AddLoader((ref string luaFileNameFromXLuaPopOut) =>
            {
                if (!luaFileNameAndBytes.ContainsKey(luaFileNameFromXLuaPopOut))
                    AssertThat.Fail("you havent add this lua file to Dic: " + luaFileNameFromXLuaPopOut);
                return luaFileNameAndBytes[luaFileNameFromXLuaPopOut];
            });
        }

        /// <summary>
        /// 绑定lua方法
        /// </summary>
        public void BindLuaFunction()
        {
            luaUpdate = LuaEnv.Global.Get<LuaFunction_float>("Update");
            luaFixedUpdate = LuaEnv.Global.Get<LuaFunction_float>("FixedUpdate");
            luaLaterUpdate = LuaEnv.Global.Get<LuaFunction_float>("LaterUpdate");
        }
    }
}