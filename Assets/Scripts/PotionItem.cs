using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PotionItem : MonoBehaviour
{
    public PotionType PotionType;

    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] Collider2D collider;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] int orderLayer = 0;

    float _originalGravityScale = 1f;
    private void Start()
    {
        TryGetComponent<Rigidbody2D>(out rigidbody);
        _originalGravityScale = rigidbody.gravityScale;
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = orderLayer;
    }

    public PotionItem GrabItem(Vector3 mousePosition)
    {
        // Set the gravity to zero while we're being held.
        rigidbody.gravityScale = 0;
        spriteRenderer.sortingOrder += 1;
        collider.enabled = false;
        // Return this potion item.
        return this;
    }

    public void DropItem()
    {
        spriteRenderer.sortingOrder -= 1;
        // Set the gravity scale back to normal.
        rigidbody.gravityScale = _originalGravityScale;
        collider.enabled = true;
    }
}
