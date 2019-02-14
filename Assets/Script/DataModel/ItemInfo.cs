 

using Boo.Lang;

public class ItemInfo { 
    private int perchase_type; //0:포인트로 구매, 1:캐쉬로 구매
    private int code; //아이템고유코드
    
    private string name; //제품명
    private int price; //가격
    private string shopping_category; //앱에 표시되는 가라 카테고리

    public ItemInfo(int perchase, int code, string name, int price, string shoppingCategory) {
        this.perchase_type = perchase;
        this.code = code;
        this.name = name;
        this.price = price;
        shopping_category = shoppingCategory;
    }
 

    public int Perchase {
        get { return perchase_type; }
    }

    public int Code {
        get { return code; }
    }

    public string Name {
        get { return name; }
    }

    public int Price {
        get { return price; }
    }

    public string ShoppingCategory {
        get { return shopping_category; }
    }
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