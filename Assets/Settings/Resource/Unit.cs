using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    void Start()
    {
        SelectionMovementManager.Instance.allUnitList.Add(gameObject);
        
    }
    private void OnDestroy()
    {
        SelectionMovementManager.Instance.allUnitList.Remove(gameObject);
    }
}
