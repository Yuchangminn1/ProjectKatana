using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WG_PlayerSlash : MonoBehaviour
{
    int Frame = 0;

    //인스펙터에서 이펙트 넣어줌
    protected GameObject slashHitEffect;
    protected GameObject ParryEffect;
    //생성될 이펙트
    GameObject Hit_Clone;


    bool isHit, isHitBullet;

    private void Awake()
    {
        //오브젝트 생명주기상 Awake, Start가 Update보다 일찍 일어나기 때문에
        //이 스크립트에서 각도 받으면 시작할때 각도 변하면서 나오는건 구현이 불가능함
        //InputManager에서 각도값 Update에서 갱신되는거 사용
        transform.rotation = Quaternion.Euler(0, 0, WG_InputManager.instance.playerLookingCursorAngle);
        slashHitEffect = WG_FXManager.instance.playerSlashHitEffect.slashHitEffect;
        ParryEffect = WG_FXManager.instance.playerSlashHitEffect.ParryEffect;

    }

    private void Start()
    {
        isHit = false;
        isHitBullet = false;

        if (!WG_RecordManager.instance.Player_rewind.isRewinding)
            WG_SoundManager.instance.PlayEffectSound("Sound_Player_Slash" + Random.Range(1, 4));
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        //플레이어 AnimationTrigger에서 매니저 접근해서 삭제시켜봤는데
        //상태 여러개 동시에 변할때 삭제 안되는 버그있어서 그냥 여기서 함
        Frame++;



        //회전할때 잔상 방지하려고 좀 빠르게 삭제하고
        //유니티에서 slash 애니메이션 마지막을 12프레임에서 10프레임으로 옮겼음
        if (Frame >= 10)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //같은 오브젝트가 리스트에 여러번 들어가는것 방지
        if (collision.gameObject.CompareTag("Enemy") && !isHit
            && !WG_FXManager.instance.playerSlashEffect.alreadyHitEnemy.Contains(collision.gameObject))
        {
            WG_FXManager.instance.cameraEffect.ShakeTimer = 0f;
            //콜라이더 연속으로 체크하는거 방지
            isHit = true;

            //공격 범위에 trigger한 모든 적들을 List에 넣어줌
            WG_FXManager.instance.playerSlashEffect.alreadyHitEnemy.Add(collision.gameObject);

            SetClosestEnemy();

            if (WG_FXManager.instance.playerSlashEffect.ClosestEnemyInList != null
                && collision.gameObject == WG_FXManager.instance.playerSlashEffect.ClosestEnemyInList)
            {

                OntriggerExcute();
                CameraShakeEffect();

            }
        }

        if (collision.gameObject.CompareTag("EnemyBullet") && !isHitBullet)
        {
            collision.gameObject.tag = "PlayerBullet";

            collision.gameObject.AddComponent<WG_ParriedBullet>();

            if (ParryEffect != null)
            {
                ParryEffect.transform.position = collision.gameObject.transform.position;
                ParryEffect.SetActive(true);
            }
        }
    }

    protected virtual void CameraShakeEffect()
    {
        if (!WG_FXManager.instance.cameraEffect.isShaking)
        {
            //코루틴 끝나는건 CameraEffect쪽에 yield넣어줌
            WG_FXManager.instance.cameraEffect.StartCoroutine("HitShake");

            WG_FXManager.instance.playerSlashEffect.TimeStop();
        }
    }

    private GameObject SetClosestEnemy()
    {

        float closestDistance = Mathf.Infinity;
        Vector2 playerPosition = WG_PlayerManager.instance.player.transform.position;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in WG_FXManager.instance.playerSlashEffect.alreadyHitEnemy)
        {
            float DistanceBetweenPlayerAndEnemy = Vector2.Distance(playerPosition,
                enemy.transform.position);

            //무한보다 작으면
            if (DistanceBetweenPlayerAndEnemy < closestDistance)
            {
                //맨 처음은 무한보다 적으니 아무 enimies의 거리나 들어올거고
                //그 다음 closestDistance이 들어간 거리가 되고
                //그거보다 작은거 있으면 넣고 하면서 반복함
                //이제 마지막까지 순회하면서 점점 작아지다가 더이상 작아지지 못하는 거리가
                //가장 가까운 거리임
                closestDistance = DistanceBetweenPlayerAndEnemy;
                closestEnemy = enemy;
            }
        }
        WG_FXManager.instance.playerSlashEffect.ClosestEnemyInList = closestEnemy;
        return closestEnemy;
    }


    protected virtual void OntriggerExcute()
    {
        Hit_Clone
        = Instantiate(slashHitEffect,
          WG_FXManager.instance.playerSlashEffect.ClosestEnemyInList.transform.position,
          Quaternion.identity, WG_FXManager.instance.transform);

        Hit_Clone.transform.rotation = transform.rotation;

        WG_FXManager.instance.playerSlashEffect.alreadyHitEnemy.Clear();
    }
}
