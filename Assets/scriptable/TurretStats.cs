using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turret stats")]
public class TurretStats : ScriptableObject
{
    public int Speed;
    public int Damge;
    public float DelayAttack;
    public float RangeAttack;
}
