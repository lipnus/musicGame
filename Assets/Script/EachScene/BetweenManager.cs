using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BetweenManager : MonoBehaviour {

    public Text stageText;

    public Text titleText;

    public Text subTitleText;
    
    
    void Start()
    {
        initText();
        StartCoroutine(changeScene(2f, Utils.getsceneNameStr() ));
    }
    
    
    IEnumerator changeScene(float delayTime, string sceneName) { 
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadSceneAsync( sceneName );
        
    }

    private void initText() {
        stageText.text = Utils.getStageStr();
        titleText.text = Utils.getTitleStr();
        subTitleText.text = Utils.getsubTitleStr();
    }

    
}
