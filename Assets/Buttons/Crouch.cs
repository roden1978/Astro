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
        
            try
            {
                if (!player) player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                player.UICrouchButton = player && !player.UICrouchButton;
                image.color = player.UICrouchButton ? Color.green : Color.white;
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw;
            }
    }

  // Start is called before the first frame update
    void Start()
    {
        
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
