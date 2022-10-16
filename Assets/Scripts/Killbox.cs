using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PotionItem>(out PotionItem potion))
        {
            PotionItemManager.Instance.ResetPotion(potion, true);
        }
    }
}
