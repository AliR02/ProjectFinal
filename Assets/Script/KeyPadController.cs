using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class KeyPadController: MonoBehaviour
{
    public List<int> correctPassword = new List<int>();
    private List<int> inputPasswordList = new List<int>();
    [SerializeField] private TMP_InputField codeDisplay;
    [SerializeField] private float resetTime = 2f;
    [SerializeField] private string successText;
    [SerializeField] private Animator Door;
    [Space(5f)]
    [Header("Keypad Entry Events")]
    public UnityEvent onCorrectPassword;
    public UnityEvent onIncorrectPassword;

    public bool allowMultipleActivations = false;
    private bool hasUsedCorrectCode = false;
    public bool HasUsedCorrectCode { get { return hasUsedCorrectCode; } }
    
    public void UserNumberEntry(int selectedNum)
    {
        if(inputPasswordList.Count >= 4) 
        {
            return;
        }
        inputPasswordList.Add(selectedNum);

        UpdateDisplay();

        if(inputPasswordList.Count >= 4)
        {
            CheckPassword();
        }

    }

    private void CheckPassword()
    {
        for(int i=0; i< correctPassword.Count; i++)
        {
            if(inputPasswordList[i] != correctPassword[i])
            {
                IncorrectPassword();
                return;
            }

        }
        correctPasswordGiven();
    }
    private void correctPasswordGiven()
    {
        if (allowMultipleActivations)
        {
            onCorrectPassword.Invoke();
            StartCoroutine(ResetKeyCode());
        }
        else if(!allowMultipleActivations && !hasUsedCorrectCode)
        {
            onCorrectPassword.Invoke();
            hasUsedCorrectCode = true;
            codeDisplay.text = successText;
            Door.SetBool("Open",true);
            StartCoroutine("StopDoor");

        }
    }
    

    private void IncorrectPassword()
    {
        onIncorrectPassword.Invoke();
        StartCoroutine(ResetKeyCode());
    }

    private void UpdateDisplay()
    {
        codeDisplay.text = null;
        for(int i=0; i< inputPasswordList.Count; i++)
        {
            codeDisplay.text += inputPasswordList[i];
        }
    }

    public void DeleteEntry()
    {
        if (inputPasswordList.Count <= 0)
            return;
        
        var listposition = inputPasswordList.Count - 1;
        inputPasswordList.RemoveAt(listposition);
        UpdateDisplay();
    }
    
    IEnumerator ResetKeyCode()
    {
        yield return new WaitForSeconds(resetTime);
        inputPasswordList.Clear();
        codeDisplay.text = "Enter Code...";
    }
    IEnumerator StopDoor()
    {
        yield return new WaitForSeconds(0.5f);
        Door.SetBool("Open", false);
        Door.enabled = false;
    }


}
