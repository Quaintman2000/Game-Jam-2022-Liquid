using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Cauldron _cauldron;
    // The current potionItem we're holding.
    PotionItem _potionItem = null;
    // Bool to keep track if we have a potion in hand.
    bool _hasPotionInHand => _potionItem != null;

    Vector3 _mousePosition = Vector3.zero;
    Vector3 _worldPositon;

    Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
        if (_cauldron != null)
            _cauldron.OnPotionAddedAction += DropPotion;

    }

    // Update is called once per frame
    void Update()
    {
        // Grab the mouse position.
        _mousePosition = Input.mousePosition;
        _mousePosition.z = 10;
        _worldPositon = _mainCamera.ScreenToWorldPoint(_mousePosition);
        // If we left click...
        if (Input.GetMouseButtonDown(0))
        {
            // Send a raycast from our mouse position.
            RaycastHit2D _hit;
            // If we hit something with our raycast...
            _hit = Physics2D.Raycast(_worldPositon, Vector2.zero, Mathf.Infinity);
            if (_hit.collider != null)
            {
                // If the thing we hit has a potion item script on it...
                if (_hit.collider.TryGetComponent<PotionItem>(out PotionItem item))
                {
                    // Set that item to be our current Item.
                    _potionItem = item.GrabItem(_mousePosition);
                }
            }
        }
        // Else, if we hold down the left mouse button and have a potion in hand...
        else if (Input.GetMouseButton(0) && _hasPotionInHand)
        {
            // Move the potion with your mouse.
            _potionItem.transform.position = _worldPositon;
        }
        // Else, if we let go of the left mouse button and have a potion in hand...
        else if (Input.GetMouseButtonUp(0) && _hasPotionInHand)
        {
            DropPotion();
        }
    }

    void DropPotion()
    {
        // Drop the item.
        _potionItem.DropItem();
        _potionItem = null;
    }
}
