using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class User : MonoBehaviour
{

    public float userSpeed;
    public float jumpPower;
    public SoundManager soundManager;
    public TutorialFieldManager fieldManager;
    public UIManager uiManager;

    public List<GameObject> icons = new List<GameObject>();

    public GameObject top;
    public GameObject bottom;
    public GameObject shoes;

    private bool jumpOK=true; 
    private bool slow = false;
    
    //현재 입고있는 옷
    private GameObject cur_top;
    private GameObject cur_bottom;
    private GameObject cur_shoes;
    
    private GameObject cur_top_bg;
    private GameObject cur_bottom_bg;
    private GameObject cur_shoes_bg;



    public bool catCollision = false;

    
    // Use this for initialization
    void Start(){
        Time.timeScale = 1f; //시간은 정상적으로 흐른다	
        
        //옷을 입는다
        wearCloth();
    }

    
    // Update is called once per frame
    void Update(){
        transform.Translate(Vector3.right * userSpeed * Time.deltaTime);
    }
    

    //점프
    public void Jump() {
        if (jumpOK) {
            jumpOK = false;
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpPower);
            
            fieldManager.GetComponent<TutorialFieldManager>().bgJumpEffect(); //점프할때 배경효과
            
            cur_top.GetComponent<Animator>().SetTrigger("jump_t");
            cur_top_bg.GetComponent<Animator>().SetTrigger("jump_t");
            
            cur_bottom.GetComponent<Animator>().SetTrigger("jump_t");
            cur_bottom_bg.GetComponent<Animator>().SetTrigger("jump_t");
            
            cur_shoes.GetComponent<Animator>().SetTrigger("jump_t");
            cur_shoes_bg.GetComponent<Animator>().SetTrigger("jump_t");
            
            StartCoroutine("JumpCheck", 0.5f);
        }
    }
    
    
    IEnumerator JumpCheck(float delayTime) {
        yield return new WaitForSeconds(delayTime); //표시시간
        soundManager.footPlay(); //효과음(발소리)
        jumpOK = true;
    }
    
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        //고양이는 tag가 cat, 첫글자가 i(init,초성)인지 c(choice,선택)인지로 구분
        if(col.tag.Equals("cat")) colCat(col);

        //음표
        if (col.tag.Equals("score")){
            string type = col.name.Substring(0, 1);

            //어떤 것과 충돌했는지 판단(l:라인, n:음표, c:collidar)
            if (type.Equals("l")) colLine(col);
            else if (type.Equals("n")) colNote(col);
        }
    }


    public void startShowIcon(int num) {
        StartCoroutine(showIcon(num, 4.5f));
    }
    
    IEnumerator showIcon(int num, float delayTime) {
        icons[num].GetComponent<Animator>().SetBool("correct_b", true);
        yield return new WaitForSeconds(delayTime);
        icons[num].GetComponent<Animator>().SetBool("correct_b", false);
        yield return new WaitForSeconds(0.1f);
    }
    
    
    
    //야옹충돌
    private void colCat(Collider2D col){
        icons[0].GetComponent<Animator>().SetBool("cat_b", true); //고앙이아이콘
        catCollision = true; //야옹충돌(이걸 켜면 고양이 터치가 가능해짐)   
        fieldManager.pauseMove();
        soundManager.catPlay();
    }

    
    //음표충돌
    private void colNote(Collider2D col){
        col.GetComponent<Animator>().SetTrigger("Die_t");
        soundManager.notePlay(); //효과음
        uiManager.setScoreText();
        uiManager.raiseScore(1); //캐릭터 위에 오버랩되는 효과
        GlobalScript.modifyScore(1);
    }


    //라인충돌
    private void colLine(Collider2D col){
        col.GetComponent<Animator>().SetTrigger("Die_t");
        uiManager.raiseScore(1); //캐릭터 위에 오버랩되는 효과
        GlobalScript.modifyScore(1);
        uiManager.setScoreText();
    }

    
    //무슨 옷을 입고있는지 찾아서 입힌다
    void wearCloth() {
        string item_code;

        item_code = GlobalScript.getTop().ToString();
        cur_top = top.transform.Find(item_code).gameObject;
        cur_top.SetActive(true);
        cur_top_bg = top.transform.Find(item_code+"_b").gameObject;
        cur_top_bg.SetActive(true);

        item_code = GlobalScript.getBottom().ToString();
        cur_bottom = bottom.transform.Find(item_code).gameObject;
        cur_bottom.SetActive(true);
        cur_bottom_bg = bottom.transform.Find(item_code+"_b").gameObject;
        cur_bottom_bg.SetActive(true);
 
        item_code = GlobalScript.getShoes().ToString();
        cur_shoes = shoes.transform.Find(item_code).gameObject;
        cur_shoes.SetActive(true);
        cur_shoes_bg = shoes.transform.Find(item_code+"_b").gameObject;
        cur_shoes_bg.SetActive(true);
    }
}
