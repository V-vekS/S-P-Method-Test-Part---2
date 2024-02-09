using ReadyPlayerMe.Core;
using UnityEngine;
using PlayerMovementScript;
using System;


public class AvatarLoader : MonoBehaviour
{
    [SerializeField] private GameObject placeHolder;
    [SerializeField] private PlayerMovementPH playerMovementScript;

    private void Start()
    {
        Debug.Log("Avatar loader connected");
        string url = "https://models.readyplayer.me/65732d85245f556b8079c5a9.glb";

        AvatarObjectLoader loader = new AvatarObjectLoader(); //it uses AvatrObjectLoader you idiot..

        loader.LoadAvatar(url);
        loader.OnCompleted += OnAvatarCreated;
        Debug.Log("End of start in AL");


    }

    private void OnAvatarCreated(object sender, CompletionEventArgs args)
    {
        Debug.Log("Inside OnAvatarCreated Method");
        // Disable the placeholder
        placeHolder.SetActive(false);
        //DisableMainCamera();

        // Set the parent of the ReadyPlayerMe avatar to be the same as the placeholder
        args.Avatar.transform.SetParent(placeHolder.transform.parent, worldPositionStays: false);

        // Set the position and rotation of the ReadyPlayerMe avatar to match the placeholder
        args.Avatar.transform.position = placeHolder.transform.position;
        args.Avatar.transform.rotation = placeHolder.transform.rotation;

        // Use the animator from the placeholder for the ReadyPlayerMe avatar.
        Animator placeholderAnimator = placeHolder.GetComponent<Animator>();
        if (placeholderAnimator != null)
        {
            Animator readyPlayerMeAnimator = args.Avatar.GetComponent<Animator>();
            if (readyPlayerMeAnimator != null)
            {
                readyPlayerMeAnimator.runtimeAnimatorController = placeholderAnimator.runtimeAnimatorController;
                readyPlayerMeAnimator.applyRootMotion = false;

                // Set the avatar and animator in PlayerMovementPH script.
                playerMovementScript.SetAvatarAndAnimator(args.Avatar.transform, readyPlayerMeAnimator);

            }
        }
        Debug.Log("Avatar created successfully.");
        //playerMovementScript.enabled = true;
    }

    /*public void DisableMainCamera()
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if(mainCamera != null)
        {
            mainCamera.SetActive(false);
        } 
    }*/



}

