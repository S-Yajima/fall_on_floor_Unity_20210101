using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * キューブのコントローラ
 * 床への落下検知後に姿勢を固定する
 */
public class CubeController : MonoBehaviour
{
    // キューブのY軸の回転角度
    private float rotate_angle_y = 0.0f;
    // キューブがUpdate()実行ごとに回転する/しないのフラグ
    private bool is_enable_role = false;

    // Start is called before the first frame update
    void Start()
    {
        // Y軸の回転角度を初期値としてメンバ変数に取得する
        Vector3 angles = transform.localEulerAngles;
        this.rotate_angle_y = angles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.is_enable_role == true)
        {
            // 指定の角度ずつ回転するように設定する
            this.rotate_angle_y += 0.5f;

            // 算出した回転を実行する
            Vector3 angles = transform.localEulerAngles;
            angles.x = 0.0f;
            angles.y = this.rotate_angle_y;    // 0.0f以外の数値が取得されるケースがあるため明示的に0.0fを代入する
            angles.z = 0.0f;
            transform.localEulerAngles = angles;
        }
    }

    /*
     * 衝突時に自動で実行される。
     * Collider is Trigger フラグが false の場合のみ実行される。
     * 入力: collision 衝突情報 (ここでは使用しない)
     */
    private void OnCollisionEnter(Collision collision)
    {
        // 床に落ちた瞬間にキューブの姿勢RigidbodyコンポーネントのisKinematicフラグにより固定する
        // Rigidbody
        // bool isKinematic : 物理演算の影響を受けるかどうかを示すフラグ
        GetComponent<Rigidbody>().isKinematic = true;
        // キューブを回転させるように設定する
        this.is_enable_role = true;

        /*
         * xyz位置と回転のフリーズによる姿勢の固定を試した。
         * 位置と姿勢の固定自体は正常に動作するが仕組みとして不安を感じるため
         * isKinematicにて姿勢の固定を行う
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX |
                                                RigidbodyConstraints.FreezePositionY |
                                                RigidbodyConstraints.FreezePositionZ |
                                                RigidbodyConstraints.FreezeRotationX |
                                                RigidbodyConstraints.FreezeRotationY |
                                                RigidbodyConstraints.FreezeRotationZ;
        */
    }
}
