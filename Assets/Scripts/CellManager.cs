using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    [SerializeField] private List<Cell> allCells = new List<Cell>();
    [SerializeField] private List<Cell> freeCells = new List<Cell>();

    private GameCycle myGameCycle;

    private void Start()
    {
        myGameCycle = FindObjectOfType<GameCycle>();
        allCells = FindCells();
        freeCells = FindFreeCells();
    }

    private List<Cell> FindCells()
    {
        List<Cell> cellsList;

        var cellsArray = FindObjectsOfType<Cell>();

        cellsList = new List<Cell>(cellsArray);

        return cellsList;
    }

    private List<Cell> FindFreeCells()
    {
        List<Cell> cellsList;

        cellsList = allCells.Where(ñ => ñ.Free).ToList();

        return cellsList;
    }

    public List<Cell> GetFreeCells()
    {
        freeCells = FindFreeCells();
        return freeCells;
    }

    public void SetFree(int index, bool free)
    {
        freeCells[index].SetFree(free);
        freeCells = FindFreeCells();
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Soldier>();

        if(enemy != null)
        {
            if (enemy.myTeam == Team.Enemy)
            {
                myGameCycle.State = GameState.Restart; // if enemy collide with red line - restart menu activate
                myGameCycle.SetPause(true); // pause game
            }
        }
    }
}
