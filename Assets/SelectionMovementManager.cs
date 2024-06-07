using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SelectionMovementManager : MonoBehaviour
{
    public static SelectionMovementManager Instance { get; set; }

    public List<GameObject> allUnitList = new List<GameObject>();
    public List<GameObject> selectedUnitList = new List<GameObject>();

    public LayerMask clickable;
    public LayerMask ground;
    public GameObject groundMarker;

    private Camera cam;
    private void Awake()
    { 
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        { 
            Instance = this;
        }
    }

    private void Start()
    {
       cam = Camera.main;
    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (Input.GetMouseButton(0))
                {
                    MultiSelection(hit.collider.gameObject);
                }
                else
                {
                    SelectByClicking(hit.collider.gameObject);
                }
            }
            else
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
                {
                    DeSelectAll();
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && selectedUnitList.Count > 0)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                groundMarker.transform.position = hit.point;

                groundMarker.SetActive(false);
                groundMarker.SetActive(true);

            }
        }
    }

        private void MultiSelection(GameObject unit)
        {
            if (!selectedUnitList.Contains(unit))
            {
                selectedUnitList.Add(unit);
                SelectedUnit(unit, true);
            }
            else
            {
                SelectedUnit(unit, false);
                selectedUnitList.Remove(unit);
            }
        }

        public void DeSelectAll()
        {
            foreach (var unit in selectedUnitList)
            {
                 SelectedUnit(unit, false);
            }
            groundMarker.SetActive(false);
            selectedUnitList.Clear();
        }

        private void SelectByClicking(GameObject unit)
        {
            DeSelectAll();

            selectedUnitList.Add(unit);

            SelectedUnit(unit, true);
        }
        private void EnableMovment(GameObject unit, bool shouldMove)
        {
            unit.GetComponent<UnitMovement>().enabled = shouldMove;
        }

        private void TriggerSelectionIndicator(GameObject unit, bool visible)
        { 
            unit.transform.GetChild(0).gameObject.SetActive(visible);
        }

        internal void DragSelect(GameObject unit)
        {
            if (selectedUnitList.Contains(unit) == false)
            {
                selectedUnitList.Add(unit);
                SelectedUnit(unit, true);
            }
        }
        private void SelectedUnit(GameObject unit, bool selected)
        {
            TriggerSelectionIndicator(unit, true);
            EnableMovment(unit, true);
        }

}


