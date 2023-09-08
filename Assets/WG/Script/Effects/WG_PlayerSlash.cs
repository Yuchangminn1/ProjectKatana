using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WG_PlayerSlash : MonoBehaviour
{
    int Frame = 0;

    //�ν����Ϳ��� ����Ʈ �־���
    protected GameObject slashHitEffect;
    protected GameObject ParryEffect;
    //������ ����Ʈ
    GameObject Hit_Clone;


    bool isHit, isHitBullet;

    private void Awake()
    {
        //������Ʈ �����ֱ�� Awake, Start�� Update���� ���� �Ͼ�� ������
        //�� ��ũ��Ʈ���� ���� ������ �����Ҷ� ���� ���ϸ鼭 �����°� ������ �Ұ�����
        //InputManager���� ������ Update���� ���ŵǴ°� ���
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
            && !WG_FXManager.instance.playerSlashEffect.alreadyHitEnemy.Contains(collision.gameObject))
        {
            WG_FXManager.instance.cameraEffect.ShakeTimer = 0f;
            //�ݶ��̴� �������� üũ�ϴ°� ����
            isHit = true;

            //���� ������ trigger�� ��� ������ List�� �־���
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
            //�ڷ�ƾ �����°� CameraEffect�ʿ� yield�־���
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
