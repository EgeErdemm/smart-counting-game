using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private GameObject bombTutorialPanel;


    public void closeBombTutorialPanel()
    {
        bombTutorialPanel.transform.DOScale(new Vector3(0f, 0f, 0f), 1f);
    }

}
