using Entity.Base;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Singleton.Manager
{
    public class EntityManager : MonoSingleton<EntityManager>
    {
        [SerializeField]
        private List<BaseEntity> characters = new List<BaseEntity>();
        
        [SerializeField]
        private List<BaseEntity> monsters = new List<BaseEntity>();
    }
}
