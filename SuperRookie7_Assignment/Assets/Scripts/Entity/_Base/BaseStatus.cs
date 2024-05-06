using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity.Base
{
    public class BaseStatus : MonoBehaviour
    {
        public  SO_StatusData so_StatusData;

        [UnityEngine.SerializeField]
        protected float currentHP;
        public float CurrentHP { get { return currentHP; } set { currentHP = value; } }
    }
}