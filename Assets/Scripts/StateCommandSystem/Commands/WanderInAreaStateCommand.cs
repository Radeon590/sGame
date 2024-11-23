using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateCommandSystem.Commands
{
    public class WanderInAreaStateCommand : NavigateStateCommand
    {
        private Vector2 _areaMiddlePoint;
        private float _distanceFromMiddlePoint;

        public float WonderingTimer = 5;
        
        public WanderInAreaStateCommand(Vector2 areaMiddlePoint, float distanceFromMiddlePoint) 
            : base(GetRandomPointInArea(areaMiddlePoint, distanceFromMiddlePoint))
        {
            _areaMiddlePoint = areaMiddlePoint;
            _distanceFromMiddlePoint = distanceFromMiddlePoint;
        }

        private static Vector2 GetRandomPointInArea(Vector2 areaMiddlePoint, float distanceFromMiddlePoint)
        {
            var x = Random.Range(areaMiddlePoint.x - distanceFromMiddlePoint, areaMiddlePoint.x + distanceFromMiddlePoint);
            var y = Random.Range(areaMiddlePoint.y, areaMiddlePoint.y + distanceFromMiddlePoint);
            return new Vector2(x, y);
        }

        public override void Invoke(StateCommandTarget stateCommandTarget)
        {
            base.Invoke(stateCommandTarget);
            stateCommandTarget.StartCoroutine(WanderCoroutine(_navigatable));
        }

        public override void Cancel(StateCommandTarget stateCommandTarget)
        {
            base.Cancel(stateCommandTarget);
            stateCommandTarget.StopCoroutine(WanderCoroutine(_navigatable));
        }

        private IEnumerator WanderCoroutine(Navigatable navigatable)
        {
            while (true)
            {
                yield return new WaitForSeconds(WonderingTimer);
                _targetPos = GetRandomPointInArea(_areaMiddlePoint, _distanceFromMiddlePoint);
                navigatable.SetTarget(_targetPos);
            }
        }
    }
}