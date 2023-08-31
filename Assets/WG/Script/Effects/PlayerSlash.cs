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

    bool isHit, isHitBullet, timerStop;

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
        isHitBullet = false;
    }

    private void FixedUpdate()
    {
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
            FXManager.instance.cameraEffect.ShakeTimer = 0f;
            //�ݶ��̴� �������� üũ�ϴ°� ����
            isHit = true;

            //���� ������ trigger�� ��� ������ List�� �־���
            FXManager.instance.playerSlashEffect.alreadyHitEnemy.Add(collision.gameObject);

            SetClosestEnemy();

            if (FXManager.instance.playerSlashEffect.ClosestEnemyInList != null
                && collision.gameObject == FXManager.instance.playerSlashEffect.ClosestEnemyInList)
            {

                OntriggerExcute();

                //�ڷ�ƾ �����°� CameraEffect�ʿ� yield�־���
                FXManager.instance.cameraEffect.StartCoroutine("HitShake");

                FXManager.instance.playerSlashEffect.TimeStop();
            }
        }

        if (collision.gameObject.CompareTag("EnemyBullet") && !isHitBullet)
        {
            collision.gameObject.tag = "PlayerBullet";

            //trigger�� ������Ʈ�� ���� ������Ʈ���� ����ִ��� Ȯ���ϰ� ������ true ������ false ��ȯ
            if (collision.gameObject.TryGetComponent<Rigidbody2D>(out var colRb)
                && collision.gameObject.TryGetComponent<BulletParentsData>(out var bulletParentsData))
            {
                //�Ѿ��� �Ѿ��� �θ� �ٶ󺸴� ������ ����ȭ
                Vector2 parryDir = (bulletParentsData.Parent.transform.position - colRb.transform.position).normalized;

                //Ȥ�� ������ �������� �𸣴� �ϴ� ���
                float parryAngle = Mathf.Atan2(parryDir.y, parryDir.x) * Mathf.Rad2Deg;

                colRb.velocity = Vector2.zero;

                //����ȭ �� �������� �����ֱ�
                colRb.velocity =
                    parryDir * FXManager.instance.playerSlashEffect.ParryToShootSpeed;
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
