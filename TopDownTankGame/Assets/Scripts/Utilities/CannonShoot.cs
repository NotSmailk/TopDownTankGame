using UnityEngine;

public class CannonShoot
{
    protected CannonParametres _parametres;
    protected Transform _shootPoint;
    protected bool _isReloading = false;

    public void Shoot(Vector3 direction, Quaternion rotation)
    {
        if (_isReloading)
            return;

        Shell shell = GameObject.Instantiate(_parametres.Shell, _shootPoint.position, rotation);
        shell.Launch(direction, _parametres.TargetMask, _parametres.Damage);
        Reload(_parametres.Cooldown);
    }

    public async void Reload(int cooldown)
    {
        _isReloading = true;
        cooldown *= 1000;
        await System.Threading.Tasks.Task.Delay(cooldown);
        _isReloading = false;
    }
}
