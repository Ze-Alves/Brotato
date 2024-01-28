using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerShootCanon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _canonFirepoint;
    [SerializeField] private GameObject _potatoBabyPrefab;
    [SerializeField] private float _potatoBabyForce;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private GameObject _shootFX;
    [SerializeField] private Animator _canonAnimator;

    [Header("Baby Potato Things")]
    [SerializeField] private Transform _tickleTarget;

    [Header("Values")]
    [SerializeField] private float _delayBetweenShots;
    [SerializeField] private float _maxAmmo;
    private float _currentAmmo;
    private float _timeSinceLastShot = 0;

    private void Awake()
    {
        _currentAmmo = _maxAmmo;
        UpdateAmmoCount();

    }

    private void Update()
    {
        _timeSinceLastShot += Time.deltaTime;
        if (Input.GetMouseButton(1) && _timeSinceLastShot >= _delayBetweenShots && _currentAmmo > 0)
        {
            Debug.Log("Shoot");

            _timeSinceLastShot = 0;
            _currentAmmo--;
            Instantiate(_shootFX, _canonFirepoint.position, _canonFirepoint.rotation);
            CameraShakeController.Instance.DoShake(2f,2f,.2f);
            _canonAnimator.SetTrigger("CanonShoot");

            UpdateAmmoCount();

            var potatoBaby = Instantiate(_potatoBabyPrefab, _canonFirepoint.position, _canonFirepoint.rotation);
            var potatoBabyScript = potatoBaby.GetComponent<PotatoBabiesActivate>();

            potatoBabyScript.AddForce(_potatoBabyForce);
            potatoBabyScript.SetTickleTarget(_tickleTarget);
        }
    }

    public void AddPotato()
    {
        _currentAmmo++;
        if (_currentAmmo > _maxAmmo)
        {
            _currentAmmo = _maxAmmo;
        }
        UpdateAmmoCount();
    }

    private void UpdateAmmoCount()
    {
        if (!_ammoText) return;

        _ammoText.text = _currentAmmo.ToString();
    }
}
