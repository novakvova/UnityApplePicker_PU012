using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    public Text scoreGT; 
    // Start is called before the first frame update
    void Start()
    {
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        scoreGT = scoreGO.GetComponent<Text>();
        scoreGT.text = "0";

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 maousePos2D = Input.mousePosition;
        maousePos2D.z=-Camera.main.transform.position.z;
        Vector3 maousePos3D = Camera.main.ScreenToWorldPoint(maousePos2D);
        Vector3 pos = this.transform.position;
        pos.x = maousePos3D.x;
        this.transform.position = pos;
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedWith = collision.gameObject;
        if (collidedWith.tag == "Apple")
        {
            Destroy(collidedWith);
            int score = int.Parse(scoreGT.text);
            score += 100;
            scoreGT.text = score.ToString();
        }
    }
}
