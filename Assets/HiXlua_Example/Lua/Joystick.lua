local joystickGo
local joystickRadius
local contentRectTransform
local scroll



function Start()
print("Joystick start")

local canvasGo = CS.UnityEngine.GameObject.Find("Canvas")
joystickGo = CS.UnityEngine.MonoBehaviour.Instantiate(CS.UnityEngine.Resources.Load("Joystick"))
joystickGo.transform:SetParent(canvasGo.transform)
local joystickRectTransform = joystickGo:GetComponent(typeof(CS.UnityEngine.RectTransform))
joystickRectTransform.anchoredPosition = CS.UnityEngine.Vector2.zero
joystickRadius = joystickRectTransform.sizeDelta.x*0.5
contentRectTransform = joystickGo.transform:Find("Content").gameObject:GetComponent(typeof(CS.UnityEngine.RectTransform))

scroll = joystickGo:GetComponent(typeof(CS.UnityEngine.UI.ScrollRect))
scroll.onValueChanged:AddListener(OnDrag)
end


function OnDrag(value)
local contentPos = contentRectTransform.anchoredPosition
    if(contentPos.magnitude>joystickRadius)then
        local newPos = contentPos.normalized*joystickRadius
        contentRectTransform.anchoredPosition = newPos
    end
end

Start()
