using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class Block : MonoBehaviour
    {
        [Tooltip("ACTIONS = ANIMATOR TRIGGERS \n They are read in this order.")]
        [SerializeField]private List<string> actions;

        public Block(List<string> actions)
        {
            this.actions = actions;
        }

        public List<string> Actions
        {
            get => actions;
            set => actions = value;
        }
    }
}