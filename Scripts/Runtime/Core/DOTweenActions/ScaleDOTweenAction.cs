﻿using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace BrunoMikoski.AnimationSequencer
{
    [Serializable]
    public sealed class ScaleDOTweenAction : DOTweenActionBase
    {
        public override Type TargetComponentType => typeof(Transform);
        public override string DisplayName => "Scale to Size";

        [SerializeField]
        private Vector3 scale;
        [SerializeField]
        private AxisConstraint axisConstraint;
        
        public override bool CreateTween(GameObject target, float duration, int loops, LoopType loopType)
        {
            TweenerCore<Vector3, Vector3, VectorOptions> scaleTween = target.transform.DOScale(scale, duration).SetEase(ease);
            scaleTween.SetOptions(axisConstraint);

            SetTween(scaleTween, loops, loopType);
            return true;
        }
    }
}
