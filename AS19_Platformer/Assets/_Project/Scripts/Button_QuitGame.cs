using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Button_QuitGame : MonoBehaviour
{
	private Button button;	
	
	private void Awake()
	{
		button = GetComponent<Button>();
	}

	private void Start()
	{
		button.onClick.AddListener(QuitGame);
	}

	private void QuitGame()
	{
		// Debug.Log("Quit Game");
		Application.Quit(); // Funktioniert nur im Build
		
		// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/preprocessor-directives/preprocessor-if
		// Der Code im if wird nur augeführt, wenn wir uns im Edior befinden.
		// Ohne if hätten wir im Build einen Error, weil er den Namespace UnityEditor nicht besitzt.
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
}
