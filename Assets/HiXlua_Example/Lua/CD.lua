
local util = require("xlua.util")
local runnerGo = CS.UnityEngine.GameObject("Coroutine_Runner")
CS.UnityEngine.Object.DontDestroyOnLoad(runnerGo)
local runner = runnerGo:AddComponent(typeof(CS.Coroutine_Runner))

local function async_yield_return(to_yield,cb)
    runner:YieldAndCallback(to_yield,cb)
end


local yield_return = util.async_to_sync(async_yield_return)


local image

local button

image = CS.UnityEngine.GameObject.Find("Image").gameObject:GetComponent(typeof(CS.UnityEngine.UI.Image))
image.type = CS.UnityEngine.UI.Image.Type.Filled
image.fillMethod = CS.UnityEngine.UI.Image.FillMethod.Radial360

button = CS.UnityEngine.GameObject.Find("Button").gameObject:GetComponent(typeof(CS.UnityEngine.UI.Button))


local isCDing = false

function CD()
    if(isCDing==false)then
        isCDing = true
        print("cd start")

        local co = coroutine.create(function()
    
        while(image.fillAmount>0)
        do
            image.fillAmount=image.fillAmount-0.01
         yield_return(CS.UnityEngine.WaitForSeconds(0.01))
        end
        print("cs finish")
        isCDing = false
        image.fillAmount=1   
        end)    
    coroutine.resume(co)
    end
end
button.onClick:AddListener(CD)