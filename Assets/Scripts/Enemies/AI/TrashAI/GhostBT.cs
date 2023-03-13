﻿using System.Collections.Generic;
using BehaviorTree;
using Scriptables;
using UnityEngine;
using Tree = BehaviorTree.Tree;

namespace AI.GhostAI
{
    public class GhostBT : Tree
    {
        [SerializeField] private LayerMask _enemiesMask;

        private GhostStatsSO _ghostStatsSO;

        protected override Node SetupTree()
        {
            _ghostStatsSO = GetComponent<Ghost>()._ghostSO;
            
            Node root = new Selector(new List<Node>
            {
                new CheckStun(transform),
                new Sequence(new List<Node>
                {
                    new CheckPlayerInAttackRange(transform, _ghostStatsSO.AttackRange),
                    new TaskAttack(transform, _ghostStatsSO.AttackCD),
                }),
                new Sequence(new List<Node>
                {
                    new CheckPlayer(transform),
                    new TaskGoToTarget(transform, _enemiesMask, _ghostStatsSO.MoveSpeed),
                })
            });

            return root;
        }
    }
}