using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleTest : MonoBehaviour
{
    public GameObject LeftHip;
    public GameObject RightHip;
    public GameObject RightAnkle;

    // �� ���Ͱ��� ���� ���()
    public float GetAngle(GameObject referenceFrom, GameObject referenceTo, GameObject from, GameObject to)
    {
        Vector3 referenceVector = referenceTo.transform.position - referenceFrom.transform.position;
        Vector3 directionVector = to.transform.position - from.transform.position;
        Debug.Log("referenceVector : " + referenceVector);
        Debug.Log("directionVector : " + directionVector);

        referenceVector.Normalize();
        directionVector.Normalize();
        Debug.Log("referenceVector Normalize : " + referenceVector);
        Debug.Log("directionVector Normalize : " + referenceVector);

        float angle = Vector3.Angle(referenceVector, directionVector);
        Debug.Log("angle : " + angle);

        return angle;
    }

    // �� ���Ͱ��� ��ȣ�ִ� ���� ���()
    public float GetSignedAngle(GameObject referenceFrom, GameObject referenceTo, GameObject from, GameObject to)
    {
        Vector3 referenceVector = referenceTo.transform.position - referenceFrom.transform.position;
        Vector3 directionVector = to.transform.position - from.transform.position;
        Debug.Log("referenceVector : " + referenceVector);
        Debug.Log("directionVector : " + directionVector);

        referenceVector.Normalize();
        referenceVector.Normalize();
        Debug.Log("referenceVector Normalize : " + referenceVector);
        Debug.Log("directionVector Normalize : " + referenceVector);

        Vector3 crossProduct = Vector3.Cross(referenceVector, directionVector);
        Debug.Log("crossProduct : " + crossProduct);

        float signedAngle = Vector3.SignedAngle(referenceVector, directionVector, crossProduct);
        Debug.Log("signedAngle : " + signedAngle);

        return signedAngle;
    }

    


    void Start()
    {
        Debug.Log("[��ȣ���� ����]");
        GetAngle(LeftHip, RightHip, RightAnkle, RightHip);

        Debug.Log("[��ȣ�ִ� ����]");
        GetSignedAngle(LeftHip, RightHip, RightAnkle, RightHip);
        
        /*
        Debug.Log("[���� ��� �׽�Ʈ]");
        Vector3 vectorAB = RightHip.transform.position - LeftHip.transform.position;
        Vector3 vectorBC = RightHip.transform.position - RightAnkle.transform.position;

        // �� ���͸� ����ȭ
        vectorAB.Normalize();
        vectorBC.Normalize();

        // ���� AB�� ���� BC ������ ������ ���
        float angle = Vector3.Angle(vectorAB, vectorBC);

        // ����� ���
        Debug.Log("���� : " + angle );
        */
    }

    void Update()
    {
        
    }
}
