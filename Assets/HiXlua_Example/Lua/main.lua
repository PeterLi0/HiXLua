require("Common")

function Start()
    print("Lua start")
end

function Update(time)
    --print("update"..time)
end

function FixedUpdate(time)
    --print("fixedUpdate"..time)
end

function LateUpdate(time)
    --print("LateUpdate"..time)
end

function Destory()
    --print("Destory")
end





base_type=class()		-- 定义一个基类 base_type

function base_type:ctor(x)	-- 定义 base_type 的构造函数
	print("base_type ctor")
	self.x=x
end
 
function base_type:print_x()	-- 定义一个成员函数 base_type:print_x
	print(self.x)
end
 
function base_type:hello()	-- 定义另一个成员函数 base_type:hello
	print("hello base_type")
end


test=class(base_type)	-- 定义一个类 test 继承于 base_type
 
function test:ctor()	-- 定义 test 的构造函数
    print("test ctor")
end
 
function test:hello()	-- 重载 base_type:hello 为 test:hello
	print("hello test")
end
--[[
test2 = class(test)
function test2:ctor()
print("test2")
end]]


a=test.new(1)	-- 输出两行，base_type ctor 和 test ctor 。这个对象被正确的构造了。
a:print_x()	-- 输出 1 ，这个是基类 base_type 中的成员函数。
a:hello()	-- 输出 hello test ，这个函数被重载了。

print("----------")

