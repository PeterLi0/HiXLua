require("Joystick")

function LuaStart()
    print("Lua start")
end

LuaStart()


function Update(time)
    print("update"..time)
end

function FixedUpdate(time)
    --print("fixedUpdate"..time)
end

function LateUpdate(time)
    --print("LateUpdate"..time)
end

function Destory()
    print("Destory")
end