# UI框架DI优化使用指南

## 概述

这个DI容器专门为您的UI框架设计，提供了简单但功能完整的依赖注入支持，让UI开发更加模块化和可测试。

## 核心组件

### 1. DI容器 (`UIServiceContainer`)
- 单例模式，全局访问
- 自动依赖注入
- 支持服务注册和解析

### 2. 服务接口
- `IUIService` - UI管理服务
- `IAudioService` - 音频服务  
- `ILocalizationService` - 本地化服务
- `IConfigService` - 配置服务

### 3. 服务实现
- `UIService` - UI服务实现
- `AudioService` - 音频服务实现
- `LocalizationService` - 本地化服务实现
- `ConfigService` - 配置服务实现

### 4. BasePanelWithDI
- 继承自BasePanel
- 内置DI服务支持
- 提供便捷的服务访问方法

## 使用方法

### 1. 初始化服务

```csharp
// 在游戏启动时调用
ServiceInitializer.InitializeServices();
```

### 2. 使用BasePanelWithDI

```csharp
public class MyPanel : BasePanelWithDI
{
    public MyPanel() : base("MyPanel") { }
    
    protected override void ViewInit()
    {
        base.ViewInit();
        
        // 使用DI服务
        ShowMessage("面板初始化");
        PlayUISound("panel_open");
        
        // 使用本地化
        var text = GetLocalizedText("welcome");
        
        // 使用配置
        var volume = GetConfig<float>("volume");
    }
    
    public override void DIInit()
    {
        // 使用DI服务进行额外初始化
        SetConfig("ui_theme", "dark");
    }
}
```

### 3. 手动获取服务

```csharp
var uiService = UIServiceContainer.Instance.Resolve<IUIService>();
var audioService = UIServiceContainer.Instance.Resolve<IAudioService>();
```

### 4. 替换服务实现

```csharp
// 测试环境使用Mock服务
UIServiceContainer.Instance.RegisterInstance<IAudioService>(new TestAudioService());

// 不同平台使用不同实现
UIServiceContainer.Instance.Register<IAudioService, WebGLAudioService>();
```

## 优势

### 1. 解耦设计
- UI逻辑与具体实现分离
- 易于替换服务实现
- 支持模块化开发

### 2. 可测试性
```csharp
// 单元测试中使用Mock服务
public class TestPanel : BasePanelWithDI
{
    public TestPanel() : base("TestPanel") { }
    
    public void TestMethod()
    {
        // 可以注入Mock服务进行测试
        ShowMessage("测试消息");
    }
}
```

### 3. 灵活性
- 开发期：使用基础实现
- 测试期：使用Mock服务  
- 发布期：使用完整实现

### 4. 维护性
- 服务集中管理
- 依赖关系清晰
- 易于扩展新功能

## 示例场景

将 `DIExampleController` 脚本挂载到场景中的GameObject，运行即可看到完整的DI功能演示。

## 扩展方法

### 添加新服务

1. 定义接口：
```csharp
public interface INewService
{
    void NewMethod();
}
```

2. 实现服务：
```csharp
public class NewService : INewService
{
    public void NewMethod()
    {
        // 实现逻辑
    }
}
```

3. 注册服务：
```csharp
UIServiceContainer.Instance.Register<INewService, NewService>();
```

4. 在BasePanelWithDI中添加便捷方法：
```csharp
protected void NewMethod()
{
    UIServiceContainer.Instance.Resolve<INewService>().NewMethod();
}
```

这个DI优化让您的UI框架更加现代化和可维护！