using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * スフィアのコントローラスクリプト
 * すべてのスフィア共通
 * 床の端や他のスフィアと衝突したら反射ベクトルを算出し進行方向のVector3に設定する
 */
public class SphereController : MonoBehaviour
{
    // 仮想的な壁の法線。ここで仮に定義する
    // (本来は壁のクラスやManager管理クラスなどで定義したい)
    private Vector3 wall_normal_z_p_v3 = new Vector3(0.0f, 0.0f, 1.0f);
    private Vector3 wall_normal_z_m_v3 = new Vector3(0.0f, 0.0f, -1.0f);
    private Vector3 wall_normal_x_p_v3 = new Vector3(1.0f, 0.0f, 0.0f);
    private Vector3 wall_normal_x_m_v3 = new Vector3(-1.0f, 0.0f, 0.0f);

    // 球体Sphereの属性
    // 進行方向ベクトル。ベクトルの大きさは速度、向きは進行方向を表す。
    private Vector3 velocity_v3 = new Vector3(0.0f, 0.0f, 0.0f);
    // スフィアの半径。初期値は0.0fに設定する。
    private float radius = 0.0f;

    // Start is called before the first frame update
    void Start()
    {   
        // 球の半径を取得する
        radius = GetComponent<SphereCollider>().radius;
    }

    /*
     * 一定時間毎に自動で実行される。
     * 物理演算は当メソッドで実行する。
     */
    void FixedUpdate()
    {
        Vector3 position_v3 = transform.position;

        // z軸が壁に衝突しているか判定
        if (transform.position.z > 5.0f - radius)
        {
            position_v3.z = 5.0f - radius;
            transform.position = position_v3;
            reflect(wall_normal_z_m_v3);
        }
        if (transform.position.z < -5.0f + radius)
        {
            position_v3.z = -5.0f + radius;
            transform.position = position_v3;
            reflect(wall_normal_z_p_v3);
        }

        // x軸が壁に衝突しているか判定
        if (transform.position.x > 5.0f - radius)
        {
            position_v3.x = 5.0f - radius;
            transform.position = position_v3;
            reflect(wall_normal_x_m_v3);
        }
        if (transform.position.x < -5.0f + radius)
        {
            position_v3.x = -5.0f + radius;
            transform.position = position_v3;
            reflect(wall_normal_x_p_v3);
        }

        // 速度のベクトルだけ球体を進行させる
        transform.position += velocity_v3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 壁の法線から速度のベクトルの反射ベクトルを算出する
    private void reflect(Vector3 normal_v3)
    {
        velocity_v3 = velocity_v3 + normal_v3 * Vector3.Dot(velocity_v3 * -1, normal_v3) * 2;

    }

    /*
    * 衝突時に自動で実行される。
    * Collider is Trigger フラグが false の場合のみ実行される。
    * 入力: collision 衝突情報
    */
    private void OnCollisionEnter(Collision collision)
    {
        //if(GetComponent<Rigidbody>().useGravity == true)
        if (velocity_v3 == Vector3.zero)    // 最初に床に落ちた場合
        {
            // Y軸の位置を固定し、進行方向のベクトルを設定する
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            //GetComponent<Rigidbody>().useGravity = false;
            velocity_v3 = new Vector3(-0.05f, 0.0f, -0.1f);
        }
        else
        {
            // キューブか他のスフィアと衝突した場合
            // 衝突した相手の法線を取得し反射ベクトルを算出し進行ベクトルに設定する
            Vector3 normal = collision.contacts[0].normal;
            normal.y = 0.0f;
            reflect(normal);
            //Debug.Log("OnCollisionEnter");
        }
        // パーティクルを表示する
        GetComponent<ParticleSystem>().Play();

    }


    // isTriggerがOnに設定されている状態で衝突すると呼び出される
    // isTriggerがOnの状態では衝突時の挙動が自動では実行されない
    // 
    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("OnTriggerEnter");
    }

}
