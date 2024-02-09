using Fusion;
using UnityEngine;

public class NetworkPlayerSettings : NetworkBehaviour
{
    [Networked(OnChanged = nameof(AvatarUrlChanged))]
    public NetworkString<_128> avatarUrl { get; set; }

    [Networked(OnChanged = nameof(PlayerNameChanged))]

    public NetworkString <_16> playerName { get; set; }

    [SerializeField] public ReadyPlayerMeAvatarLoader avatarLoader;
    [SerializeField] private GameComponentManager gameComponentManager;

    /*private void Awake()
    {
        if (gameComponentManager != null && gameComponentManager.runner != null && gameComponentManager.runner.LocalPlayer != null)
        {
            gameComponentManager.playerSettings = this;
        }
        else
        {
            Debug.LogError("gameComponentManager or its properties are null. Make sure they are properly initialized.");
        }
    }*/

    private void Awake()
    {
        if (gameComponentManager.runner.LocalPlayer)
        {
            gameComponentManager.playerSettings = this;
        }
       
    }

    public static void AvatarUrlChanged(Changed<NetworkPlayerSettings> change)
    {
        if (change.Behaviour.avatarLoader != null)
        {
            change.Behaviour.avatarLoader.LoadAvatar(change.Behaviour.avatarUrl.ToString());
        }
        else
        {
            Debug.LogError("avatarLoader is null. Make sure it is properly initialized.");
        }
    }

    public static void PlayerNameChanged(Changed<NetworkPlayerSettings> change)
    {
        
    }
}
