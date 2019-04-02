using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieText{
    
    private List<string> list = new List<string>();

    public string getDieText() {
        addText();
        int pick = Random.Range(0, list.Count-1);
        return list[pick];
    }
    
    private void addText() {
       
        list.Add("누구와 닮았다는 이야기는 곧 신랄한 비판과도 같다.\n내 스타일을, 음악을 모욕하는 언사다.\n\nSnoop Dogg");
        list.Add("가사에 허황된 이야기를 쓰지 않는다.\n내가 하는 음악은 내 삶이 닮긴 결과물이다.\n\nScHoolboy Q");
        list.Add("돈까스 좋아하세요?\n\nSwings");
        list.Add("인간에게 가장 해로운 벌레는 '대충'\n\nYou know yunho");
        list.Add("한국 힙합 망해라!!\n\nMOMMY SON");
        list.Add("내 생에 최고의 순간은 아직 오지 않았다.\n\nFrank Sinatra");
        list.Add("그리고 내 노래의 고동은 이어진다.\n\nSalvatore Phillip Bono");
        list.Add("난 이 세상의 밑바닥이 아닌 밑받침.\n\nTablo");
        list.Add("언어가 끝나는 곳에서 음악은 시작된다.\n\nWolfgang Amadeus Mozart");
        list.Add("마음이 깨끗한 자만이 맛있는 음식을 요리할 수 있다.\n\nLudwig van Beethoven");
        list.Add("음악은 나의 생명이며, 나는 연주하기 위해서 살고 있다.\n\nLouis Armstrong");
        list.Add("모든 예술은 음악의 상태를 지향한다.\n\nSchopenhauer");
        list.Add("음악과 리듬은 영혼의 비밀 장소로 파고든다.\n\nPlato");
        list.Add("갈대의 나부낌에도 음악이 있다.\n시냇물의 흐름에도 음악이 있다.\n사람들이 귀를 가지고 있다면 모든 사물에서 음악을 들을 수 있다.\n\nG. Byron");
        list.Add("음악의 언어는 무한하다. 여기에는 모든 것이 들어 있고 모든 것을 설명할 수 있다.\n\nHonore de Balzac");
        list.Add("음악만이 세계어에서 번역할 필요가 없다.\n거기서는 혼이 혼에게 호소된다.\n\n아우에르바하");
        list.Add("세계는 이젠 음악을 받아들이지 않는다.\n왜냐하면 음악이 이미 세계를 받아들였기 때문이다.\n\nGeorg Simmel");
        list.Add("사람을 화나게 하는 방법은 두가지가 있다.\n첫번째는 말을 끝까지 하지 않는 것이고..\n\nAmumal Big Festival");
        list.Add("엔딩까지 함께 하는 방안을 진지하게 숙고했습니다만,\n아쉽게도 이번 플레이에서는 함께하기 어렵게 되었다는 말씀을 전하게 되었습니다.\n\n엔딩담당자");
        list.Add("비트와 밀당을 하는 나, 힙합밀당녀!\n\n육지담");
        list.Add("짬에서 나오는 바이브가 있을 거에요.\n\n원썬");
        list.Add("So you need to stop checking these bitches and keep your dick in.\nNiggas need to go back to the OG penitentiary days and start jacking off.\n\n2Pac");

        
    }
}
