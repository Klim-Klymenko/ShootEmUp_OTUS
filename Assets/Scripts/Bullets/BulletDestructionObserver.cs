using ShootEmUp.Interfaces;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Bullet))]
    //создал отдельный обсервер для того, чтобы в самой пуле не получать зависимость на BulletManager для вызова анспавна + появляется расширяемость добавлять доп эффекты при destory пули
    //если отталкиваться от предложения подписать и отписать на анспавн в самом BulletManager, то как по мне берет доп. ответственности слушателя + в методе спавн подписка не соответсвовала бы тому, что выполняет данный метод
    //если подписывать и отписывать в самом спавнере, то нарушает немного SRP. Задача спавнера настраивать объект, а не еще доп. быть слушателем. Поэтому остановился на гибком варианте - обсервер
    //кривое прокидование менеджера и других компонентов будет убрано при выполнении домашки по DI
    public class BulletDestructionObserver : MonoBehaviour, IGameStartListener,
        IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        [SerializeField] private Bullet _bullet;

        private IBulletUnspawner _bulletUnspawner;

        public IBulletUnspawner BulletUnspawner
        {
            set => _bulletUnspawner = value;
        }

        private void OnValidate() => _bullet = GetComponent<Bullet>();

        private void Enable() => _bullet.OnBulletDestroyed += UnspawnBullet;

        private void Disable() => _bullet.OnBulletDestroyed -= UnspawnBullet;

        private void UnspawnBullet() => _bulletUnspawner.UnspawnBullet(_bullet);
        
        void IGameStartListener.OnStart() => Enable();

        void IGameFinishListener.OnFinish() => Disable();

        void IGameResumeListener.OnResume() => Enable();

        void IGamePauseListener.OnPause() => Disable();
    }
}