 

using Boo.Lang;

public class Quiz {
    public MusicInfo musicInfo;
    public int quiz_pk;
    public Choice[] choices = new Choice[3];
}
 
 
 
////문제
//let responseData = {
//music_pk: rows[0].music_pk, //데이터수집용
//quiz_pk: rows[0].quiz_pk, //데이터수집용
//title: rows[0].title,
//singer: rows[0].singer,
//path: rows[0].path,
//};
//responseData.choice = [];
//
////선택지
//for(let i=0; i<rows.length; i++){
//let choice_obj = {
//    choice: rows[i].choice_text,
//    truth: rows[i].truth,
//};
//responseData.choice.push(choice_obj);
//}
//res.json( responseData );