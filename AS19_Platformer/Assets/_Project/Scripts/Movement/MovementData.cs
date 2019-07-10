using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Beispiel für ein ScriptableObject, in dem wir Werte abspeichern.
// Können das mit Gegnern und Spielern verlinken, die sich alle aktualisieren, wenn wir das SO ändern (sogar runtime).
[CreateAssetMenu(fileName ="MovementData", menuName = "Data/Movement")]
public class MovementData : ScriptableObject
{
	public float walkSpeed = 10f;
	public float jumpSpeed = 40f;
}
