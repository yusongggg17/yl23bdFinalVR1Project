using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HoldWithProgressBar : MonoBehaviour
{
    public XRGrabInteractable interactable;
    public float holdTime = 2f;
    public GameObject thingToShow;
    public Slider progressBar;

    private float holdTimer = 0f;
    private bool isHolding = false;
    private bool triggered = false;

    private void OnEnable()
    {
        interactable.selectEntered.AddListener(OnGrab);
        interactable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnGrab);
        interactable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        isHolding = true;
        triggered = false;
        holdTimer = 0f;
        progressBar.value = 0f;
        progressBar.gameObject.SetActive(true);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isHolding = false;
        holdTimer = 0f;
        progressBar.value = 0f;
        progressBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isHolding || triggered) return;

        holdTimer += Time.deltaTime;
        progressBar.value = holdTimer / holdTime;

        if (holdTimer >= holdTime)
        {
            TriggerAction();
        }
    }

    private void TriggerAction()
    {
        triggered = true;
        progressBar.gameObject.SetActive(false);
        thingToShow.SetActive(true);
        Debug.Log("Hold complete — action triggered!");
    }
}
