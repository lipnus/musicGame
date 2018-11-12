using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class User : MonoBehaviour
{

    public float userSpeed;
    public float jumpPower;
    public SoundManager soundManager;
    public TutorialFieldManager TutorialFieldManager;
    private bool isJumped;
    

    private int groundPosition; //점프하기 전의 y좌표(제일앞 3자리만 비교)

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


    //Collision박스 영역에서 벗어남
    private void OnTriggerExit2D(Collider2D col) {

//        if (col.tag.Equals("cat")){
//            GameObject.Find("cat_icon").GetComponent<Animator>().SetBool("cat_b", false);
//            
//            TutorialFieldManager.savePosition(); //현재 레이어(유저포함)들의 위치를 전역에 기억
//            string stageType = col.name.Substring(0, 1);
//
//            if (stageType.Equals("i")) {
//                SceneManager.LoadScene("Quiz_initial");
//            }
//            else if (stageType.Equals("c")) {
//                SceneManager.LoadScene("Quiz_choice");
//            }
//        }
    }
    
   
    

    //야옹충돌
    private void colCat(Collider2D col){

        GameObject.Find("cat_icon").GetComponent<Animator>().SetBool("cat_b", true);
        catCollision = true; //야옹충돌(이걸 켜면 고양이 터치가 가능해짐)
        
        //화면전환효과
        TutorialFieldManager.pauseMove();
        soundManager.catPlay();
    }

    
    //음표충돌
    private void colNote(Collider2D col){
        col.GetComponent<Animator>().SetTrigger("Die_t");
        soundManager.notePlay(); //효과음
        GlobalScript.modifyScore(1);
        GameObject.Find("UIManager").GetComponent<UIManager>().setScoreText();

    }


    //라인충돌
    private void colLine(Collider2D col){
        col.GetComponent<Animator>().SetTrigger("Die_t");
        GlobalScript.modifyScore(1);
        GameObject.Find("UIManager").GetComponent<UIManager>().setScoreText();

    }

    
    //무슨 옷을 입고있는지 찾아서 입힌다
    void wearCloth() {
        string item_code;

        item_code = GlobalScript.getTop().ToString();
        cur_top = GameObject.Find("Top").transform.Find(item_code).gameObject;
        cur_top.SetActive(true);
        cur_top_bg = GameObject.Find("Top").transform.Find(item_code+"_b").gameObject;
        cur_top_bg.SetActive(true);

        item_code = GlobalScript.getBottom().ToString();
        cur_bottom = GameObject.Find("Bottom").transform.Find(item_code).gameObject;
        cur_bottom.SetActive(true);
        cur_bottom_bg = GameObject.Find("Bottom").transform.Find(item_code+"_b").gameObject;
        cur_bottom_bg.SetActive(true);
 
        item_code = GlobalScript.getShoes().ToString();
        cur_shoes = GameObject.Find("Shoes").transform.Find(item_code).gameObject;
        cur_shoes.SetActive(true);
        cur_shoes_bg = GameObject.Find("Shoes").transform.Find(item_code+"_b").gameObject;
        cur_shoes_bg.SetActive(true);
    }
}
