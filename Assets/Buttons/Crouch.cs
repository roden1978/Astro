﻿using UnityEngine.EventSystems;
using UnityEngine;

public class Crouch : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    private PlayerController playerController;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (playerController != null)
        {
            if (gameObject.name == "CrouchButton")
            {
                playerController.UICrouchButton = !playerController.UICrouchButton;
                playerController.Crouch();
            }
        } else
        {
            print("player not found");
        }
        
    }

    
    // Start is called before the first frame update
    void Start()
    {
	    playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

 }
