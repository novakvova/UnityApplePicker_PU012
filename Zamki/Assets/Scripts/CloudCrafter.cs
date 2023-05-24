using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int numClouds = 40; //кільікість хмаринок
    public GameObject cloudPrefab; //шаблон для хмаринок
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1; //мінімлаьний маштаб хмаринок
    public float cloudScaleMax = 3; //максимальний маштаб хмаринок
    public float cloudSppedMult = 0.5f; //Коефіцієнт швикдості хмаринок

    private GameObject[] cloudInstances;

    private void Awake()
    {
        cloudInstances = new GameObject[numClouds];
        GameObject anchor = GameObject.Find("CloudAnchor");
        GameObject cloud;
        for (int i = 0; i < numClouds; i++)
        {
            cloud = Instantiate<GameObject>(cloudPrefab);
            //місце розташування хмарки
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);
            //Маштабування хмарку
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            //Менша хмартинка ближче до землі
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
            //Менша хмаринка має буть далі
            cPos.z = 100 - 90 * scaleU;
            //Примініємо отримані дані до хмаринки
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            cloud.transform.SetParent(anchor.transform);
            cloudInstances[i] = cloud;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject cloud in cloudInstances)
        {
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.localPosition;
            cPos.x -= scaleVal * Time.deltaTime * cloudSppedMult;
            if (cPos.x <= cloudPosMin.x)
                cPos.x = cloudPosMax.x;
            cloud.transform.position = cPos;
        }
    }
}
