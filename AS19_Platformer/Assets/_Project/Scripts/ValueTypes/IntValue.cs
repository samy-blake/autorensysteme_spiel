using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ScriptableObject
// Hat unserialisierte Werte (runtimeValue), die also beim Neustart resettet werden.
// Da es eine extern gespeicherte Datei ist, die außerhalb der Szene lebt, können wir sie in Fields referenzieren.
// Das erlaubt uns, Variablen ohne ein Singleton zu kommunizieren.
// Außerdem haben wir ein Event, das getriggert wird, wenn sich der Wert ändert - das kann von anderen Scripten
// subscribed werden, damit sie dynamisch auf Änderungen reagieren. (Keine Update-Funktion nötig.)
// Das gleiche Prinzip könnten wir auch für andere Variablentypen anwenden (float, bool, etc.).
// Siehe BoolValue.cs
[CreateAssetMenu(fileName = "IntValue", menuName = "Data/Values/Int")]
// ^ Erlaubt uns, durch das Kontextmenü (Rechtsklick) einen neuen Wert zu erstellen
public class IntValue : ScriptableObject, ISerializationCallbackReceiver
{
	public int defaultValue; // Sollte eigentlich eine Property sein, damit wir sie extern nicht setten können.
	[NonSerialized] private int runtimeValue;

	public event Action<int> OnValueChanged;

	public int RuntimeValue
	{
		get { return runtimeValue; }
		set
		{
			if (value == runtimeValue)
				return;

			runtimeValue = value;
			OnValueChanged?.Invoke(runtimeValue);
		}
	}

	public void OnBeforeSerialize()
	{
		// Leer, aber wir müssen es aufgrund des Interfaces (ISerializationCallbackReceiver) implementieren.
	}

	public void OnAfterDeserialize()
	{
		runtimeValue = defaultValue;
	}
}