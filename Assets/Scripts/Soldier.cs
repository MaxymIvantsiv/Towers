using UnityEngine;

public enum Team
{
    Player,
    Enemy
}

abstract public class Soldier : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float life;
    [SerializeField] public Team myTeam;
    [SerializeField] protected float raycastDistance;
    [SerializeField] protected float destroyTime;
    [SerializeField] protected ParticleSystem destroyEffectPrefab;
    protected Animator myAnimator;
    protected string animBoolWalking = "Walking";
    protected string animTriggerAttack;
    protected bool walk;
    protected Weapon myWeapon;
    protected bool canTakeDamage;
    protected Vector3 raycastPositionOffset;
    protected bool attackEnemy;
    protected float destryTimeRemaining;

    protected abstract void Initing();
    protected abstract void Brain();
    public abstract void TakeDamage(float damage);
    public abstract void Death();
    protected abstract void Destroying();
    protected abstract void TriggerEnter(Collider other);
    protected abstract void TriggerExit(Collider other);

}
