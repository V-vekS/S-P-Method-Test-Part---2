using Fusion;
using Fusion.Sockets;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine; 
using UnityEngine.SceneManagement;
    

public class FusionConnection : MonoBehaviour, INetworkRunnerCallbacks
{
    [Header("Game Arguments")]
    [SerializeField] private GameMode gameMode;
    [SerializeField] private string roomName;

    [SerializeField] public NetworkRunner runner;

    [SerializeField] private NetworkObject playerPrefab;

    [SerializeField] private Transform playerParent;
    [SerializeField] private GameComponentManager gameComponentManager;


    private INetworkSceneManager sceneManager;


    private void Awake()
    {
        if(runner == null)
        {
            if (transform.gameObject.GetComponent<NetworkRunner>())
            {
                runner = transform.gameObject.GetComponent<NetworkRunner>();
            }
            else
            {
                runner = gameObject.AddComponent<NetworkRunner>();
            }
        }
    }

    private async void Start()
    {
        await Connect();
    }

    public async Task Connect()
    {
        //Create the scene manager if it does not exist
        if(sceneManager == null)
        {
            sceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        //Start or join(depends on gamemode) a session with a specific name
        var args = new StartGameArgs()
        {
            GameMode = gameMode,
            SessionName = roomName,
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = sceneManager,
           // CustomPhotonAppSettings = Settings.AppSettings
        };
        await runner.StartGame(args);
    }


                 


    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("On Connected To Server");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.LogWarning("OnConnectFailed");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        Debug.LogWarning("OnDisconnectedFromServer");
    }

   
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("Player " + player.PlayerId + "has joined");

        if(player == runner.LocalPlayer)
        {
            Debug.Log("Another player prefab ha been spawned");
            //From Philip Herlitz ---> Line is sus...
            NetworkObject avatar =
            runner.Spawn(playerPrefab, position: transform.position, rotation: transform.rotation, player, (runner, obj) => { });
            avatar.transform.parent = playerParent;
            gameComponentManager.playerSettings = avatar.GetComponent<NetworkPlayerSettings>();
            if (runner.SessionInfo.PlayerCount > 1)
            {
                gameComponentManager.playerSettings.avatarUrl = "https://models.readyplayer.me/658a9d41eab06131ed28f657.glb";
            }
            else
            {
                gameComponentManager.playerSettings.avatarUrl = "https://models.readyplayer.me/658a9d8afc8bec93d0642a31.glb";
            }
        }
       
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("Player " + player.PlayerId + "has left"); 
    } 

   

    #region unused fusion callbacks
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
      
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
       
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }



    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
      
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
       
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
       
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
       
    }
    #endregion

}

