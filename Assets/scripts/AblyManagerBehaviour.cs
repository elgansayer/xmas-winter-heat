using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
﻿using System.Linq;
using System.Threading;
using IO.Ably;
using IO.Ably.Realtime;
using UnityEngine.UI;

public class AblyManagerBehaviour : MonoBehaviour
{
    private AblyRealtime _ably;
    private ClientOptions _clientOptions;

    // It's recommended to use other forms of authentication. E.g. JWT, Token Auth 
    // This is to avoid exposing root api key to a client
    private static string _apiKey = "U0ALVg.DLhdIQ:uNrgKHf61SiaJH5XFjHV5bMYCugNvi8vzfZyMNJNp20";

    void Start()
    {
        InitializeAbly();
    }

    private void InitializeAbly()
    {
        _clientOptions = new ClientOptions
        {
            Key = _apiKey,
            // this will disable the library trying to subscribe to network state notifications
            AutomaticNetworkStateMonitoring = false,
            AutoConnect = true,
            // this will make sure to post callbacks on UnitySynchronization Context Main Thread
            CustomContext = SynchronizationContext.Current
        };

        _ably = new AblyRealtime(_clientOptions);
        _ably.Connection.On(args =>
        {
            Debug.Log($"Connection State is <b>{args.Current}</b>");

            switch (args.Current)
            {
                case ConnectionState.Initialized:
                    // connectionStatusBtnImage.color = Color.white;
                    break;
                case ConnectionState.Connecting:
                    // connectionStatusBtnImage.color = Color.gray;
                    break;
                case ConnectionState.Connected:
                    // connectionStatusBtnImage.color = Color.green;
                    this.subscribeToChannels();
                    break;
                case ConnectionState.Disconnected:
                    // connectionStatusBtnImage.color = Color.yellow;
                    break;
                case ConnectionState.Closing:
                    // connectionStatusBtnImage.color = Color.yellow;
                    break;
                case ConnectionState.Closed:
                case ConnectionState.Failed:
                case ConnectionState.Suspended:
                    // connectionStatusBtnImage.color = Color.red;
                    break;
                    // default:
                    // throw new ArgumentOutOfRangeException();
            }
        });
    }

    void subscribeToChannels()
    {
        Debug.Log($"Subscribing to channels");

        var channelNames = string.Join(", ", _ably.Channels.Select(channel => channel.Name));
        Debug.Log($"Channel Names - <b>{channelNames}</b>");

        // gameAction , clicks
        _ably.Channels.Get("gameAction").Presence.Subscribe(message =>
        {
            Debug.Log($" Received gameAction <b>{message}</b> from channel <b>GameAction</b>");
        });

        _ably.Channels.Get("gameAction").Subscribe("clicks", (msg) =>
        {
            Debug.Log($" Received gameAction <b>{msg}</b> from channel <b>GameAction</b>");
        });

        // playerPresence, present
        _ably.Channels.Get("playerPresence").Presence.Subscribe(message =>
        {
            Debug.Log($" Received playerPresence <b>{message}</b> from channel <b>GameAction</b>");
        });

        _ably.Channels.Get("teamInfo").Presence.Subscribe(message =>
        {
            Debug.Log($" Received teamInfo <b>{message}</b> from channel <b>GameAction</b>");
        });

    }
}
