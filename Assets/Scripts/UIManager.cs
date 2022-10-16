using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _bookUIObject;
    [SerializeField] GameObject _bookPanelObject;
    [SerializeField] GameObject _gameUIParentObject;
    [SerializeField] GameObject _mainMenuUIParentObject;

    bool _bookOpened = false;
    public void MainMenuButtonClicked()
    {
        PotionItemManager.Instance.HideAllPotions();
        _mainMenuUIParentObject.SetActive(true);
        _gameUIParentObject.SetActive(false);
    }

    public void BookButtonClicked()
    {
        _bookOpened = !_bookOpened;
        _bookUIObject.SetActive(!_bookOpened);
        _bookPanelObject.SetActive(_bookOpened);
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }

    public void PlayButtonClicked()
    {
        PotionItemManager.Instance.RevealAllPotions();
        _mainMenuUIParentObject.SetActive(false);
        _gameUIParentObject.SetActive(true);
    }
  
}
