using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class BookUIManager : MonoBehaviour
{
    [System.Serializable]
    struct MonsterIcon
    {
        public MonsterData Monster;
        public Image MaskImage;
        public bool IsRevealed;
    }
    [SerializeField] AnimationCurve _revealAmountCurve;
    [SerializeField]
    List<MonsterIcon> _monsterIcons = new List<MonsterIcon>();
    [SerializeField]
    float _delayBetweenEachReveal = 0f;
    [SerializeField] float _revealTime = 1f;

    List<MonsterIcon> _newlyFoundMonsterIcons = new List<MonsterIcon>();

    private void OnEnable()
    {
        if (_newlyFoundMonsterIcons.Count > 0)
            StartRevealRoutinesRoutine();
    }

    private void OnDisable()
    {
        foreach (MonsterIcon monster in _monsterIcons)
        {
            Color __revealColor = new Color(0, 0, 0, 0);
            monster.MaskImage.color = __revealColor;
        }
    }
    async void StartRevealRoutinesRoutine()
    {
        int i = 0;
        float __nextRevealTime = Time.time;
        do
        {
            if (Time.time > __nextRevealTime)
            {
                __nextRevealTime += _delayBetweenEachReveal;
                RevealMonsterIconRoutine(_newlyFoundMonsterIcons[i]);
                i++;
            }
            else
            {
                await Task.Yield();
            }
        } while (i < _newlyFoundMonsterIcons.Count);

        _newlyFoundMonsterIcons.Clear();
    }

    async void RevealMonsterIconRoutine(MonsterIcon monsterIcon)
    {
        var __startTime = Time.time;
        float __alphaEvaluation = 1;
        while (__alphaEvaluation > 0)
        {
            __alphaEvaluation = 1 - _revealAmountCurve.Evaluate((Time.time - __startTime) / _revealTime);
            Color __revealColor = new Color(0, 0, 0, __alphaEvaluation);
            monsterIcon.MaskImage.color = __revealColor;
            await Task.Yield();
        }

    }

    public void DiscoverMonster(MonsterData monster)
    {
        Debug.Log("DiscoveredMonster: " + monster.name);
        MonsterIcon __foundIcon = FindIconForMonster(monster);
        __foundIcon.IsRevealed = true;
        _newlyFoundMonsterIcons.Add(__foundIcon);
    }

    MonsterIcon FindIconForMonster(MonsterData monster)
    {
        foreach (MonsterIcon icon in _monsterIcons)
        {
            if (monster == icon.Monster)
            {
                return icon;
            }
        }
        return _monsterIcons[0];
    }
}
