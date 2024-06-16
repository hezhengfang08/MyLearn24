
using MySelf.QOSM.Common;
using System.Configuration;
using System.Reflection;

namespace MySelf.QOSM.BLLFactory
{
    public class InstanceFactory
    {
        //ʵ�ֲ��������
        //private static readonly string serviceName = ConfigHelper.GetAppSettings<AppSettings>("AppSettings");
        private static readonly string serviceName=  ConfigHelper.GetSectionClassValue<AppSettings>("AppSettings").ServicesName;
        public static T CreateInstance<T>()
        {
            string interfaceName = typeof(T).Name;//�ӿ�����
            //string serviceTypeName = ConfigurationManager.AppSettings[interfaceName] +string.Empty; //ʵ��������
            string serviceTypeName = ConfigHelper.GetSectionKeyValue("AppSettings:ServicesMapping", interfaceName); //ʵ��������
            string fullTypeName = serviceName + "." + serviceTypeName; //ʵ���� ȫ����
            Assembly assembly = Assembly.Load(serviceName);//��̬���س���
            Type type = assembly.GetType(fullTypeName);//ʵ��������
            object obj = Activator.CreateInstance(type);    
            if(obj != null) { 
              return (T)obj;    
            }
            return default;

        }
    }

}
