using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace HiXlua
{
    public class LuaManager : MonoBehaviour
    {
        public LuaEnv LuaEnv { get; private set; }
        public static LuaManager Instance;

        private static bool isLuaEvnExist = false;
        void Awake()
        {
            if (isLuaEvnExist)
            {
                Destroy(gameObject);
            }
            else
            {
                isLuaEvnExist = true;
                DontDestroyOnLoad(gameObject);
                Instance = this;
                LuaEnv = new LuaEnv();
            }
        }

        // Update is called once per frame
        void Update()
        {
            LuaEnv.Tick();
        }
        private readonly Dictionary<string, byte[]> _luaFileNameAndBytesDic = new Dictionary<string, byte[]>();
        public void AddLuaFileBytes(string luaFileName, byte[] bytes)
        {
            if (_luaFileNameAndBytesDic.ContainsKey(luaFileName))
                Debug.LogError("already contain this lua file");
            _luaFileNameAndBytesDic.Add(luaFileName, bytes);

            LuaEnv.AddLoader((ref string luaFileNameFromXLua) =>
            {
                if (!_luaFileNameAndBytesDic.ContainsKey(luaFileNameFromXLua))
                    Debug.LogError("you havent add this lua file to Dic: " + luaFileNameFromXLua);
                return _luaFileNameAndBytesDic[luaFileNameFromXLua];
            });
        }
    }
}