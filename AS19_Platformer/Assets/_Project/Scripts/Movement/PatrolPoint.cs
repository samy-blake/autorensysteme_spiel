using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Structs sind ähnlich wie Klassen. Praktisch, um Werte abzuspeichern.
[System.Serializable]
public struct PatrolPoint
{
    public Transform transform;
    public float waitForSeconds;

    // Macht es bequemer für uns.
    public float GetPosX()
    {
        return transform.position.x;
    }
}
