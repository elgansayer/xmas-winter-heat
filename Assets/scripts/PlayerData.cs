using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using IO.Ably;
using IO.Ably.Realtime;
using UnityEngine.UI;
using Newtonsoft.Json;

[Serializable]
public struct IPlayerData
{
    public IPlayer player;
    public int a;
    public int b;
    public int toggle;
    public long time;
}

[Serializable]
public struct IPlayer
{
    public string id;
    public string name;
    public string dept;
    public ITeam team;
}

[Serializable]
public struct ITeam
{
    public string id;
    public string name;
    public string color;
}