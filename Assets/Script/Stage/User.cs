using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class User : MonoBehaviour
{

    public float userSpeed;
    public float jumpPower;
    public SoundManager soundManager;
    private bool isJumped;
    

    private bool catCollision;
    private int groundPosition; //점프하기 전의 y좌표(제일앞 3자리만 비교)

    private bool jumpOK=true;
    private bool slow = false;
    

    // Use this for initialization
    void Start(){
        Time.timeScale = 1f; //시간은 정상적으로 흐른다	
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
            GetComponent<Animator>().SetTrigger("jump_t");
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
//        Debug.Log("충돌: " + col + "태그: " + col.tag + " 이름: " + col.name);
        if(col.tag.Equals("cat")) colCat(col);


        if (col.tag.Equals("score")){
            string type = col.name.Substring(0, 1);

            //어떤 것과 충돌했는지 판단(l:라인, n:음표, c:collidar)
            if (type.Equals("l")) colLine(col);
            else if (type.Equals("n")) colNote(col);
        }
    }


    private void OnTriggerExit2D(Collider2D col) {

        if (col.tag.Equals("cat")){
            GameObject.Find("cat_icon").GetComponent<Animator>().SetBool("cat_b", false);
            catCollision = false;
            
            GameObject.Find("FieldManager").GetComponent<FieldManager>().savePosition(); //현재 레이어(유저포함)들의 위치를 전역에 기억
            SceneManager.LoadScene("Quiz_initial");
        }
    }
 
    

    //야옹충돌
    private void colCat(Collider2D col){

//        Debug.Log("애옹");
        GameObject.Find("cat_icon").GetComponent<Animator>().SetBool("cat_b", true);
        
        //화면전환효과
        GameObject.Find("FieldManager").GetComponent<FieldManager>().catEffect();
        catCollision = true;
        soundManager.catPlay();
    }

    //음표충돌
    private void colNote(Collider2D col){
        col.GetComponent<Animator>().SetTrigger("Die_t");
        soundManager.notePlay(); //효과음
        GlobalScript.modifyScore(1);
    }


    //라인충돌
    private void colLine(Collider2D col){
        col.GetComponent<Animator>().SetTrigger("Die_t");
        GlobalScript.modifyScore(1);
    }
}
