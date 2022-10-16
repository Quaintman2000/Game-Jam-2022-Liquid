using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItemManager : MonoBehaviour
{

    public static PotionItemManager Instance;
    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);

        Instance = this;
    }
    struct PotionInfo
    {
        public PotionItem Potion;
        public Vector3 SpawnPosition;
    }
    List<PotionInfo> _potions = new List<PotionInfo>(0);

    public void AddPotion(PotionItem item)
    {
        PotionInfo __newPotion = new PotionInfo();
        __newPotion.Potion = item;
        __newPotion.SpawnPosition = item.transform.position;
        _potions.Add(__newPotion);
    }

    public void ResetPotion(PotionItem potionItem)
    {
        foreach(PotionInfo info in _potions)
        {
            if(info.Potion == potionItem)
            {
                potionItem.gameObject.SetActive(false);
                potionItem.transform.position = info.SpawnPosition;
                break;
            }
        }
    }

    public void RevealAllPotions()
    {
        foreach (PotionInfo info in _potions)
        {
            info.Potion.gameObject.SetActive(true);
        }

    }
}
