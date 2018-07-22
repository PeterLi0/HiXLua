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
    public class LuaManager : MonoBehaviour
    {
        public static LuaManager Instance;

        public LuaEnv LuaEnv { get; private set; }

        /// <summary>
        /// 全局唯一一个LuaEnv
        /// </summary>
        private static bool isLuaEvnExist = false;

        /// <summary>
        /// 
        /// </summary>
        [CSharpCallLua]
        private delegate void LuaFunction_NoParam();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [CSharpCallLua]
        private delegate void LuaFunction_int(int value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [CSharpCallLua]
        private delegate void LuaFunction_float(float value);

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
                InitDelegate();
                InitLoader();
            }
        }

        // Update is called once per frame
        void Update()
        {
            AssertThat.IsNotNull(LuaEnv, "Lua env is null");
            LuaEnv.Tick();
            UpdateLuaFunction();
        }

        /// <summary>
        /// lua文件名和对应的二进制文件
        /// 
        /// </summary>
        private readonly Dictionary<string, byte[]> luaFileNameAndBytes = new Dictionary<string, byte[]>();
        public void AddLuaFileBytes(string luaFileName, byte[] bytes)
        {
            if (luaFileNameAndBytes.ContainsKey(luaFileName))
                Debug.LogError("already contain this lua file");
            luaFileNameAndBytes.Add(luaFileName, bytes);
        }


        void InitLoader()
        {
            LuaEnv.AddLoader((ref string luaFileNameFromXLua) =>
            {
                if (!luaFileNameAndBytes.ContainsKey(luaFileNameFromXLua))
                    Debug.LogError("you havent add this lua file to Dic: " + luaFileNameFromXLua);
                return luaFileNameAndBytes[luaFileNameFromXLua];
            });
        }

        /// <summary>
        /// 初始化绑定lua方法
        /// </summary>
        private void InitDelegate()
        {
            luaUpdate = LuaEnv.Global.Get<LuaFunction_float>("Update");
            luaFixedUpdate = LuaEnv.Global.Get<LuaFunction_float>("FixedUpdate");
            luaLaterUpdate = LuaEnv.Global.Get<LuaFunction_float>("LaterUpdate");
        }

        /// <summary>
        /// 更新lua帧
        /// </summary>
        private void UpdateLuaFunction()
        {
            if (luaUpdate != null)
            {
                luaUpdate(Time.deltaTime);
            }
            if (luaFixedUpdate != null)
            {
                luaFixedUpdate(Time.fixedDeltaTime);
            }
            if (luaLaterUpdate != null)
            {
                luaLaterUpdate(Time.deltaTime);
            }
        }
    }
}