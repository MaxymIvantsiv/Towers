using UnityEngine;

public class SwordMan : Soldier
{
    private void Start()
    {
        Initing();
    }

    private void Update()
    {
        Destroying();
        Brain();
    }

    protected override void Initing()
    {
        myAnimator = GetComponent<Animator>();
        myWeapon = GetComponentInChildren<Weapon>();
        myWeapon.SetTeam(myTeam);
        life = 100;
        //raycastDistance = 2;
        animBoolWalking = "Walking";
        animTriggerAttack = "Attack";
        walk = true;
        canTakeDamage = true;
        raycastPositionOffset = new Vector3(0, 0.4f, 0);
        attackEnemy = false;
        destroyTime = 30;
        destryTimeRemaining = 0;
        destryTimeRemaining = destroyTime;
    }

    protected override void Brain()
    {
        RaycastHit objectHit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 raycastPos = transform.position + raycastPositionOffset;
        Soldier soldierContact = null;

        Debug.DrawRay(raycastPos, fwd * raycastDistance, Color.green);

        if (Physics.Raycast(raycastPos, fwd, out objectHit, raycastDistance))
        {
            soldierContact = objectHit.transform.GetComponent<Soldier>();
        }

        if (soldierContact != null)
        {
            if (soldierContact.myTeam == myTeam) // if soldier from my team
            {
                walk = false; // Stop

                attackEnemy = false;//Dont attack
            }
            else // if soldier from enemy team
            {
                attackEnemy = true; // Attack
                walk = false; // Stop
            }
        }
        else
        {
            walk = true;
            attackEnemy = false;
        }

        if (attackEnemy)
        {
            myAnimator.SetTrigger(animTriggerAttack);
        }

        if (walk)
        {
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        }

        myAnimator.SetBool(animBoolWalking, walk);
    }

    public override void TakeDamage(float damage)
    {
        if (life > 0)
        {
            if (canTakeDamage)
            {
                life -= damage;
                canTakeDamage = false;
            }
        }
        else
        {
            Death();
        }
    }

    public override void Death()
    {
        var expEffect = Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
        Destroy(transform.gameObject.gameObject);
    }

    protected override void Destroying()
    {
        if (destryTimeRemaining > 0)
        {
            destryTimeRemaining -= Time.deltaTime;
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }

    protected override void TriggerEnter(Collider other)
    {
        var enemyWeapon = other.GetComponentInChildren<Weapon>();

        if (enemyWeapon != null)
        {
            Team enemyWeaponTeam = enemyWeapon.GetTeam();

            if (enemyWeaponTeam != myTeam)
            {
                float damage = enemyWeapon.GetDamage();

                TakeDamage(damage);
            }
        }
    }

    protected override void TriggerExit(Collider other)
    {
        var enemyWeapon = other.GetComponentInChildren<Weapon>();

        if (enemyWeapon != null)
        {
            Team enemyWeaponTeam = enemyWeapon.GetTeam();

            if (enemyWeaponTeam != myTeam)
            {
                canTakeDamage = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerExit(other);
    }
}
