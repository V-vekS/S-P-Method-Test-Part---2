using UnityEngine;
using Fusion;
using PlayerMovementScript;

[CreateAssetMenu(fileName ="GameComponentManager", menuName = "Scriptable Objects/GameComponentManager/Test", order =1)]
public class GameComponentManager : ScriptableObject
{
    public NetworkRunner runner;
    public NetworkPlayerSettings playerSettings;
    public ReadyPlayerMeAvatarLoader avatarLoader;
    public Transform playerParent;
    public PlayerMovementPH playerMovement;
}
 