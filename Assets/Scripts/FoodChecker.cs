using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;

public class FoodChecker : MonoBehaviour
{
    public DialogueManager animal;
    private string PenguinFood1Tag="";
    private string DeerFood1Tag = "";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            CheckFood(collision.gameObject);
        }

    }
    private void CheckFood(GameObject food)
    {
        if (animal.currentAnimal.tag == "Penguin")
        {
            if (animal.currentDialogue == 0)
            {
                if (CheckChildTag(food, "Nut"))
                {
                    animal.CorrectDialogue();
                }
                else
                {
                    animal.WrongDialogue();
                }
            }
            else if (animal.currentDialogue == 1)
            {
                CheckTwoFood(food, "Apple", "Pie", ref PenguinFood1Tag);
            }
            else if (animal.currentDialogue == 2)
            {
                CheckTwoFood(food, "Chocolate", "Icecream", ref PenguinFood1Tag);
            }
        }
        else if (animal.currentAnimal.tag == "Deer") {
            if (animal.currentDialogue == 0)
            {

            }
            else if (animal.currentDialogue == 1)
            {

            }
            else if (animal.currentDialogue == 2)
            {
            }
        }
        Destroy(food);
    }

    private bool CheckChildTag(GameObject parent, string Targettag) {
        foreach (Transform childTransform in parent.transform) {
            if (childTransform.CompareTag(Targettag)) {
                return true;
            }
        }
        return false;
    }

    private void CheckTwoFood(GameObject food, string targetFood1, string targetFood2, ref string targetAnimalFoodTag) {

        if (CheckChildTag(food, targetFood1))
        {
            if (targetAnimalFoodTag == targetFood2)
            {
                animal.CorrectDialogue();
                targetAnimalFoodTag = "";
            }
            else
            {
                targetAnimalFoodTag = targetFood1;
                animal.RequireAdditionalDialogue();
            }
        }
        if (CheckChildTag(food, targetFood2))
        {
            if (targetAnimalFoodTag == targetFood1)
            {
                animal.CorrectDialogue();
                targetAnimalFoodTag = "";
            }
            else
            {
                targetAnimalFoodTag = targetFood2;
                animal.RequireAdditionalDialogue();
            }
        }
        //else {
        //    animal.WrongDialogue();
        //}

    }

}
