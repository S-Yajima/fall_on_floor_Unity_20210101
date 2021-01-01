using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 床のコントローラ
 * Sceneの始まりでrotate_max_countの数値回数だけ回転する
 */
public class PlaneController : MonoBehaviour
{
    // x軸回転角度 初期値 90°
    private float angle_x = 90;
    // 回転最大回数と回転した回数
    private int rotate_max_count = 2;
    private int rotate_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 設定した回数回っていたらメソッドを抜ける
        if (this.rotate_count >= this.rotate_max_count) return;

        // 20度ずつ回転するように設定する
        this.angle_x += 20.0f;

        // 角度が360を超えていたら0に戻す
        if (this.angle_x >= 360.0f){
            this.angle_x = 0.0f;
            this.rotate_count++;
        }

        // 算出した床の回転を実行する
        Vector3 angles = transform.localEulerAngles;
        angles.x = angle_x;
        angles.y = 0.0f;    // 0.0f以外の数値が取得されるケースがあるため明示的に0.0fを代入する
        angles.z = 0.0f;
        transform.localEulerAngles = angles;
    }
}
