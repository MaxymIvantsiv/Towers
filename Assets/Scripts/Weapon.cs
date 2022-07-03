using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Team myTeam;
    [SerializeField] protected float damage = 50f;

    public float GetDamage()
    {
        return damage;
    }

    public Team GetTeam()
    {
        return myTeam;
    }

    public void SetTeam(Team team)
    {
        myTeam = team;
    }
}
