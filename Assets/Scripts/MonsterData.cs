using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMonsterData", menuName ="Monster Data")]
public class MonsterData : ScriptableObject
{
    public new string name;
    public PotionType FirstIngredient;
    public PotionType SecondIngredient;
    public Sprite MonsterImage;
}
