using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionConnection2 : MonoBehaviour, INetworkRunnerCallbacks
{
    public bool connectOnAwake;
    [HideInInspector]public NetworkRunner runner;

    [SerializeField] NetworkObject playerPrefab;

    private void Awake()
    {
        if(connectOnAwake == true)
        {
            ConnectToRunner();
        } 
    }

    public async void ConnectToRunner()
    {
        if (runner == null)
        {
            runner = gameObject.GetComponent<NetworkRunner>();
        }
        await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = "test",
            PlayerCount = 5,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }






    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log(" OnConnectedToServer");
        NetworkObject playerObject =  runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity);
        runner.SetPlayerObject(runner.LocalPlayer, playerObject);
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.Log("OnConnectFailed");
    }

  

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        Debug.LogWarning("DisconnectedFromServer");
    }

    

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("OnPlayerJoined");
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("Player" + player.PlayerId + " has left");
    }

    #region unused callbacks

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

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {

    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
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

    #endregion
}
