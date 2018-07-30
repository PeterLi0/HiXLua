local image

image = CS.UnityEngine.GameObject.Find("Image").gameObject:GetComponent(typeof(CS.UnityEngine.UI.Image))
print(image.name)

image.type = CS.UnityEngine.UI.Image.Type.Filled
image.fillMethod = CS.UnityEngine.UI.Image.FillMethod.Radial360


local util = require("xlua.util")
local runnerGo = CS.UnityEngine.GameObject("Coroutine_Runner")
CS.UnityEngine.Object.DontDestroyOnLoad(runnerGo)
local runner = runnerGo:AddComponent(typeof(CS.Coroutine_Runner))

local function async_yield_return(to_yield,cb)
    runner:YieldAndCallback(to_yield,cb)
end

local yield_return = util.async_to_sync(async_yield_return)

print("cd start")

local co = coroutine.create(function()

while(image.fillAmount>0)
do
    image.fillAmount=image.fillAmount-0.01
    yield_return(CS.UnityEngine.WaitForSeconds(0.01))
end


--[[
print("start")
local s = os.time()
yield_return(CS.UnityEngine.WaitForSeconds(3))
print("wait")
]]


end)

coroutine.resume(co)

print("cs finish")
