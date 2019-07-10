using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hier könnten wir Extensions verschiedenen Types speichern.
// Damit wir aber den Überblick behalten, erstellen wir pro Type eine neue Datei.
public static class ColorExtensions
{
	// Im Gegensatz zu einer "normalen" Funktion können wir das hier direkt durch eine Farbe aufrufen:
	// spriteRender.color.ChangeAlpha(0.2f);
	// Das macht es leichter für uns, und unsere Teammitglieder, die Funktion nicht zu vergessen.
	// (Gut für die Momente, in denen man ahnungslos IntelliSense-Vorschläge durchscrollt.)
	
	// Den ersten Parameter müssen wir beim Aufrufen nicht definieren, da er sich den automatisch
	// vom aufrufenden Typ holt.
	// (Im Beispiel also spriteRender.color.)
	
	// Anatomie:
	// public static TYPE Funktionsname(this TYPE variablenname)
	// + sonstige Parameter
	// Muss entsprechend TYPE returnen.
	
	/// <summary>
	/// Returns the color with the specified alpha.
	/// </summary>
	/// <param name="color"></param>
	/// <param name="alpha"></param>
	/// <returns></returns>
	public static Color ChangeAlpha(this Color color, float alpha)
	{
		return new Color(color.r, color.g, color.b, alpha);
	}	
	
	// Kürzere Schreibweise von dem hier:
	// spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
	// Oder:
	// Color tempColor = spriteRenderer.color;
	// tempColor.a = 0.5f;
	// spriteRenderer.color = tempColor;
	
}
