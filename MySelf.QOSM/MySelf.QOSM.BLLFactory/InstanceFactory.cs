
using System.Configuration;
using System.Reflection;

namespace MySelf.QOSM.BLLFactory
{
    public class InstanceFactory
    {
        //实现层程序集名称
        private static readonly string serviceName = ConfigurationManager.AppSettings["ServicesName"] + string.Empty;
        public static T CreateInstance<T>()
        {
            string interfaceName = typeof(T).Name;//接口名称
            string serviceTypeName = ConfigurationManager.AppSettings[interfaceName] +string.Empty; //实现类名称
            string fullTypeName = serviceName + "." + serviceTypeName; //实现类 全名称
            Assembly assembly = Assembly.Load(serviceName);//动态加载程序集
            Type type = assembly.GetType(fullTypeName);//实现类类型
            object obj = Activator.CreateInstance(type);    
            if(obj != null) { 
              return (T)obj;    
            }
            return default;

        }
    }

}
