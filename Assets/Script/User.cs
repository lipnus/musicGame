using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{

    public float userSpeed;
    public float jumpPower;
    public GameManager gameManager;
    public BackgroundManager backgroudManager;
    public SoundManager soundManager;

    public List<GameObject> note = new List<GameObject>();
    public List<GameObject> line = new List<GameObject>();


    // Use this for initialization
    void Start(){
        Time.timeScale = 1f; //시간은 정상적으로 흐른다	
    }

    // Update is called once per frame
    void Update(){
        transform.Translate(Vector3.right * userSpeed * Time.deltaTime);
    }

    public void Jump(){ 
        GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpPower);
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

    //야옹충돌
    private void colCat(Collider2D col){

        //부딪힌 야옹이 찾음
        GameObject catObj = GameObject.Find(col.name).GetComponent<GameObject>();

        //주인공 속도감소
        backgroudManager.userSpeedControl(0.7f);

    }

    //음표충돌
    private void colNote(Collider2D col){
        GameObject noteObj=null;

        //부딪힌 음표를 찾음
        for (int i = 0; i < note.Count; i++){
            if (col.transform.name == note[i].name) noteObj = note[i];
        }

        noteObj.GetComponent<Animator>().SetTrigger("Die_t");
        soundManager.notePlay(); //효과음
    }


    //라인충돌
    private void colLine(Collider2D col){
        GameObject lineObj = null;

        //부딪힌 라인을 찾음
        for (int i = 0; i < line.Count; i++){
            if (col.transform.name == line[i].name) lineObj = line[i];
        }

        lineObj.GetComponent<Animator>().SetTrigger("Die_t");
    }
}
