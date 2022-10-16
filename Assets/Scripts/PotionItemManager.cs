using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItemManager : MonoBehaviour
{

    public static PotionItemManager Instance;
    [System.Serializable]
    struct PotionInfo
    {
        public PotionItem Potion;
        public Vector3 SpawnPosition;
    }
    [SerializeField]
    List<PotionInfo> _potions = new List<PotionInfo>(0);
    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);

        Instance = this;
    }

    public void ResetPotion(PotionItem potionItem, bool isVisible)
    {
        foreach(PotionInfo info in _potions)
        {
            if(info.Potion == potionItem)
            {
                potionItem.gameObject.SetActive(isVisible);
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
    public void HideAllPotions()
    {
        foreach (PotionInfo info in _potions)
        {
            info.Potion.gameObject.SetActive(false);
        }
    }
}
