using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity.Base
{
    public class BaseStatus : MonoBehaviour
    {
        public  SO_StatusData so_StatusData;

        [UnityEngine.SerializeField]
        protected float current_HP;
        public float Current_HP { get { return current_HP; } set { current_HP = value; } }
    }
}