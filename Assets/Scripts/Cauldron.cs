using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;

public class Cauldron : MonoBehaviour
{
    
    public UnityEvent MonsterMadeEvent;
    public UnityEvent DudMadeEvent;
    public System.Action OnPotionAddedAction;
    [SerializeField] BookUIManager _book;
    [SerializeField] int _numOfPotionsToMakeCombo = 2;
    [SerializeField] MonsterData[] _monsters;
    [SerializeField] SpriteRenderer _discoverSpriteRenderer;
    [SerializeField] AnimationCurve _animationCurve;
    [SerializeField] float _animationTime;
    [SerializeField] float _showTime;
    [SerializeField] RectTransform _bookUIObject;

    List<PotionType> _potionsAdded = new List<PotionType>();

    private void Awake()
    {
        _potionsAdded.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PotionItem>(out PotionItem potion))
        {
            PotionItemManager.Instance.ResetPotion(potion,false);
            _potionsAdded.Add(potion.PotionType);
            if(_potionsAdded.Count == _numOfPotionsToMakeCombo)
            {
                // TODO: Determine which monster is created.
                MonsterData __madeMonster = DetermineMonster();

                if(__madeMonster == null)
                {
                    DudMadeEvent?.Invoke();
                }
                else
                {
                    MonsterMadeEvent?.Invoke();
                    PlayReveilAnimation(__madeMonster);
                    _book.DiscoverMonster(__madeMonster);
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

    public async void PlayReveilAnimation(MonsterData monster)
    {
        var __originalPosition = _discoverSpriteRenderer.transform.position;

        _discoverSpriteRenderer.gameObject.SetActive(true);
        var __moveToPosition = Camera.main.ScreenToWorldPoint(_bookUIObject.position + (Vector3.forward * 10));

        var __originalScale = _discoverSpriteRenderer.transform.localScale;
        _discoverSpriteRenderer.sprite = monster.MonsterImage;

        var __distance = Vector2.Distance(_discoverSpriteRenderer.transform.position, __moveToPosition);
        var __speed = __distance / _animationTime;

        float __showTimeLeft = _showTime;

        while(__showTimeLeft >= 0)
        {
            __showTimeLeft -= Time.deltaTime;
            await Task.Yield();
        }

        var __startTime = Time.time;
        float __timeLeft = _animationTime;
        while (__timeLeft >= 0)
        {
            _discoverSpriteRenderer.transform.position += (__moveToPosition - _discoverSpriteRenderer.transform.position) * __speed * Time.deltaTime;
            _discoverSpriteRenderer.transform.localScale = __originalScale * _animationCurve.Evaluate(Time.time - __startTime);
            __timeLeft -= Time.deltaTime;
            await Task.Yield();
        }

        _discoverSpriteRenderer.gameObject.SetActive(false);
        _discoverSpriteRenderer.transform.position = __originalPosition;
        _discoverSpriteRenderer.transform.localScale = __originalScale;
    }

}
