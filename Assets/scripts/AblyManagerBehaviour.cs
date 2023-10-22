using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using IO.Ably;
using IO.Ably.Realtime;
using UnityEngine.UI;
using Newtonsoft.Json;

public class AblyManagerBehaviour : MonoBehaviour
{
    private AblyRealtime _ably;
    private ClientOptions _clientOptions;

    private static string _apiKey = "U0ALVg.DLhdIQ:uNrgKHf61SiaJH5XFjHV5bMYCugNvi8vzfZyMNJNp20";

    private PlayerHandler _playerHandler;

    void Start()
    {
        InitializeAbly();
        // Game the player handler game object
        this._playerHandler = GameObject.Find("PlayerHandler").GetComponent<PlayerHandler>();
        // playerHandlerGameObject.
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

        _ably.Channels.Get("gameAction").Subscribe("clicks", (msg) =>
        {                 
            string jsonString = msg.Data.ToString();
            // Convert json to IPlayerData
            // IPlayerData playerData = JsonUtility.FromJson<IPlayerData>(jsonString);
            IPlayerData playerData = JsonConvert.DeserializeObject<IPlayerData>(jsonString);
                
            this._playerHandler.Enqueue(playerData);

            Debug.Log($"gameAction clicks received");
            // Debug.Log(playerData);            
            // Debug.Log($" Received gameAction clicks <b>{msg.Data}</b> from channel <b>GameAction</b>");
        });

    }
}


