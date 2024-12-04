using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();   
    }

    // 무한 맵 구현
    // 플레이어가 일정 영역 밖으로 나갔을 때 맵도 따라서 이동
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;
        Vector3 playerDir = GameManager.instance.player.inputVec;


        // case "Enemy": 구문은 무한맵 특성상 한 방향으로 도망쳐 화면 밖으로 몬스터가 사라졌을 때 반대편으로 나오게하는 로직
        // 우리가 만드는 게임에는 필요 없을 듯 하기에 추후 수정 필요
        switch (transform.tag)
        {
            case "Ground":
                float dirX = playerPos.x - myPos.x;
                float dirY = playerPos.y - myPos.y;
                float diffX = Mathf.Abs(dirX);
                float diffY = Mathf.Abs(dirY);

                dirX = dirX > 0 ? 1 : -1;
                dirY = dirY > 0 ? 1 : -1;

                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;

            case "Enemy":
                if(coll.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-3, 3), 0);
                    transform.Translate(ran + dist * 2);
                }
                break;
        }

    }

}
