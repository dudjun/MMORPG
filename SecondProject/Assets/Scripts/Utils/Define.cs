using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public enum WorldObject
    {
        Unknown,
        Player,
        Monster,
        Item
    }

    public enum Layer 
    {
        Monster = 8,
        Ground = 9,
        Block =  10,
    }

    public enum MouseEvent 
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }

    public enum Scene
    {
        Unknown,
        Login,
        Game,
    }

    public enum UIEvent 
    { 
        Click,
        Drag,
    }

    public enum Character
    {
        Unknown,
        Warrior,
        Wizard,
    }

    public enum State
    {
        Die,
        Moving,
        Idle,
        Skill,
        ICE,
    }
}
