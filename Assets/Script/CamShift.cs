using Cinemachine;
using UnityEngine;

public class CamShift : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera cam = default;

    private void Awake()
    {
        cam.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cam.gameObject.SetActive(true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cam.gameObject.SetActive(false);
        }

    }
}