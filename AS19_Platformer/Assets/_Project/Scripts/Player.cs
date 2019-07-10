using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Im Laufe des Projekts redundant geworden. -> Funktionalität wurde aufgesplittet.
public class Player : MonoBehaviour
{
    public TrailRenderer trailRenderer;
    public BoolValue allowInput;
    
    // Könnte in den LevelManager (LevelManager.Instance.ResetPlayerPosition)
    public void ResetPosition(bool allowMovement)
    {
        StartCoroutine(WaitAndReset(allowMovement));
    }

    // Coroutine, damit wir bis zum Ende des Frames warten.
    // Kann sonst passieren, dass wir einen Dialog wegdrücken, und danach springen.
    // (Beides Leertaste.)
    IEnumerator WaitAndReset(bool allowMovement)
    {
        trailRenderer.enabled = false;
        transform.position = LevelManager.Instance.LastPosition;
        yield return new WaitForEndOfFrame();
        trailRenderer.enabled = true;
        trailRenderer.Clear();
        allowInput.RuntimeValue = allowMovement;
    }
}
