using UnityEngine;
using Fusion;
using ReadyPlayerMe;

using ReadyPlayerMe.Core;
using PlayerMovementScript;


public class ReadyPlayerMeAvatarLoader : MonoBehaviour
{
    [SerializeField] private GameComponentManager gameComponentManager;

    [SerializeField] private Animator animator;
    [SerializeField] private RuntimeAnimatorController animatorController;

    public void LoadAvatar(string url)
    {
        //chnaged AvatarLoader to AvatarObjetLoader
        AvatarObjectLoader avatarLoader = new AvatarObjectLoader();
        avatarLoader.OnCompleted += AvatarLoadComplete;
        avatarLoader.OnFailed += AvatarLoadFail;
        avatarLoader.LoadAvatar(url);
        
    }

    private void AvatarLoadFail(object sender , FailureEventArgs e)
    {
        Debug.Log("Avatar Failed to load: " + e.Message);
    }

    private void AvatarLoadComplete(object sender , CompletionEventArgs e)
    {
        if (this.GetComponent<NetworkObject>().HasStateAuthority)
        {
            var avatar = e.Avatar;
            avatar.name = "Avatar";


            avatar.transform.parent = transform;


            avatar.transform.localPosition = Vector3.zero;
            avatar.transform.localEulerAngles = Vector3.zero;
            gameComponentManager.playerMovement.avatar = avatar.transform;
            animator = avatar.GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorController;

            PlayerMovementPH playerMovement = gameComponentManager.playerMovement;
            playerMovement.avatar = avatar.transform;
            animator = avatar.GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorController;

            // Ensure the PlayerMovementPH script has been updated
            playerMovement.animator = animator;

            // Call the method to synchronize the Animator's magnitude parameter
            UpdateAnimatorMagnitude(playerMovement.speed);  





        }
    }

    private void UpdateAnimatorMagnitude(float magnitudeValue)
    {
        if (animator != null)
        {
            // Assuming "Magnitude" is the parameter name in the Animator Controller
            animator.SetFloat("Magnitude", magnitudeValue);
            animator.applyRootMotion = false;
            
            //for root motion.
            //animator.applyRootMotion = Mathf.Approximately(magnitudeValue, 0f);
        }
    }


}
    