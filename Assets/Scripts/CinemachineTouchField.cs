using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class CinemachineTouchField : MonoBehaviour, IDragHandler
{
    public CinemachineFreeLook freeLookCamera; // Reference to Cinemachine Free Look Camera
    public float horizontalSensitivity = 0.2f; // Sensitivity for horizontal rotation
    public float verticalSensitivity = 0.1f;   // Sensitivity for vertical rotation

    public void OnDrag(PointerEventData eventData)
    {
        // Adjust horizontal (X-axis) rotation
        freeLookCamera.m_XAxis.Value += eventData.delta.x * horizontalSensitivity;

        // Adjust vertical (Y-axis) rotation
        freeLookCamera.m_YAxis.Value -= eventData.delta.y * verticalSensitivity;

        // Ensure Y-axis stays in bounds (to prevent flipping or extreme angles)
        freeLookCamera.m_YAxis.Value = Mathf.Clamp(freeLookCamera.m_YAxis.Value, 0f, 1f);
    }
}
