using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cauldron : MonoBehaviour
{
    public UnityEvent MonsterMadeEvent;
    public UnityEvent DudMadeEvent;
    public System.Action OnPotionAddedAction;
    [SerializeField] int _numOfPotionsToMakeCombo = 2;
    [SerializeField] MonsterData[] _monsters;

    List<PotionType> _potionsAdded = new List<PotionType>();

    private void Awake()
    {
        _potionsAdded.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PotionItem>(out PotionItem potion))
        {
        Debug.Log("triggered");
            PotionItemManager.Instance.ResetPotion(potion);
            _potionsAdded.Add(potion.PotionType);
            if(_potionsAdded.Count == _numOfPotionsToMakeCombo)
            {
                // TODO: Determine which monster is created.
                MonsterData __madeMonster = DetermineMonster();

                if(__madeMonster == null)
                {
                    Debug.Log("Dud made.");
                    DudMadeEvent?.Invoke();
                }
                else
                {
                    Debug.Log("Monster made:" + __madeMonster.name);
                    MonsterMadeEvent?.Invoke();
                }
                _potionsAdded.Clear();
                PotionItemManager.Instance.RevealAllPotions();
            }
        }
    }

    MonsterData DetermineMonster()
    {
        for(int i = 0; i <_monsters.Length; i++)
        {
            if(_monsters[i].FirstIngredient == _potionsAdded[0] && _monsters[i].SecondIngredient == _potionsAdded[1])
            {
                return _monsters[i];
            }
        }
        return null;
    }

}
