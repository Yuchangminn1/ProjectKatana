using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayData
{
    #region ����������
    public Vector3 position { get; private set; }
    public ReplayData(Vector3 position)
    {
        this.position = position;
    }

    #endregion
}
