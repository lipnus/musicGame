using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{

    public float userSpeed;
    public float jumpPower;
    public GameManager gameManager;
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
        //어떤 것과 충돌했는지 판단(l:라인, n:음표, c:collidar)
        string type = col.transform.name.Substring(0, 1);
        GameObject obj = null;

        Debug.Log("충돌: " + col + " / " + col.transform.name + " type: " + type);

        if (type.Equals("l")){
            colLine(col);
        }else if(type.Equals("n")){
            colNote(col);
        }else if(type.Equals("c")){

        }

    }

    //음표충돌
    private void colNote(Collider2D col){
        GameObject noteObj=null;

        //부딪힌 음표를 찾음
        for (int i = 0; i < note.Count; i++){
            if (col.transform.name == note[i].name) noteObj = note[i];
        }

        noteObj.GetComponent<Animator>().SetTrigger("Die_t");
        soundManager.notePlay();
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
