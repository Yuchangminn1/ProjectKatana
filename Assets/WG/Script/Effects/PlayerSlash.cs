using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlash : MonoBehaviour
{
    int Frame = 0;
    private void Awake()
    {
        //오브젝트 생명주기상 Awake, Start가 Update보다 일찍 일어나기 때문에
        //이 스크립트에서 각도 받으면 시작할때 각도 변하면서 나오는건 구현이 불가능함
        //InputManager에서 각도값 Update에서 갱신되는거 사용
        transform.rotation = Quaternion.Euler(0, 0, InputManager.instance.playerLookingCursorAngle);
    }

    private void FixedUpdate()
    {
        //플레이어 AnimationTrigger에서 매니저 접근해서 삭제시켜봤는데
        //상태 여러개 동시에 변할때 삭제 안되는 버그있어서 그냥 여기서 함
        Frame++;

        //회전할때 잔상 방지하려고 좀 빠르게 삭제하고
        //유니티에서 slash 애니메이션 마지막을 12프레임에서 10프레임으로 옮겼음
        if (Frame >= 10) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
