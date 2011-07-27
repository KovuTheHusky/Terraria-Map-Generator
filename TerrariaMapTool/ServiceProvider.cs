using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerrariaMapTool {
    class ServiceProvider : IServiceProvider {
        private Dictionary<Type, object> services = new Dictionary<Type, object>();

        public void AddService(Type type, object o) {
            services.Add(type, o);
        }

        public object GetService(Type serviceType) {
            object o;
            services.TryGetValue(serviceType, out o);
            return o;
        }
    }
}
