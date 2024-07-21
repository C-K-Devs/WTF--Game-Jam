using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Laptop : MonoBehaviour, IInteractable
{
    public int actualPassword;
    public TMP_InputField passInput;
    public Button submitButton;
    public TMP_Text message;
    public GameObject note;
    public bool isOpen = false;
    public Transform laptopLid;
    public float laptopLidOpenTime = 0.5f;
    public float angleOfRotation;

    private Quaternion originalRotation;
    private Quaternion targetRotation;

    private void OnEnable()
    {
        UIManager.instance.ShowSubtitle("That’s why I made a hint for my password.", 5f, true);
        submitButton.onClick.AddListener(CheckPass);
    }

    private void Start()
    {
        originalRotation = laptopLid.rotation;
        targetRotation = Quaternion.Euler(originalRotation.eulerAngles + new Vector3(angleOfRotation, 0, 0));
    }

    public void Interact()
    {
        if (QuestManager.instance.wiresMatched)
        {
            if (!QuestManager.instance.mailChecked){
                if (!isOpen)
                {
                    isOpen = true;
                    StartCoroutine(OpenLaptop());
                }
                QuestManager.instance.canLook = false;
                QuestManager.instance.canMove = false;
                passInput.enabled = true;
                passInput.ActivateInputField();
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else{
            UIManager.instance.ShowSubtitle("I need some light first", 5f, true);
        }
        
    }

    private IEnumerator OpenLaptop()
    {
        float elapsedTime = 0f;
        while (elapsedTime < laptopLidOpenTime)
        {
            laptopLid.rotation = Quaternion.Lerp(originalRotation, targetRotation, elapsedTime / laptopLidOpenTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        laptopLid.rotation = targetRotation; // Ensure final rotation is exactly the target
    }

    public void CheckPass()
    {
        if (int.TryParse(passInput.text, out int inputPassword) && inputPassword == actualPassword)
        {
            ShowNote();
        }
        else
        {
            ShowWrongPassword();
        }
    }

    private void ShowNote()
    {
        if (note != null)
        {
            note.SetActive(true);
        }
        UIManager.instance.ShowSubtitle("Mummy said, Don’t forget to make your Bed.", 5f, true);
        QuestManager.instance.mailChecked = true;
        QuestManager.instance.StartQuest(2);
        UIManager.instance.StopShowSubtitle();
        LeaveLaptop();
    }

    private void ShowWrongPassword()
    {
        message.text = "Incorrect Password. Please try again!";
        message.gameObject.SetActive(true);
        StartCoroutine(HideMessageAfterDelay(3f));
    }

    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        message.gameObject.SetActive(false);
    }

    private IEnumerator HideUIElementsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        passInput.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);
        message.gameObject.SetActive(false);
    }

    public void LeaveLaptop(){
        QuestManager.instance.canLook = true;
        QuestManager.instance.canMove = true;
        passInput.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
