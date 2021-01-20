using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //cornPrefabを入れる
    public GameObject conePrefab;
    //スタート地点
    private int startPos = 80;
    //ゴール地点
    private int goalPos = 360;
    //アイテムを出すｘ方向の範囲
    private float posRange = 3.4f;

    //Unityちゃんのオブジェクト（発展課題）
    private GameObject unitychan;
    //Unityちゃんの現在位置（発展課題）
    private float currentPosition = 0f;
    //次のオブジェクト設置位置（発展課題）※初期値はスタート地点と一緒
    private float installationPosition = 80f;
    //次のオブジェクト設置間隔（発展課題）
    private float nextDistance;

    // Start is called before the first frame update
    void Start()
    {
        //Unityちゃんのオブジェクトを取得（発展課題）
        this.unitychan = GameObject.Find("unitychan");
    }

    // Update is called once per frame（下記すべて発展課題）
    void Update()
    {
        //現在のunityちゃんの位置取得
        currentPosition = this.unitychan.transform.position.z;
        
        //次のオブジェクト設置間隔設定
        nextDistance = Random.Range(40, 50);

        //unityちゃんからから40～50ｍ先を設定
        float nextPosition = currentPosition + nextDistance;

        //オブジェクトの設置間隔の判定
        if (installationPosition < nextPosition)
        {
            //オブジェクト設置する範囲の判定
            if (startPos < nextPosition && nextPosition < goalPos)
            {
                int num = Random.Range(1, 11);
                if (num <= 2)
                {
                    //コーンをx軸方向に一直線に生成
                    for (float j = -1; j <= 1; j += 0.4f)
                    {
                        GameObject cone = Instantiate(conePrefab);
                        cone.transform.position = new Vector3(4 * j, cone.transform.position.y, nextPosition);
                    }
                }
                else
                {
                    //レーンごとにアイテムを生成
                    for (int j = -1; j <= 1; j++)
                    {
                        //アイテムの種類を決める
                        int item = Random.Range(1, 11);
                        //アイテムを置くZ座標のオフセットをランダムに設定
                        int offsetZ = Random.Range(-5, 6);
                        //60%コイン配置：30％車配置;10％何もなし
                        if (1 <= item && item <= 6)
                        {
                            //コイン生成
                            GameObject coin = Instantiate(coinPrefab);
                            coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, nextPosition + offsetZ);
                        }
                        else if (7 <= item && item <= 9)
                        {
                            //車を生成
                            GameObject car = Instantiate(carPrefab);
                            car.transform.position = new Vector3(posRange * j, car.transform.position.y, nextPosition + offsetZ);
                        }
                    }
                }

                //次のオブジェクト設置間隔を設定
                installationPosition += 15f;
            }
        }
    }
}
