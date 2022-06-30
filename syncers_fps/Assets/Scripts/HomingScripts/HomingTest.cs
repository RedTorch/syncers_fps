using System.Collections;
using UnityEngine;
public sealed class HomingTest : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField, Min(0)]
    float time = 1;

    [SerializeField]
    float lifeTime = 2;

    [SerializeField]
    bool limitAcceleration = false;

    [SerializeField, Min(0)]
    float maxAcceleration = 100;

    [SerializeField]
    Vector3 minInitVelocity;

    [SerializeField]
    Vector3 maxInitVelocity;
    
    Vector3 position;
    Vector3 velocity;
    Vector3 acceleration;
    Transform thisTransform;

    public Transform Target
    {
        set {
            target = value;
        }
        get {
            return target;
        }
    }

    void Start()
    {
        // thisTransformにこのオブジェクトのtransformの情報（回転位置大きさなど）を代入
        thisTransform = transform;
        // positionにはオブジェクトの位置を代入
        position = thisTransform.position;
        // 速度のベクトルを決定する
        velocity = new Vector3(Random.Range(minInitVelocity.x, maxInitVelocity.x), Random.Range(minInitVelocity.y, maxInitVelocity.y), Random.Range(minInitVelocity.z, maxInitVelocity.z));
        StartCoroutine(nameof(Timer));
    }
    public void Update()
    {
        // ターゲットがいなければ処理終了
        if (target == null) {
            return;
        }

        // 加速度を算出
        acceleration = 2f / (time * time) * (target.position - position - time * velocity);

        // もし加速度制限がTrue 且つ 加速度の値が実際に超過していた場合 (大きさを比較するときsqrを使う方が計算が早い)
        if (limitAcceleration && acceleration.sqrMagnitude > maxAcceleration * maxAcceleration)
        {
            // 正規化をしたaccelerationに最大値を掛ける
            acceleration = acceleration.normalized * maxAcceleration;
        }

        time -= Time.deltaTime;

        // 命中するまでの時間が0になったら処理終了
        if (time < 0f)
        {
            return;
        }

        // 速度 = 加速度 * 時間
        velocity += acceleration * Time.deltaTime;
        // 距離 = 速度 * 時間
        position += velocity * Time.deltaTime;
        // 球の位置に距離を代入
        thisTransform.position = position;
        // rotationに速度のベクトルを代入する
        thisTransform.rotation = Quaternion.LookRotation(velocity);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}