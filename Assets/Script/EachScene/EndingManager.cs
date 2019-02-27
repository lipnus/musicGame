using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour {

    public List<GameObject> group;
    public Text clearCount;
    public SoundManager soundManager;
    public Text userName;
    public Text playResultText;
    public GameObject cancelText;
    
    // Start is called before the first frame update
    void Start() {

        userName.text = Utils.getNickname();
        
        PlayData playData = Utils.getPlayData();
        playResultText.text = 
            "Correct: " + playData.correct 
                        + "		Wrong:" + playData.wrong
                        + "		Point:" + playData.point;
        
        StartCoroutine(Scenario());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator Scenario() {

        clearCount.text = Utils.getGameClear()+"";

        yield return new WaitForSeconds(0.5f);
        
        Utils.modifyGameClear(1);
        clearCount.text = Utils.getGameClear()+"";
        soundManager.okPlay();
        yield return new WaitForSeconds(4);

        hideAllGroup();
        group[1].active = true;
        cancelText.active = true;        
        yield return new WaitForSeconds(5);
        
        //이름
        hideAllGroup();
        group[2].active = true;
        yield return new WaitForSeconds(5);
        
        hideAllGroup();
        group[3].active = true;
        yield return new WaitForSeconds(5);
        
        hideAllGroup();
        group[4].active = true;
        yield return new WaitForSeconds(5);
        
        hideAllGroup();
        group[5].active = true;
        yield return new WaitForSeconds(5);
        
        hideAllGroup();
        group[6].active = true;
        yield return new WaitForSeconds(3);
        
        
        
        yield return new WaitForSeconds(20f);
        SceneManager.LoadSceneAsync("MainScene");
    }



    
    void hideAllGroup() {
        for (int i = 0; i < @group.Count; i++) {
            @group[i].active = false;
        }
    }


    public void onClick_cancel() {
        SceneManager.LoadSceneAsync("MainScene");
    }
}
