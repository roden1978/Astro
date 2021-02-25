using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Crouch : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject player;
    private Player playerController;
    private Image image;
    public void OnPointerDown(PointerEventData eventData)
    {
       //playerController.UICrouchButton = playerController && !playerController.UICrouchButton;
       //playerController.StateMashine.SwitchToNewState(typeof(CrouchState));
       image.color = playerController.UICrouchButton ? Color.green : Color.white;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            playerController = player.GetComponent<Player>();
        }
        catch (Exception e)
        {
            Debug.Log(e);
            throw;
        }

        try
        {
            image = gameObject.GetComponent<Image>();
        }
        catch (Exception e)
        {
            Debug.Log(e);
            throw;
        }
        
    }

 }
