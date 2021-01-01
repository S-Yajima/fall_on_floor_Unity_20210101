using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * ディレクタースクリプト
 * 時間の経過とともにキューブとスフィアを順次落下させる
 */
public class Director : MonoBehaviour
{
    private GameObject cube;
    private GameObject Sphere_blue;
    private GameObject Sphere_red;
    private GameObject Sphere_green;
    private GameObject Sphere_yellow;

    // Start is called before the first frame update
    void Start()
    {
        // キューブとスフィアのGameObjectへの参照を取得する
        this.cube = GameObject.Find("Cube");
        this.Sphere_blue = GameObject.Find("Sphere_blue");
        this.Sphere_red = GameObject.Find("Sphere_red");
        this.Sphere_green = GameObject.Find("Sphere_green");
        this.Sphere_yellow = GameObject.Find("Sphere_yellow");

        // 時間経過により呼び出すメソッドをInvoke()により設定する
        // 注意: Invoke()へ引数は渡せない
        Invoke("fall_cube", 3.0f);
        Invoke("fall_Sphere_blue", 10.0f);
        Invoke("fall_Sphere_red", 20.0f);
        Invoke("fall_Sphere_green", 30.0f);
        Invoke("fall_Sphere_yellow", 40.0f);
    }

    // キューブを落下させる
    void fall_cube()
    {
        this.cube.GetComponent<Rigidbody>().useGravity = true;
    }

    // スフィア(青)を落下させる
    void fall_Sphere_blue()
    {
        this.Sphere_blue.GetComponent<Rigidbody>().useGravity = true;
    }

    // スフィア(赤)を落下させる
    void fall_Sphere_red()
    {
        this.Sphere_red.GetComponent<Rigidbody>().useGravity = true;
    }

    void fall_Sphere_green()
    {
        this.Sphere_green.GetComponent<Rigidbody>().useGravity = true;
    }

    void fall_Sphere_yellow()
    {
        this.Sphere_yellow.GetComponent<Rigidbody>().useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
