using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypadMain : MonoBehaviour
{
    public TextMeshProUGUI[] numbers;
    public int[] codeNumbers;
    public GameObject barrier;

    private int numbersEnteredCount;
    private int[] numbersEntered;

    // Start is called before the first frame update
    void Start()
    {
        numbersEnteredCount = 0;
        numbersEntered = new int[codeNumbers.Length];
    }

    // Update is called once per frame
    void Update()
    {

    }
    // Enteres number pressed on keypad
    public void EnterNumber(int numberEntered)
    {
        numbers[numbersEnteredCount].SetText(numberEntered.ToString());
        numbersEntered[numbersEnteredCount] = numberEntered;
        numbersEnteredCount++;

        //runs when enough numbers have been punched in to check if matches code for the key pad
        if (numbersEnteredCount == codeNumbers.Length)
        {
            Debug.Log("Code lengths showing as matched");
            Debug.Log("numbers count is: " + numbersEnteredCount);
            Debug.Log("Length is " + (codeNumbers.Length - 1));
            bool codeEntered = true;
            for (int i = 0; i < codeNumbers.Length; i++)
            {
                if (codeNumbers[i] != numbersEntered[i])
                {
                    codeEntered = false;
                }
            }
            //if correct pin entered the barier in game is taken down
            if (codeEntered)
            {
                barrier.SetActive(false);
            }

            else
            {
                for(int i = 0; i < codeNumbers.Length; i++)
                {
                    numbers[i].SetText(" ");
                }
                numbersEnteredCount = 0;
            }
        }
    }
}
