using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : BaseSingleton<EventBus>
{
    private Dictionary<string, List<Action<object>>> eventListeners = new Dictionary<string, List<Action<object>>>();
    private Dictionary<string, List<Action>> eventListenersNoParam = new Dictionary<string, List<Action>>();

    /// <summary>
    /// 注册事件监听器，第二个方法监听第一个方法触发的事件
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="triggerAction">触发事件的方法</param>
    /// <param name="listenerAction">监听事件的方法</param>
    public void RegisterListener(string eventName, Action triggerAction, Action listenerAction)
    {
        if (!eventListenersNoParam.ContainsKey(eventName))
        {
            eventListenersNoParam[eventName] = new List<Action>();
        }

        // 将监听器添加到事件
        eventListenersNoParam[eventName].Add(listenerAction);

        // 修改触发方法，使其在调用时发布事件
        Action wrappedAction = () =>
        {
            triggerAction?.Invoke();
            Publish(eventName);
        };

        // 返回包装后的方法（如果需要的话，这里只是演示，实际使用时需要替换原来的触发方法）
    }

    /// <summary>
    /// 注册事件监听器（带参数版本）
    /// </summary>
    public void RegisterListener<T>(string eventName, Action<T> triggerAction, Action<T> listenerAction)
    {
        if (!eventListeners.ContainsKey(eventName))
        {
            eventListeners[eventName] = new List<Action<object>>();
        }

        // 将监听器添加到事件
        eventListeners[eventName].Add((obj) => listenerAction?.Invoke((T)obj));

        // 修改触发方法，使其在调用时发布事件
        Action<T> wrappedAction = (param) =>
        {
            triggerAction?.Invoke(param);
            Publish(eventName, param);
        };

        // 返回包装后的方法（如果需要的话）
    }

    /// <summary>
    /// 发布事件（无参数）
    /// </summary>
    public void Publish(string eventName)
    {
        if (eventListenersNoParam.ContainsKey(eventName))
        {
            foreach (var listener in eventListenersNoParam[eventName])
            {
                listener?.Invoke();
            }
        }
    }

    /// <summary>
    /// 发布事件（带参数）
    /// </summary>
    public void Publish(string eventName, object data)
    {
        if (eventListeners.ContainsKey(eventName))
        {
            foreach (var listener in eventListeners[eventName])
            {
                listener?.Invoke(data);
            }
        }
    }

    /// <summary>
    /// 移除事件监听器
    /// </summary>
    public void RemoveListener(string eventName, Action listenerAction)
    {
        if (eventListenersNoParam.ContainsKey(eventName))
        {
            eventListenersNoParam[eventName].Remove(listenerAction);
        }
    }

    /// <summary>
    /// 移除事件监听器（带参数版本）
    /// </summary>
    public void RemoveListener<T>(string eventName, Action<T> listenerAction)
    {
        if (eventListeners.ContainsKey(eventName))
        {
            // 需要找到对应的包装方法并移除
            // 这里简化处理，实际实现可能需要更复杂的逻辑
        }
    }

    /// <summary>
    /// 清除所有事件监听器
    /// </summary>
    public void ClearAllListeners()
    {
        eventListeners.Clear();
        eventListenersNoParam.Clear();
    }
}