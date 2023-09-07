using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//객체의 정보를 담고있는 클래스 생성
//인스펙터 창에서 미리 생성할 객체 넣어줘야하니 클래스 직렬화
[System.Serializable]
public class ObjectInfo
{
    public GameObject goPrefab;
    public int count;
    //TFPoolParent 아래의 자식에서 생성되게 할거임 (Canvas니까)
    public Transform TFPoolParent;
}
public class WG_ObjectPool : WG_Managers
{
    //배열로 ObjectInfo클래스 불러옴
    [SerializeField] ObjectInfo[] ObejctInfos;

    //공유자원 인스턴스를 통해 어디서든 넣고 빼고가 자유롭게
    public static WG_ObjectPool instance;

    //데이터가 선입선출 순서대로
    public Queue<GameObject> ObjectQueue = new Queue<GameObject>();
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        GetMyself();

        //노트 큐에 배열 오브젝트인포 0번째 객체 넣어줌
        ObjectQueue = InsterQueue(ObejctInfos[0]);
    }

    private void GetMyself()
    {
        //자기 자신 값을 넣어줘서 데이터 할당
        if (instance != null)
            Destroy(instance.gameObject);

        else
            instance = this;
    }

    //GameObject형식의 큐를 리턴시키는 함수
    Queue<GameObject> InsterQueue(ObjectInfo p_objectInfo)
    {
        //임시 큐
        Queue<GameObject> temp_queue = new Queue<GameObject>();

        for (int i = 0; i < p_objectInfo.count; i++)
        {
            GameObject t_clone = Instantiate(p_objectInfo.goPrefab, transform.position, Quaternion.identity);
            t_clone.SetActive(false);

            //TFPoolParent에 부모를 등록했다면
            if (p_objectInfo.TFPoolParent != null)
                //부모로 삼아줌
                t_clone.transform.SetParent(p_objectInfo.TFPoolParent);

            //부모 등록 안했으면 이 스크립트 붙어있는 오브젝트를 부모로 삼아줌
            else
                t_clone.transform.SetParent(this.transform);

            //큐에 p_objectInfo.count 개수만큼 넣어줌.
            temp_queue.Enqueue(t_clone);
        }

        return temp_queue;
    }

    public void RefreshTheQueue(int RoomNumber)
    {

        ObjectQueue = InsterQueue(ObejctInfos[RoomNumber]);
    }
}