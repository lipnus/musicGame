using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class User : MonoBehaviour
{

    public float userSpeed;
    public float jumpPower;
    public GameManager gameManager;
    public BackgroundManager backgroudManager;
    public SoundManager soundManager;
    

    private bool catCollision;
    

    // Use this for initialization
    void Start(){
        Time.timeScale = 1f; //시간은 정상적으로 흐른다	
    }

    // Update is called once per frame
    void Update(){
        transform.Translate(Vector3.right * userSpeed * Time.deltaTime);
    }

    //점프
    public void Jump(){ 
        GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpPower);
        GetComponent<Animator>().SetTrigger("jump_t");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("충돌: " + col + "태그: " + col.tag + " 이름: " + col.name);
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

            Vector3 mPosition = gameObject.transform.position;
            mPosition.x += 3;
            GlobalScript.userPosition = mPosition; 
            SceneManager.LoadScene("Quiz_initial");
        }
    }
 
    

    //야옹충돌
    private void colCat(Collider2D col){

        Debug.Log("애옹");
        GameObject.Find("cat_icon").GetComponent<Animator>().SetBool("cat_b", true);

        //주인공 속도감소
//        backgroudManager.userSpeedControl(0.8f);
        
        //화면전환효과
        GameObject.Find("BackgroundManager").GetComponent<BackgroundManager>().catEffect();

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
