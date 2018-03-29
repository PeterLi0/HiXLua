print("score.lua")
local obj=CS.UnityEngine.Resources.Load("Score")
local go = CS.UnityEngine.Object.Instantiate(obj)
go.transform:SetParent(uiRoot.transform,false);