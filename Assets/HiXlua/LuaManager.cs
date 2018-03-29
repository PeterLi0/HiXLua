using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace HiXlua
{
    public class LuaManager : MonoBehaviour
    {
        public LuaEnv LuaEnv { get; private set; }
        public LuaManager Instance;

        private static bool isLuaEvnExist = false;
        void Awake()
        {
            Instance = this;
        }

        // Use this for initialization
        void Start()
        {
            LuaEnv = new LuaEnv();
            if (isLuaEvnExist)
            {
                Destroy(gameObject);
            }
            else
            {
                isLuaEvnExist = true;
                DontDestroyOnLoad(gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {
            LuaEnv.Tick();
        }
        Dictionary<string, byte[]> luaFileNameAndBytesDic = new Dictionary<string, byte[]>();
        public void AddLuaFileBytes(string luaFileName, byte[] bytes)
        {
            if (luaFileNameAndBytesDic.ContainsKey(luaFileName))
                Debug.LogError("already contain this lua file");
            luaFileNameAndBytesDic.Add(luaFileName, bytes);

            LuaEnv.AddLoader((ref string luaFileName_FromXLua) =>
            {
                if (!luaFileNameAndBytesDic.ContainsKey(luaFileName_FromXLua))
                    Debug.LogError("you havent add this lua file to Dic: " + luaFileName_FromXLua);
                return luaFileNameAndBytesDic[luaFileName_FromXLua];
            });
        }
    }
}