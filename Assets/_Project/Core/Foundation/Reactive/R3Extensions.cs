using System;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Core.Foundation.Reactive
{
    public static class R3Extensions
    {
        public static IDisposable BindToText(this ReactiveProperty<string> property, TMP_Text text)
        {
            if (text == null) 
            {
                return Disposable.Empty;
            }

            return property.ObserveOnMainThread().Subscribe(value => text.text = value);
        }

        public static IDisposable BindToFillAmount(this ReactiveProperty<float> property, Image image)
        {
            if (image == null) 
            {
                return Disposable.Empty;
            }

            return property.ObserveOnMainThread().Subscribe(value => image.fillAmount = value);
        }

        public static IDisposable BindToActive(this ReactiveProperty<bool> property, GameObject gameObject)
        {
            if (gameObject == null) return Disposable.Empty;
            return property.ObserveOnMainThread().Subscribe(active =>
            {
                if (gameObject.activeSelf != active) gameObject.SetActive(active);
            });
        }

        public static Observable<T> ThrottleFirst<T>(this Observable<T> source, TimeSpan duration)
        {
            return ObservableExtensions.ThrottleFirst(source, duration);
        }

        public static Observable<long> SafeTimer(TimeSpan period)
        {
            Scene currentScene = SceneManager.GetActiveScene();

            Observable<Scene> sceneUnloadedTrigger = Observable.Create<Scene>(observer =>
            {
                void handler(Scene scene)
                {
                    observer.OnNext(scene);
                }

                SceneManager.sceneUnloaded += handler;
                return Disposable.Create(() => SceneManager.sceneUnloaded -= handler);
            });


            return Observable.Interval(period)
                .ObserveOn(UnityFrameProvider.Update)
                .Select((_, index) => (long)index)
                .TakeUntil(sceneUnloadedTrigger.Where(scene => scene == currentScene));
        }
    }
}