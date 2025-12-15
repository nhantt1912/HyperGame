using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    private static Dictionary<Type, Delegate> events = new();


    public static void AddListener<T>(Action<T> listener)
    {
        var type = typeof(T);

        if (events.TryGetValue(type, out var del))
        {
            events[type] = Delegate.Combine(del, listener);
        }
        else
        {
            events[type] = listener;
        }
        
    }

    public static void RemoveListener<T>(Action<T> listener)
    {
        var type = typeof(T);

        if (events.TryGetValue(type, out var del))
        {
            var currentDel = Delegate.Remove(del, listener);
            if (currentDel == null)
            {
                events.Remove(type);
            }
            else
            {
                events[type] = currentDel;
            }
            
        }
    }

    public static void Invoke<T>(T args)
    {
        var type = typeof(T);
        if (events.TryGetValue(type, out var del))
        {
            ((Action<T>) del).Invoke(args);
        }
    }
    
    
    
}
