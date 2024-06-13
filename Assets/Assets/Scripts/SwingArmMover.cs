using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HingeJoint))]
[RequireComponent(typeof(SpringJoint))]
public class SwingArmMover : MonoBehaviour
{
    [SerializeField] private HingeJoint _hingeJoint;
    [SerializeField] private SpringJoint _springJoint;
    [SerializeField] private float _pullDownForce = 500;
    [SerializeField] private float _pullDownTargetVelocity = 20;
    [SerializeField] private float _loadAngle = -90;
    [SerializeField] private float _shootForce = 20;

    private JointMotor _motor;
    private JointLimits _limits;
    private float _shootAngle = 5;

    public UnityAction ReadyToRelease;

    private void Awake()
    {
        _springJoint = GetComponent<SpringJoint>();
        _hingeJoint = GetComponent<HingeJoint>();
        _motor = _hingeJoint.motor;
        _limits = _hingeJoint.limits;
        _limits.max = _shootAngle;
        _hingeJoint.useLimits = true;
    }

    private void Update()
    {
        if (_hingeJoint.angle <= _loadAngle)
        {
            ReadyToRelease?.Invoke();
        }
    }

    public void Release()
    {
        _hingeJoint.useMotor = false;
    }

    public void PullDown()
    {
        _motor.force = _pullDownForce;
        _motor.targetVelocity = -_pullDownTargetVelocity;
        _limits.min = _loadAngle;

        _springJoint.spring = _shootForce;

        _hingeJoint.useMotor = true;
    }
}
