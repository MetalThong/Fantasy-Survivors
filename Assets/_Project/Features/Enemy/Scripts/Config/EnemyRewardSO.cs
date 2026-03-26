using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_EnemyReward", menuName = "SO/Enemy/EnemyReward")]
public class EnemyRewardSO : ScriptableObject
{
    [SerializeField] private float expReward;
    [SerializeField] private int expAmount;
    [SerializeField] private float epicDropRate;

    public float ExpReward => expReward;
    public int ExpAmount => expAmount;
    public float EpicDropRate => epicDropRate;
}
