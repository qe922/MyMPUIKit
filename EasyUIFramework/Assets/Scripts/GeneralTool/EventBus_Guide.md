# EventBus 使用指南

## 概述

EventBus 是一个基于单例模式的事件总线系统，用于在 Unity 项目中实现组件间的松耦合通信。它提供了注册、发布、移除事件监听器的功能，支持无参数和带参数的事件。

## 基本用法

### 获取 EventBus 实例

```csharp
// 通过单例模式获取 EventBus 实例
EventBus eventBus = EventBus.Instance;
```

## 无参数事件

### 注册监听器

```csharp
void Start()
{
    // 注册无参数事件监听器
    EventBus.Instance.RegisterListener("OnPlayerDeath", HandlePlayerDeath);
}

private void HandlePlayerDeath()
{
    Debug.Log("玩家死亡事件触发");
    // 处理玩家死亡逻辑
}
```

### 发布事件

```csharp
void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Enemy"))
    {
        // 发布无参数事件
        EventBus.Instance.Publish("OnPlayerDeath");
    }
}
```

### 移除监听器

```csharp
void OnDestroy()
{
    // 移除事件监听器
    EventBus.Instance.RemoveListener("OnPlayerDeath", HandlePlayerDeath);
}
```

## 带参数事件

### 定义事件数据类

```csharp
// 自定义事件数据类
public class DamageEventData
{
    public int DamageAmount { get; set; }
    public Vector3 HitPosition { get; set; }
    public GameObject Attacker { get; set; }
}
```

### 注册带参数监听器

```csharp
void Start()
{
    // 注册带参数事件监听器
    EventBus.Instance.RegisterListener<DamageEventData>("OnDamageTaken", HandleDamageTaken);
}

private void HandleDamageTaken(DamageEventData damageData)
{
    Debug.Log($"受到 {damageData.DamageAmount} 点伤害，来自 {damageData.Attacker.name}");
    // 处理伤害逻辑
}
```

### 发布带参数事件

```csharp
void ApplyDamage(int damage, Vector3 hitPosition, GameObject attacker)
{
    var damageData = new DamageEventData
    {
        DamageAmount = damage,
        HitPosition = hitPosition,
        Attacker = attacker
    };
    
    // 发布带参数事件
    EventBus.Instance.Publish("OnDamageTaken", damageData);
}
```

### 移除带参数监听器

```csharp
void OnDestroy()
{
    // 移除带参数事件监听器
    EventBus.Instance.RemoveListener<DamageEventData>("OnDamageTaken", HandleDamageTaken);
}
```

## 高级用法

### 检查事件监听状态

```csharp
// 检查事件是否有监听器
bool hasListeners = EventBus.Instance.HasListeners("OnPlayerDeath");

// 获取事件监听器数量
int listenerCount = EventBus.Instance.GetListenerCount("OnPlayerDeath");
```

### 清除所有监听器

```csharp
// 在场景切换或游戏结束时清除所有监听器
void OnApplicationQuit()
{
    EventBus.Instance.ClearAllListeners();
}
```

## 使用场景示例

### 场景1：UI 更新

```csharp
// 在 UI 控制器中注册监听器
void Start()
{
    EventBus.Instance.RegisterListener<int>("OnScoreChanged", UpdateScoreUI);
}

private void UpdateScoreUI(int newScore)
{
    scoreText.text = $"分数: {newScore}";
}

// 在游戏逻辑中发布事件
void AddScore(int points)
{
    currentScore += points;
    EventBus.Instance.Publish("OnScoreChanged", currentScore);
}
```

### 场景2：成就系统

```csharp
// 成就系统监听多个事件
void Start()
{
    EventBus.Instance.RegisterListener("OnFirstBlood", UnlockFirstBloodAchievement);
    EventBus.Instance.RegisterListener<int>("OnKillCountChanged", CheckKillAchievements);
}

private void UnlockFirstBloodAchievement()
{
    // 解锁首杀成就
}

private void CheckKillAchievements(int killCount)
{
    if (killCount >= 100)
    {
        // 解锁百人斩成就
    }
}
```

## 最佳实践

1. **事件命名规范**：使用清晰、描述性的事件名称，如 "OnPlayerDeath"、"OnScoreChanged" 等
2. **及时移除监听器**：在 MonoBehaviour 销毁时移除相关的事件监听器，避免内存泄漏
3. **错误处理**：EventBus 内置了错误处理，但仍建议在监听器方法中添加 try-catch
4. **性能考虑**：避免在频繁调用的方法中发布事件
5. **调试**：启用 EventBus 的调试日志可以帮助跟踪事件流

## 注意事项

- EventBus 使用字符串作为事件标识符，确保事件名称的一致性
- 带参数事件需要类型匹配，否则会收到类型不匹配警告
- 在场景切换时建议清除所有监听器，避免跨场景的事件引用
- EventBus 是线程安全的，但 Unity 的主线程限制仍然适用

## 常见问题

**Q: 如何避免事件名称冲突？**
A: 使用命名空间前缀，如 "Gameplay.OnPlayerDeath"、"UI.OnButtonClick"

**Q: 多个脚本监听同一个事件，执行顺序如何？**
A: 监听器按照注册顺序执行

**Q: 事件总线会影响性能吗？**
A: 对于大多数应用场景，性能影响可以忽略不计。避免在 Update 中频繁发布事件

## 扩展建议

如果需要更复杂的事件系统，可以考虑：
- 添加事件优先级
- 支持一次性监听器
- 添加事件过滤功能
- 支持异步事件处理