using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace IocContainProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
    /// <summary>
    /// 自定义应用程序上下文对象
    /// </summary>
    public class AppContextExt : IDisposable
    {
        /// <summary>
        /// app.config读取
        /// </summary>
        public Configuration AppConfig { get; set; }
        /// <summary>
        /// 真正的ApplicationContext对象
        /// </summary>
        public ApplicationContext Application_Context { get; set; }
        //服务集合
        public static Dictionary<Type, object> Services = new Dictionary<Type, object>();
        //服务订阅事件集合
        public static Dictionary<Type, IList<Action<object>>> ServiceEvents = new Dictionary<Type, IList<Action<object>>>();
        //上下文对象的单例
        private static AppContextExt _ServiceContext = null;
        private readonly static object lockObj = new object();
        /// <summary>
        /// 禁止外部进行实例化
        /// </summary>
        private AppContextExt()
        {
        }
        /// <summary>
        /// 获取唯一实例，双锁定防止多线程并发时重复创建实例
        /// </summary>
        /// <returns></returns>
        public static AppContextExt GetInstance()
        {
            if (_ServiceContext == null)
            {
                lock (lockObj)
                {
                    if (_ServiceContext == null)
                    {
                        _ServiceContext = new AppContextExt();
                    }
                }
            }
            return _ServiceContext;
        }
        /// <summary>
        /// 注入Service到上下文
        /// </summary>
        /// <typeparam name="T">接口对象</typeparam>
        /// <param name="t">Service对象</param>
        /// <param name="servicesChangeEvent">服务实例更新时订阅的消息</param>
        public static void RegisterService<T>(T t, Action<object> servicesChangeEvent = null) where T : class
        {
            if (t == null)
            {
                throw new Exception(string.Format("未将对象实例化，对象名：{0}.", typeof(T).Name));
            }
            if (!Services.ContainsKey(typeof(T)))
            {
                try
                {
                    Services.Add(typeof(T), t);
                    if (servicesChangeEvent != null)
                    {
                        var eventList = new List<Action<object>>();
                        eventList.Add(servicesChangeEvent);
                        ServiceEvents.Add(typeof(T), eventList);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (!Services.ContainsKey(typeof(T)))
            {
                throw new Exception(string.Format("注册Service失败，对象名：{0}.", typeof(T).Name));
            }
        }
        /// <summary>
        /// 动态注入dll中的多个服务对象
        /// </summary>
        /// <param name="serviceRuntime"></param>
        public static void RegisterAssemblyServices(string serviceRuntime)
        {
            if (serviceRuntime.IndexOf(".dll") != -1 && !File.Exists(serviceRuntime))
                throw new Exception(string.Format("类库{0}不存在！", serviceRuntime));
            try
            {
                Assembly asb = Assembly.LoadFrom(serviceRuntime);
                var serviceList = asb.GetTypes().Where(t => t.GetCustomAttributes(typeof(ExportAttribute), false).Any()).ToList();
                if (serviceList != null && serviceList.Count > 0)
                {
                    foreach (var service in serviceList)
                    {
                        var ifc = ((ExportAttribute)service.GetCustomAttributes(typeof(ExportAttribute), false).FirstOrDefault()).ContractType;
                        //使用默认的构造函数实例化
                        var serviceObject = Activator.CreateInstance(service, null);
                        if (serviceObject != null)
                            Services.Add(ifc, serviceObject);
                        else
                            throw new Exception(string.Format("实例化对象{0}失败！", service));
                    }
                }
                else
                {
                    throw new Exception(string.Format("类库{0}里没有Export的Service！", serviceRuntime));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取Service的实例
        /// </summary>
        /// <typeparam name="T">接口对象</typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            if (Services.ContainsKey(typeof(T)))
                return (T)Services[typeof(T)];
            return default(T);
        }
        /// <summary>
        /// 重置Service对象,实现热更新
        /// </summary>
        /// <typeparam name="T">接口对象</typeparam>
        /// <param name="t">新的服务对象实例</param>
        public static void ReLoadService<T>(T t)
        {
            if (t == null)
            {
                throw new Exception(string.Format("未将对象实例化，对象名：{0}.", typeof(T).Name));
            }
            if (Services.ContainsKey(typeof(T)))
            {
                try
                {
                    Services[typeof(T)] = t;
                    if (ServiceEvents.ContainsKey(typeof(T)))
                    {
                        var eventList = ServiceEvents[typeof(T)];
                        foreach (var act in eventList)
                        {
                            act.Invoke(t);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else if (!Services.ContainsKey(typeof(T)))
            {
                throw new Exception(string.Format("Service实例不存在！对象名：{0}.", typeof(T).Name));
            }
        }
        /// <summary>
        /// 激活上下文
        /// </summary>
        public void Start()
        {
            GetInstance();
        }
        /// <summary>
        /// 激活上下文
        /// </summary>
        /// <param name="appContext">真正的ApplicationContext对象</param>
        public void Start(ApplicationContext appContext)
        {
            Application_Context = appContext;
            GetInstance();
        }
        /// <summary>
        /// 激活上下文
        /// </summary>
        /// <param name="config">Configuration</param>
        public void Start(Configuration config)
        {
            AppConfig = config;
            GetInstance();
        }
        /// <summary>
        /// 激活上下文
        /// </summary>
        /// <param name="appContext">真正的ApplicationContext对象</param>
        /// <param name="config">Configuration</param>
        public void Start(ApplicationContext appContext, Configuration config)
        {
            AppConfig = config;
            Application_Context = appContext;
            GetInstance();
        }
        /// <summary>
        /// Using支持
        /// </summary>
        public void Dispose()
        {
            Services.Clear();
            ServiceEvents.Clear();
            if (Application_Context != null)
            {
                Application_Context.ExitThread();
            }
        }
    }

    public class ApplicationContext
    {
    }

    public class Configuration
    {
    }
}
