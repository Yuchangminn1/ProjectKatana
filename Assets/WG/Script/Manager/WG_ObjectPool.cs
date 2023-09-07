using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//��ü�� ������ ����ִ� Ŭ���� ����
//�ν����� â���� �̸� ������ ��ü �־�����ϴ� Ŭ���� ����ȭ
[System.Serializable]
public class ObjectInfo
{
    public GameObject goPrefab;
    public int count;
    //TFPoolParent �Ʒ��� �ڽĿ��� �����ǰ� �Ұ��� (Canvas�ϱ�)
    public Transform TFPoolParent;
}
public class WG_ObjectPool : WG_Managers
{
    //�迭�� ObjectInfoŬ���� �ҷ���
    [SerializeField] ObjectInfo[] ObejctInfos;

    //�����ڿ� �ν��Ͻ��� ���� ��𼭵� �ְ� ���� �����Ӱ�
    public static WG_ObjectPool instance;

    //�����Ͱ� ���Լ��� �������
    public Queue<GameObject> ObjectQueue = new Queue<GameObject>();
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        GetMyself();

        //��Ʈ ť�� �迭 ������Ʈ���� 0��° ��ü �־���
        ObjectQueue = InsterQueue(ObejctInfos[0]);
    }

    private void GetMyself()
    {
        //�ڱ� �ڽ� ���� �־��༭ ������ �Ҵ�
        if (instance != null)
            Destroy(instance.gameObject);

        else
            instance = this;
    }

    //GameObject������ ť�� ���Ͻ�Ű�� �Լ�
    Queue<GameObject> InsterQueue(ObjectInfo p_objectInfo)
    {
        //�ӽ� ť
        Queue<GameObject> temp_queue = new Queue<GameObject>();

        for (int i = 0; i < p_objectInfo.count; i++)
        {
            GameObject t_clone = Instantiate(p_objectInfo.goPrefab, transform.position, Quaternion.identity);
            t_clone.SetActive(false);

            //TFPoolParent�� �θ� ����ߴٸ�
            if (p_objectInfo.TFPoolParent != null)
                //�θ�� �����
                t_clone.transform.SetParent(p_objectInfo.TFPoolParent);

            //�θ� ��� �������� �� ��ũ��Ʈ �پ��ִ� ������Ʈ�� �θ�� �����
            else
                t_clone.transform.SetParent(this.transform);

            //ť�� p_objectInfo.count ������ŭ �־���.
            temp_queue.Enqueue(t_clone);
        }

        return temp_queue;
    }

    public void RefreshTheQueue(int RoomNumber)
    {

        ObjectQueue = InsterQueue(ObejctInfos[RoomNumber]);
    }
}