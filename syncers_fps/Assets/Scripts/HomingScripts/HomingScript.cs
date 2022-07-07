using System.Collections;
using UnityEngine;
public sealed class HomingScript : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField, Min(0)]
    float time = 1;

    [SerializeField]
    float lifeTime = 2;

    [SerializeField]
    float turnTime = 0.5f;

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

    private bool updateAccel = true;

    float turnSpeed = 60f;

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
        // velocity = transform.forward * maxAcceleration;
        // velocity = new Vector3(0f,0f,0f);
        StartCoroutine(nameof(Timer));
    }
    public void Update()
    {
        move();
    }

    void newMove()
    {
        if (target == null) {
            return;
        }

        Vector3 targetDirection = target.position - transform.position;
        float singleStep = turnSpeed * Time.deltaTime;
        thisTransform.rotation = Quaternion.RotateTowards(thisTransform.rotation, Quaternion.LookRotation(targetDirection), singleStep);

        transform.Translate(transform.forward * 2f);
    }

    void move()
    {
        // ターゲットがいなければ処理終了
        if (target == null) {
            return;
        }

        // 加速度を算出
        if(updateAccel==true)
        {
            acceleration = 2f / (time * time) * (target.position - position - time * velocity);

            // もし加速度制限がTrue 且つ 加速度の値が実際に超過していた場合 (大きさを比較するときsqrを使う方が計算が早い)
            if (limitAcceleration && acceleration.sqrMagnitude > maxAcceleration * maxAcceleration)
            {
                // 正規化をしたaccelerationに最大値を掛ける
                acceleration = acceleration.normalized * maxAcceleration;
            }
        }

        time -= Time.deltaTime;
        turnTime -= Time.deltaTime;

        if (turnTime < 0f)
        {
            updateAccel = false;
        }

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

    Quaternion vToQ(Vector3 givenVector)
    {
        return Quaternion.Euler(givenVector.x, givenVector.y, givenVector.z);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}