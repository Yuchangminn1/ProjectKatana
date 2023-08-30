using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSlash : MonoBehaviour
{
    int Frame = 0;
    GameObject slashHitEffect;
    GameObject Hit_Clone;

    bool isHit, timerStop;
    float shakeTimer = 0f;

    private void Awake()
    {
        //오브젝트 생명주기상 Awake, Start가 Update보다 일찍 일어나기 때문에
        //이 스크립트에서 각도 받으면 시작할때 각도 변하면서 나오는건 구현이 불가능함
        //InputManager에서 각도값 Update에서 갱신되는거 사용
        transform.rotation = Quaternion.Euler(0, 0, InputManager.instance.playerLookingCursorAngle);
        slashHitEffect = FXManager.instance.playerSlashHitEffect.slashHitEffect;

    }

    private void Start()
    {
        isHit = false;
    }

    private void FixedUpdate()
    {
        shakeTimer += Time.fixedDeltaTime;
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
            && !FXManager.instance.playerSlashEffect.alreadyHitEnemy.Contains(collision.gameObject))
        {
            shakeTimer = 0f;
            //콜라이더 연속으로 체크하는거 방지
            isHit = true;

            //공격 범위에 trigger한 모든 적들을 List에 넣어줌
            FXManager.instance.playerSlashEffect.alreadyHitEnemy.Add(collision.gameObject);

            SetClosestEnemy();

            if (FXManager.instance.playerSlashEffect.ClosestEnemyInList != null
                && collision.gameObject == FXManager.instance.playerSlashEffect.ClosestEnemyInList)
            {

                OntriggerExcute();

                if (shakeTimer <= 0.25f)
                {
                    FXManager.instance.cameraEffect.StartCoroutine("HitShake");
                }
            }
        }
    }
    private GameObject SetClosestEnemy()
    {

        float closestDistance = Mathf.Infinity;
        Vector2 playerPosition = PlayerManager.instance.player.transform.position;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in FXManager.instance.playerSlashEffect.alreadyHitEnemy)
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
        FXManager.instance.playerSlashEffect.ClosestEnemyInList = closestEnemy;
        return closestEnemy;
    }


    void OntriggerExcute()
    {
        Hit_Clone
        = Instantiate(slashHitEffect,
          FXManager.instance.playerSlashEffect.ClosestEnemyInList.transform.position,
          Quaternion.identity, FXManager.instance.transform);

        Hit_Clone.transform.rotation = transform.rotation;

        FXManager.instance.playerSlashEffect.alreadyHitEnemy.Clear();
    }


}
