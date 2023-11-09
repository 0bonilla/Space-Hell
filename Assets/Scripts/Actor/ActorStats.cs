using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActorStats", menuName = "Stats/ActorStats", order = 0)]
public class ActorStats : ScriptableObject
{
    [SerializeField] private ActorStatValue stats;

    public int MaxLife => stats.MaxLife;

    public float MovementSpeed => stats.MovementSpeed;
}

[System.Serializable]
public struct ActorStatValue
{
    public int MaxLife;
    public float MovementSpeed;
    public float NextShot;

}
