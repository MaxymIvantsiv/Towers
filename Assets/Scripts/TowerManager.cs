using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private List<Tower> TowersPrefabs = new List<Tower>();

    private Vector3 offsetPos = new Vector3(0, 0.2f, 0);

    private CellManager CellManager;

    public const string TagTower = "Tower";

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        CellManager = FindObjectOfType<CellManager>();
    }

    public void AddTowerAtRandomCell(int towerIndex)
    {
        var freeCells = CellManager.GetFreeCells();
        if (freeCells.Count > 0)
        {
            int randomIndex = Random.Range(0, freeCells.Count);
            Cell selectedCell = freeCells[randomIndex];
            Vector3 towerPos = selectedCell.transform.position + offsetPos;

            var newTower = Instantiate(TowersPrefabs[towerIndex], towerPos, Quaternion.identity);

            CellManager.SetFree(randomIndex, false);
        }
    }
}
