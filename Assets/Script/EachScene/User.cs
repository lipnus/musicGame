using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class User : MonoBehaviour
{
    
    //캐릭터 움직임
    public float userSpeed;
    public float jumpPower;
    private bool jumpOK=true; 
    private bool slow = false;
    
    //Manager
    public SoundManager soundManager;
    public StageFieldManager fieldManager;
    public UIManager uiManager;
    
    //머리 위의 아이콘
    public List<GameObject> icons = new List<GameObject>();

    //착용할 수 있는 옷
    public List<GameObject> tops = new List<GameObject>();
    public List<GameObject> bottoms = new List<GameObject>();
    public List<GameObject> shoes = new List<GameObject>();
    
    //착용할 수 있는 악세사리
    public List<Image> accesories = new List<Image>();
    
    //현재 입고있는 옷
    private GameObject cur_top;
    private GameObject cur_bottom;
    private GameObject cur_shoes;
    private GameObject cur_top_bg;
    private GameObject cur_bottom_bg;
    private GameObject cur_shoes_bg;

    public GameObject particlePrefeb;
    
    public bool catCollision = false;

    
    void Start(){
        
        //시간은 정상적으로 흐른다	
        Time.timeScale = 1f; 
        
        //옷입기
        wearCloth();
        
        //악세사리(패시브아이템) 착용
        wearAccesory();
    }

    
    void Update(){
        transform.Translate(Vector3.right * userSpeed * Time.deltaTime);
    }
    

    //점프
    public void Jump() {
        if (jumpOK) {
            jumpOK = false;
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpPower);
            
            fieldManager.GetComponent<StageFieldManager>().bgJumpEffect(); //점프할때 배경효과
            
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
        soundManager.jumpPlay(); //효과음(착지)
        jumpOK = true;
    }
    
    
    private void OnTriggerEnter2D(Collider2D col) {
       
        //고양이
        if(col.tag.Equals("cat")) colCat(col);

        //음표
        if (col.tag.Equals("score")) colNote(col);
        
        //지하철
        if (col.tag.Equals("subway")) colSubway(col);
        
        //대왕고양이
        if(col.tag.Equals("boss")) colBoss();
    }
    

    //해당하는 번호(num)에 해당하는 아이콘 출력
    public void startShowIcon(int num) {
        StartCoroutine(showIcon(num, 4.5f));
    }
    
    IEnumerator showIcon(int num, float delayTime) {
        icons[num].GetComponent<Animator>().SetBool("icon_b", true);
        yield return new WaitForSeconds(delayTime);
        icons[num].GetComponent<Animator>().SetBool("icon_b", false);
        yield return new WaitForSeconds(0.1f);
    }
    
    
  
    //야옹충돌
    private void colCat(Collider2D col){
        icons[0].GetComponent<Animator>().SetBool("icon_b", true); //고앙이아이콘
        catCollision = true; //야옹충돌(이걸 켜면 고양이 터치가 가능해짐)   
        fieldManager.pauseMove();
        soundManager.catPlay();
    }


    //대왕고양이
    private void colBoss() {
        int bossLife = Utils.getPlayData().bossLife;
        
        if (bossLife == 3) {
            icons[4].GetComponent<Animator>().SetBool("icon_b", true); //왕고양이 아이콘
            Utils.modifyBossCatLife(-1);
        }else if (bossLife == 2) {
            icons[5].GetComponent<Animator>().SetBool("icon_b", true); //중간고양이 아이콘
            Utils.modifyBossCatLife(-1);
        }else if (bossLife == 1) {
            icons[0].GetComponent<Animator>().SetBool("icon_b", true); //작은고양이 아이콘
            Utils.modifyBossCatLife(-1);
        }
        
        catCollision = true; //야옹충돌(이걸 켜면 고양이 터치가 가능해짐)   
        fieldManager.pauseMove();
        soundManager.catPlay();
    }

    
    //음표충돌
    private void colNote(Collider2D col){
        
        NoteObject noteObj = col.GetComponent<NoteObject>();
        
        col.GetComponent<Animator>().SetTrigger("Die_t");
        soundManager.notePlay(); //효과음
        
        uiManager.raisePoint( noteObj.score ); //캐릭터 위에 오버랩되는 효과
        Utils.modifyPoint( noteObj.score );
        uiManager.setPointText();


        if (noteObj.noteType == NoteObject.NoteType.Note) {
            StartCoroutine(ParticleEffect(col, 1));

        }
    }


    //지하철과 충돌
    private void colSubway(Collider2D col) {
        
        icons[3].GetComponent<Animator>().SetBool("icon_b", true); //지하철
        SubwayObject subwayObj = col.GetComponent<SubwayObject>();
        subwayObj.startSubway();

    }
    
    
    //음표 먹었을 때 특수효과
    IEnumerator ParticleEffect(Collider2D col, float delayTime) {
        GameObject effect = Instantiate(particlePrefeb, col.transform.position, col.transform.rotation);
        yield return new WaitForSeconds(delayTime);
        Destroy(effect);
    }




    
    //무슨 옷을 입고있는지 찾아서 입힌다
    public void wearCloth() {
        string item_code;

        item_code = Utils.getTop().ToString();
        for (int i = 0; i < tops.Count(); i++) {
            if (tops[i].transform.name.Equals(item_code)) {
                cur_top = tops[i].gameObject;
                cur_top.SetActive(true);
            }
            
            if (tops[i].transform.name.Equals(item_code + "_b")){
                cur_top_bg = tops[i].gameObject;
                cur_top_bg.SetActive(true);
            }
            
            if(tops[i].gameObject.active && !tops[i].transform.name.Equals(item_code) && !tops[i].transform.name.Equals(item_code + "_b"))
            {
                tops[i].gameObject.SetActive(false);
            }

        }
        
        
        item_code = Utils.getBottom().ToString();
        for (int i = 0; i < bottoms.Count(); i++) {
            if (bottoms[i].transform.name.Equals(item_code)){
                cur_bottom = bottoms[i].gameObject;
                cur_bottom.SetActive(true);
            }

            if (bottoms[i].transform.name.Equals(item_code + "_b")) {
                cur_bottom_bg = bottoms[i].gameObject;
                cur_bottom_bg.SetActive(true);
            }
            
            if(bottoms[i].gameObject.active && !bottoms[i].transform.name.Equals(item_code) && !bottoms[i].transform.name.Equals(item_code + "_b"))
            {
                bottoms[i].gameObject.SetActive(false);
            }
        }
        
        
        item_code = Utils.getShoes().ToString();
        for (int i = 0; i < shoes.Count(); i++) {
            if (shoes[i].transform.name.Equals(item_code)) {
                cur_shoes = shoes[i].gameObject;
                cur_shoes.SetActive(true);
            }

            if (shoes[i].transform.name.Equals(item_code + "_b")) {
                cur_shoes_bg = shoes[i].gameObject;
                cur_shoes_bg.SetActive(true);
            }
            
            if(shoes[i].gameObject.active && !shoes[i].transform.name.Equals(item_code) && !shoes[i].transform.name.Equals(item_code + "_b"))
            {
                shoes[i].gameObject.SetActive(false);
            }
        }

    }

    
    //무슨 악세사리(패시브아이템)을 착용하고 있는지 찾아서 표시한다
    public void wearAccesory() {

        if (accesories[0] == null) return;
        const float INACTIVE = 0.2f;
        
        //아이팟
        if (Utils.isHaveItem(400)) accesories[0].color = new Color (1f, 1f, 1f, 1f);
        else accesories[0].color = new Color (1f, 1f, 1f, INACTIVE);
        
        //샤넬 No.5
        if (Utils.isHaveItem(401)) accesories[1].color = new Color (1f, 1f, 1f, 1f);
        else accesories[1].color = new Color (1f, 1f, 1f, INACTIVE);
        
        //스벅 텀블러
        if (Utils.isHaveItem(402)) accesories[2].color = new Color (1f, 1f, 1f, 1f);
        else accesories[2].color = new Color (1f, 1f, 1f, INACTIVE);
        
        //정관장 홍삼정
        if (Utils.isHaveItem(403)) accesories[3].color = new Color (1f, 1f, 1f, 1f);
        else accesories[3].color = new Color (1f, 1f, 1f, INACTIVE);
    }
}
