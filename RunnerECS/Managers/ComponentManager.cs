using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunnerECS.Components;

namespace RunnerECS.Content
{
    public class ComponentManager
    {
        private static ComponentManager cm;
        private uint nextid;
        private Dictionary<Type, Dictionary<uint, IEngineComponent>> Components = new Dictionary<Type, Dictionary<uint, IEngineComponent>>();
        private ComponentManager()
        {
            
        }

        public static ComponentManager Get()
        {
            if (cm == null)
            {
                cm = new ComponentManager();
            }
            return cm;
        }

        public uint NewEntity()
        {
            return nextid++;
        }

        public T EntityComponent<T>(uint id)
        {
            if (!Components.ContainsKey(typeof(T)))
            {
                Components.Add(typeof(T), new Dictionary<uint, IEngineComponent>());
            }
            var components = Components[typeof(T)];
            if (components.ContainsKey(id))
            {
                return (T) components[id];
            }
            else
            {
                return default(T);
            }      
        }

        public void AddComponentToEntity(IEngineComponent component, uint id)
        {
            if (!Components.ContainsKey(component.GetType()))
            {
                Components.Add(component.GetType(), new Dictionary<uint, IEngineComponent>());
            }
            var components = Components[component.GetType()];
            components.Add(id, component);
        }

        public Dictionary<uint, IEngineComponent> GetComponents<TComponentType>()
        {
            if (!Components.ContainsKey(typeof(TComponentType)))
            {
                Components.Add(typeof(TComponentType), new Dictionary<uint, IEngineComponent>());
            }
            return Components[typeof(TComponentType)];
        }


    }
}
