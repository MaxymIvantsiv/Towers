using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Cell currentCell;
    [SerializeField] private Tower currentTower;
    [SerializeField] private int level = 1;
    [SerializeField] private float spawnTime = 5;
    [SerializeField] private float spawnTimeRemaining;
    [SerializeField] private List<Soldier> soldierPrefabs = new List<Soldier>();

    private Vector3 offsetPos = new Vector3(0, 0.2f, 0);
    private TextMeshPro levelCounterText;

    private void Start()
    {
        levelCounterText = GetComponentInChildren<TextMeshPro>();
        spawnTimeRemaining = spawnTime;
    }

    private void Update()
    {
        Spawning();
    }

    private void Spawning()
    {
        if (spawnTimeRemaining > 0)
        {
            spawnTimeRemaining -= Time.deltaTime;
        }
        else
        {
            SpawnFriend();
            spawnTimeRemaining = spawnTime;
        }
    }

    private void SpawnFriend()
    {
        Vector3 newPos = transform.position;

        Soldier soldier = null;

        switch (level)
        {
            case 1:
                soldier = soldierPrefabs[0];
                break;
            case 2:
                soldier = soldierPrefabs[1];
                break;
            case 4:
                soldier = soldierPrefabs[2];
                break;
        }


        Instantiate(soldier, newPos, transform.rotation * Quaternion.Euler(0f, 0, 0f));
    }

    public void Move(Vector3 pos)
    {
        float mouseXSpeed = Input.GetAxis("Mouse X");
        float mouseYSpeed = Input.GetAxis("Mouse Y");

        if (mouseXSpeed != 0 || mouseYSpeed != 0)
        {
            Vector3 myPos = transform.position;
            transform.position = new Vector3(pos.x, myPos.y, pos.z);
        }
    }

    public void Release()
    {
        if (currentCell != null)
        {
            transform.position = currentCell.transform.position + offsetPos;
        }

        if (currentTower != null)
        {
            int currentTowerLevel = currentTower.GetLevel();

            if (currentTowerLevel == level)
            {
                level = level + level;

                levelCounterText.text = level.ToString();

                Destroy(currentTower.gameObject);
            }
        }
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int newLevel)
    {
        level = newLevel;
    }

    private void OnTriggerStay(Collider other)
    {
        var cellComponent = other.gameObject.GetComponent<Cell>();
        var towerComponent = other.gameObject.GetComponent<Tower>();

        if (cellComponent != null)
        {
            Cell cell = other.GetComponent<Cell>();

            currentCell = cell;
        }

        if (towerComponent != null)
        {
            Tower tower = other.GetComponent<Tower>();

            currentTower = tower;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var tower = other.gameObject.GetComponent<Tower>();

        if (tower != null)
        {
            currentTower = null;
        }
    }
}
