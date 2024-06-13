using UnityEngine;

public class CatapultShooter : MonoBehaviour
{
    [SerializeField] private SwingArmMover _swingArm;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Transform _projectileLoadPoint;

    private bool _isArmed = false;

    public void Shoot()
    {
        if (_isArmed == true)
        {
            _isArmed = false;
            _swingArm.ReadyToRelease -= LoadProjectile;
            _swingArm.Release();
        }
    }

    public void Load()
    {
        if (_swingArm.ReadyToRelease == null)
        {
            _swingArm.ReadyToRelease += LoadProjectile;
        }

        _swingArm.PullDown();
    }

    private void LoadProjectile()
    {
        if (_isArmed == false)
        {
            Instantiate(_projectile, _projectileLoadPoint.position, _projectileLoadPoint.rotation);
            _isArmed = true;
        }
    }
}