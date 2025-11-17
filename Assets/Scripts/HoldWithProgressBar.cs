using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HoldWithProgressBar : MonoBehaviour
{
    public XRGrabInteractable interactable;
    public float holdTime = 2f;
    public GameObject[] ingredientsPrefab;
    private int numTotalIngredients;
    List<GameObject> spawnedIngredients = new List<GameObject>();

    public Slider progressBar;

    private float holdTimer = 0f;
    private bool isHolding = false;
    private bool triggered = false;

    private void Start()
    {
        numTotalIngredients = ingredientsPrefab.Length;
        if(interactable == null) Debug.LogError("Interactable is NULL");
        if (progressBar == null) Debug.LogError("Progress bar is NULL");
        if (ingredientsPrefab == null || ingredientsPrefab.Length == 0) Debug.LogError("IngredientsPrefab array is EMPTY");
    }

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
        ShowRandomIngredient();
        Debug.Log("Hold complete — action triggered!");
    }

    private void ShowRandomIngredient() {
        int randomIndex= Random.Range(0, numTotalIngredients);
        Vector3 spawnPos = new Vector3(Random.Range(35.5f, 37f),0,Random.Range(57.5f, 58.5f));
        GameObject clone =Instantiate(ingredientsPrefab[randomIndex], spawnPos, Quaternion.identity);
        spawnedIngredients.Add(clone);
        Debug.Log("random ingredient spawned");
    }
}
