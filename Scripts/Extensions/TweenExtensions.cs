/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

using System;
using System.Reflection;
using Object = UnityEngine.Object;
using DG.Tweening;
using DG.Tweening.Core;

namespace Toolkit.Screens.Extensions
{
    public static class TweenExtensions
    {
        public static T AddOnStart<T>(this T tween, TweenCallback action) where T : Tween
        {
            if (tween == null || !tween.active)
                return tween;
            
            TweenCallback onStart = tween.GetOnStart();
            onStart += action;
            tween.OnStart(onStart);
            
            return tween;
        }
        
        public static T AddOnPlay<T>(this T tween, TweenCallback action) where T : Tween
        {
            if (tween == null || !tween.active)
                return tween;
            
            tween.onPlay += action;
            
            return tween;
        }
        
        public static T AddOnPause<T>(this T tween, TweenCallback action) where T : Tween
        {
            if (tween == null || !tween.active)
                return tween;
            
            tween.onPause += action;
            
            return tween;
        }
        
        public static T AddOnRewind<T>(this T tween, TweenCallback action) where T : Tween
        {
            if (tween == null || !tween.active)
                return tween;
            
            tween.onRewind += action;
            
            return tween;
        }
        
        public static T AddOnUpdate<T>(this T tween, TweenCallback action) where T : Tween
        {
            if (tween == null || !tween.active)
                return tween;
            
            tween.onUpdate += action;
            
            return tween;
        }
        
        public static T AddOnStepComplete<T>(this T tween, TweenCallback action) where T : Tween
        {
            if (tween == null || !tween.active)
                return tween;
            
            tween.onStepComplete += action;
            
            return tween;
        }
        
        public static T AddOnComplete<T>(this T tween, TweenCallback action) where T : Tween
        {
            if (tween == null || !tween.active)
                return tween;
            
            tween.onComplete += action;
            
            return tween;
        }
        
        public static T AddOnKill<T>(this T tween, TweenCallback action) where T : Tween
        {
            if (tween == null || !tween.active)
                return tween;
            
            tween.onKill += action;
            
            return tween;
        }
        
        public static T AddOnWaypointChange<T>(this T tween, TweenCallback<int> action) where T : Tween
        {
            if (tween == null || !tween.active)
                return tween;
            
            tween.onWaypointChange += action;
            
            return tween;
        }

        public static T DestroyTargetOnComplete<T>(this T tween) where T : Tween
        {
            if (tween == null || !tween.active)
                return tween;
            
            if (tween.target is Object unityObject)
                return tween.AddOnComplete(() => Object.Destroy(unityObject));
            else if (tween.target is IDisposable disposable)
                return tween.AddOnComplete(() => disposable.Dispose());
            else 
                return tween.AddOnComplete(() => tween.target = null);
        }

        private static TweenCallback GetOnStart<T>(this T tween) where T : Tween
        {
            return (TweenCallback)typeof(ABSSequentiable)
                .GetField("onStart", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(tween);
        }
    }
}