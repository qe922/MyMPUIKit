# 事件总线使用指南

## 概述

`EventBus` 是一个基于单例模式的事件管理系统，允许方法之间进行解耦通信。第二个方法可以监听第一个方法的调用。

## 核心功能

### 1. 注册监听器

```csharp
// 无参数版本
EventBus.Instance.RegisterListener("事件名称", 触发方法, 监听方法);

// 带参数版本
EventBus.Instance.RegisterListener<int>("事件名称", 触发方法, 监听方法);
```

### 2. 发布事件

```csharp
// 无参数事件
EventBus.Instance.Publish("事件名称");

// 带参数事件
EventBus.Instance.Publish("事件名称", 参数值);
```

### 3. 移除监听器

```csharp
EventBus.Instance.RemoveListener("事件名称", 监听方法);
```

## 使用示例

### 基本用法

```csharp
void TriggerMethod()
{
    Debug.Log("原始方法被调用");
}

void ListenerMethod()
{
    Debug.Log("监听方法收到事件");
}

// 注册监听
EventBus.Instance.RegisterListener("MyEvent", TriggerMethod, ListenerMethod);

// 触发事件（会同时调用原始方法和所有监听方法）
EventBus.Instance.Publish("MyEvent");
```

### 游戏开发示例

```csharp
// 玩家移动时更新UI和播放音效
EventBus.Instance.RegisterListener("PlayerMove", player.Move, ui.UpdatePosition);
EventBus.Instance.RegisterListener("PlayerMove", player.Move, audio.PlayFootstep);

// 玩家攻击时显示特效和播放音效
EventBus.Instance.RegisterListener("PlayerAttack", player.Attack, effects.ShowAttack);
EventBus.Instance.RegisterListener("PlayerAttack", player.Attack, audio.PlaySword);
```

## 优势

1. **解耦**: 方法之间不需要直接引用
2. **可扩展**: 可以轻松添加新的监听器
3. **灵活**: 支持带参数和不带参数的事件
4. **易维护**: 事件逻辑集中管理

## 注意事项

1. 事件名称需要保持一致
2. 带参数的事件需要类型匹配
3. 及时移除不再需要的监听器避免内存泄漏
4. 避免在性能关键路径过度使用事件总线

## 最佳实践

1. 使用有意义的字符串作为事件名称
2. 在适当的生命周期（如OnDestroy）中移除监听器
3. 考虑使用枚举或常量来管理事件名称
4. 对于高频事件，考虑使用对象池或其他优化