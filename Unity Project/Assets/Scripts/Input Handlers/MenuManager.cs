using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class MenuManager : InputHandler_Generic
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject[] menuScreens;    
    [SerializeField] private GameObject main_firstSelectedButton;
    [SerializeField] private GameObject settings_firstSelectedButton;
    [SerializeField] private GameObject settingsButton;

    void Start()
    {
        OSCSetup(); // (See note in InputHandler_Generic about OSCSetup)
        GoToPage(0);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(main_firstSelectedButton);
    }
    
    public void GoToPage(int index) {
        for (int i = 0; i < menuScreens.Length; i ++) {
            menuScreens[i].SetActive(false);
        }
        menuScreens[index].SetActive(true);

        // Set the selected UI object (for keyboard and/or drum-based navigation)
        EventSystem.current.SetSelectedGameObject(null);
        switch (index) {
            case 0:
                EventSystem.current.SetSelectedGameObject(settingsButton);
                break;
            case 1:
                EventSystem.current.SetSelectedGameObject(settings_firstSelectedButton);
                break;
            
            default:
                break;
        }
    }

    protected override void InputHandler(string command, float velocity, int note) {
        if (command.Equals("play")) {
            Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            TMP_InputField field = EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>();
            //Debug.Log(velocity);
            //Debug.Log(button);
            if (velocity > 0.5f) {
                // The user wants to input a button press with the drum
                if (button != null) {
                    button.onClick.Invoke();
                }
                if (field != null) {
                    field.onSubmit.Invoke(field.text);
                }
            }
            else {
                // The user wants to advance to the next item with the drum
                GameObject nextSelected = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnRight().gameObject;
                //Debug.Log($"nextSelected: {nextSelected}");
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(nextSelected);
            }
        }
    }

    public void GoToScene(string sceneName) {
        SceneChanger.GoToScene(sceneName);
    }
}
