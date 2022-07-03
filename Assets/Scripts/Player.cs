using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private string RaycastHitTag;

    private TowerManager TowerManager;
    private float MouseX;
    private float MouseY;
    private Vector3 MovingPos;
    private Tower currentTower;

    private void Start()
    {
        Init();
    }

    void Update()
    {
        Inputing();
    }

    private void Init()
    {
        TowerManager = FindObjectOfType<TowerManager>();
    }

    void Inputing()
    {
        Brain();
    }

    public void ButtonInput()
    {
        TowerManager.AddTowerAtRandomCell(0);       
    }

    private RaycastHit SetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            RaycastHitTag = hit.transform.tag;
            MovingPos = hit.point;
        }

        return hit;
    }

    void Brain()
    {
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");

        var hit = SetPosition();

        if (hit.transform != null)
        {
            if (Input.GetMouseButton(0))
            {
                switch (hit.transform.tag)
                {
                    case TowerManager.TagTower:

                        Tower tower = hit.transform.GetComponent<Tower>();
                        currentTower = tower;
                        currentTower.Move(MovingPos);

                        break;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            RaycastHitTag = "";
            if(currentTower != null)
            {
                currentTower.Release();
            }
        }
    }
}
