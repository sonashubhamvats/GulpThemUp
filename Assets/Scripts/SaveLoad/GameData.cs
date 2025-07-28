using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class GameData
{
    private int exppoint,coin_count,current_max_level,input_type;

    private float joySens,touchSens,volume;
    private bool[] selected_player,bought_player,selected_cannon,bought_cannon,congrats;

    private int shieldTime,gunType,rank,coolDown;

    private bool tutorialDone;
    private int[] star_count;

    public bool _tutorialDone
    {
        get
        {
            return tutorialDone;
        }
        set
        {
            tutorialDone=value;
        }
    }

    public float _volume
    {
        get
        {
            return volume;
        }
        set
        {
            volume=value;
        }
    }

    public int _input_type
    {
        get
        {
            return input_type;
        }
        set
        {
            input_type=value;
        }
    }
    public float _joySens
    {
        get
        {
            return joySens;
        }
        set
        {
            joySens=value;
        }
    }
    public float _touchSens
    {
        get
        {
            return touchSens;
        }
        set
        {
            touchSens=value;
        }
    }



    public int _current_max_level
    {
        get
        {
            return current_max_level;
        }
        set
        {
            current_max_level=value;
        }
    }
    public int[] _star_count
    {
        get
        {
            return star_count;
        }
        set
        {
            star_count=value;
        }
    }

    public bool[] _congrats
    {
        get
        {
            return congrats;
        }
        set
        {
            congrats=value;
        }
    }

    public bool[] _boughtPlayer
    {
        get
        {
            return bought_player;
        }
        set
        {
            bought_player=value;
        }
    }
    public bool[] _selected_cannon
    {
        get
        {
            return selected_cannon;
        }
        set
        {
            selected_cannon=value;
        }
    }
    public bool[] _bought_cannon
    {
        get
        {
            return bought_cannon;
        }
        set
        {
            bought_cannon=value;
        }
    }

    public bool[] _selected_player
    {
        get
        {
            return selected_player;
        }
        set
        {
            selected_player=value;
        }
    }

    public int _shielTime
    {
        get
        {
            return shieldTime;
        }
        set
        {
            shieldTime=value;
        }

    }
    public int _guntype
    {
        get
        {
            return gunType;
        }
        set
        {
            gunType=value;
        }

    }
    public int _rank
    {
        get
        {
            return rank;
        }
        set
        {
            rank=value;
        }

    }
    public int _cooldown
    {
        get
        {
            return coolDown;
        }
        set
        {
            coolDown=value;
        }

    }


    public int _exppoint
    {
        get
        {
            return exppoint;
        }
        set
        {
            exppoint=value;
        }

    }
    public int _coin_count
    {
        get
        {
            return coin_count;
        }
        set
        {
            coin_count=value;
        }
    }
}
