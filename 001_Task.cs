using System;

namespace _001_Weapon
{
    interface IDamageable
    {
        void TakeDamage(int damage);
    }

    interface IReadOnlyHealth
    {
        int Value { get; }
    }

    class Health : IReadOnlyHealth, IDamageable
    {
        public int Value { get; private set; }

        public Health(int value)
        {
            Value = value;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage));

            Value -= damage;
        }
    }

    class Bullet
    {
        private readonly int _damage;

        public int Damage => _damage;
    }

    class Weapon
    {
        private readonly int _bulletPerShoot = 1;

        private int _bulletsCount;

        private bool _isCanShoot => _bulletsCount > 0;

        private Bullet _bullet;

        public void Fire(IDamageable damageable)
        {
            if (_isCanShoot == false)
                throw new ArgumentOutOfRangeException(nameof(_isCanShoot));

            _bulletsCount -= _bulletPerShoot;
            damageable.TakeDamage(_bullet.Damage);
        }
    }

    class Player : IDamageable
    {
        private Health _health;
        private bool _isAlive => _health.Value > 0;

        public void TakeDamage(int damage)
        {
            if (_isAlive == false)
                throw new ArgumentException("Игрок мертв");

            _health.TakeDamage(damage);
        }
    }

    class Bot
    {
        private Weapon _weapon;

        public void OnSeePlayer(Player player)
        {
            if (GetTarget(player))
                _weapon.Fire(player);
        }

        private bool GetTarget(Player player)
        {
            if (player == null)
                throw new ArgumentNullException();

            return true;
        }
    }
}