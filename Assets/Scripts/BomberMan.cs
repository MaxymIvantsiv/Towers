using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberMan : Soldier
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
        life = 100;
        //raycastDistance = 1;
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
                walk = true;
                attackEnemy = false;
            }
        }
        else
        {
            walk = true;
            attackEnemy = false;
        }

        if (walk)
        {
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        }

        myAnimator.SetBool(animBoolWalking, walk);
    }

    public override void TakeDamage(float damage)
    {
        Death();
    }

    public override void Death()
    {
        var expEffect = Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
        Destroy(transform.gameObject);
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

    }

    protected override void TriggerExit(Collider other)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerExit(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.transform.GetComponent<Soldier>();

        if (enemy != null)
        {
            Team enemyTeam = enemy.myTeam;

            if(enemyTeam != myTeam)
            {
                enemy.Death();
                Death();
            }
        }
    }
}
