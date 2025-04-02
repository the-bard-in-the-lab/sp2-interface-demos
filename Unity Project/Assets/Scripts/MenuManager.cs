using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class MenuManager : InputHandler_Generic
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject[] menuScreens;    
    [SerializeField] private GameObject main_firstSelectedButton;
    [SerializeField] private GameObject settings_firstSelectedButton;
    [SerializeField] private GameObject settingsButton;

    void Start()
    {
        OSCSetup();
        GoToPage(0);
    }
    
    public void GoToPage(int index) {
        for (int i = 0; i < menuScreens.Length; i ++) {
            menuScreens[i].SetActive(false);
        }
        menuScreens[index].SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        switch (index){
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
            //Debug.Log("Message heard by Menu Manager");
            Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            Debug.Log(velocity);
            Debug.Log(button);
            if (velocity > 0.5f) {
                if (button != null) {
                    button.onClick.Invoke();
                }
            }
            else {
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
