using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Wird verwendet von Buttons (siehe Hauptmenü). Demonstriert Level Loading.
// Da die Funktionen public sind, können sie im Editor als Listener hinzugefügt werden.
// Die Parameter erlauben uns, die Werte direkt im Listener einzustellen.
// Wir können also das gleiche LoadLevel-Component für mehrere Buttons verwenden.
public class LoadLevel : MonoBehaviour
{
	public void LoadLevelById(int levelId)
	{
		SceneManager.LoadScene(levelId);
	}
	
	public void LoadLevelByName(string levelName)
	{
		SceneManager.LoadScene(levelName);
	}
}
