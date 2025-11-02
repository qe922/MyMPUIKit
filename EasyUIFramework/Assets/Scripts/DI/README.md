# 简单DI容器使用指南

## 核心功能

- **单例模式**: 所有服务默认都是单例
- **构造函数注入**: 自动解析依赖
- **简单易用**: 只有3个主要方法

## 基本用法

### 1. 定义接口和实现
```csharp
public interface IService 
{
    void DoSomething();
}

public class MyService : IService
{
    public void DoSomething() { }
}
```

### 2. 注册服务
```csharp
var di = new SimpleDI();
di.Register<IService, MyService>();
```

### 3. 使用服务
```csharp
var service = di.Resolve<IService>();
service.DoSomething();
```

## 构造函数注入

```csharp
public class App
{
    private IService _service;
    
    public App(IService service)  // 自动注入
    {
        _service = service;
    }
}

// 使用
var app = di.Resolve<App>();  // 自动创建App并注入IService
```

## 注册实例

```csharp
var instance = new MyService();
di.RegisterInstance<IService>(instance);
```

## 检查注册

```csharp
bool isRegistered = di.IsRegistered<IService>();
```

将 `SimpleExample.cs` 挂载到Unity场景即可运行演示。