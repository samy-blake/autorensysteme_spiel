using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// Statt den Spieler mittig anzuzeigen, schaut die Kamera etwas mehr in Blickrichtung.
[RequireComponent(typeof(CinemachineVirtualCamera))]
// ^ Wenn das Component nicht existiert, wird es erstellt.
// Das Component kann nicht gelöscht werden, solange das Script auf dem gameObject ist.
// Erspart uns, abzufragen, ob wir das Component haben—wir haben es definitiv.
public class CM_Gameplay : MonoBehaviour
{
//    public BoolValue facingRight;
    public MovementFromInput playerMovement;
    [Range(0, 0.5f)] public float offset = 0.3f;

    private CinemachineFramingTransposer transposer;

    private float targetOffset;
    private float targetBias;
    private bool prevState;

    public float smoothingTime = 1f;
    private float offsetVelocity; // wird als ref-Variable in SmoothDamp verändert. Am besten in Ruhe lassen.
    private float biasVelocity;

    private void Start()
    {
        transposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();

        prevState = IsFacingRight();
        UpdateTargetValue();
    }

    private void LateUpdate()
    {
        if (prevState != IsFacingRight())
        {
            UpdateTargetValue();
            prevState = IsFacingRight();
        }
        // Alternatives Smoothing: Mathf.MoveTowards
        // Oder auch: transposer.m_ScreenX = Mathf.Lerp(transposer.m_ScreenX, targetOffset, 2 * Time.deltaTime); // Mathematisch nicht richtig, aber sieht trotzdem gut aus.
        transposer.m_ScreenX = Mathf.SmoothDamp(transposer.m_ScreenX, targetOffset, ref offsetVelocity, smoothingTime);
        transposer.m_BiasX = Mathf.SmoothDamp(transposer.m_BiasX, targetBias, ref biasVelocity, smoothingTime);
    }

    private void UpdateTargetValue()
    {
        // CONDITION ? TRUE : FALSE;
        targetOffset = IsFacingRight() ? 0.5f - offset : 0.5f + offset; // 0.5f ist der "Nullpunkt" vom Offset
        targetBias = IsFacingRight() ? offset : -offset;
    }

    // Als Funktion, weil sich die facingRight-Variable im Laufe der Entwicklung mehrmals geändert hat.
    // So muss man es nur an einer Stelle anpassen.
    private bool IsFacingRight()
    {
        return playerMovement.FacingRight;
    }
}
