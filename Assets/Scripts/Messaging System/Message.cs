using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Messaging
{
    public enum Message {
        ItemPickedUp
        
    }

    private static Dictionary<Message, List<Action>> _messages = new();



    public static void Publish(Message message)
    {
        if (_messages.TryGetValue(message, out var actions))
            foreach (var action in actions)
                action();
    }

    public static void Subscribe(Message message, Action action)
    {
        if (_messages.TryGetValue(message, out var actions))
        
            actions.Add(action);
        else
        
            _messages.Add(message,new List<Action> {action});
        
    }

    public static void Unsubscribe(Message message, Action action)
    {
        if (_messages.TryGetValue(message, out var actions))
            actions.Remove(action); 
    }


}

    

