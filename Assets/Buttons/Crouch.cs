using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Crouch : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    private Player player;
    private Image image;
    public void OnPointerDown(PointerEventData eventData)
    {
        player.UICrouchButton = player && !player.UICrouchButton;
        image.color = player.UICrouchButton ? Color.green : Color.white;
    }

  // Start is called before the first frame update
    void Start()
    {
        try
        {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
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
