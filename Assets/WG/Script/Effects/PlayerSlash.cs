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
        //������Ʈ �����ֱ�� Awake, Start�� Update���� ���� �Ͼ�� ������
        //�� ��ũ��Ʈ���� ���� ������ �����Ҷ� ���� ���ϸ鼭 �����°� ������ �Ұ�����
        //InputManager���� ������ Update���� ���ŵǴ°� ���
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
        //�÷��̾� AnimationTrigger���� �Ŵ��� �����ؼ� �������Ѻôµ�
        //���� ������ ���ÿ� ���Ҷ� ���� �ȵǴ� �����־ �׳� ���⼭ ��
        Frame++;



        //ȸ���Ҷ� �ܻ� �����Ϸ��� �� ������ �����ϰ�
        //����Ƽ���� slash �ִϸ��̼� �������� 12�����ӿ��� 10���������� �Ű���
        if (Frame >= 10)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //���� ������Ʈ�� ����Ʈ�� ������ ���°� ����
        if (collision.gameObject.CompareTag("Enemy") && !isHit
            && !FXManager.instance.playerSlashEffect.alreadyHitEnemy.Contains(collision.gameObject))
        {
            shakeTimer = 0f;
            //�ݶ��̴� �������� üũ�ϴ°� ����
            isHit = true;

            //���� ������ trigger�� ��� ������ List�� �־���
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

            //���Ѻ��� ������
            if (DistanceBetweenPlayerAndEnemy < closestDistance)
            {
                //�� ó���� ���Ѻ��� ������ �ƹ� enimies�� �Ÿ��� ���ðŰ�
                //�� ���� closestDistance�� �� �Ÿ��� �ǰ�
                //�װź��� ������ ������ �ְ� �ϸ鼭 �ݺ���
                //���� ���������� ��ȸ�ϸ鼭 ���� �۾����ٰ� ���̻� �۾����� ���ϴ� �Ÿ���
                //���� ����� �Ÿ���
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
