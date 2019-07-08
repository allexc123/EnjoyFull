using Loxodon.Framework.Binding;
using Loxodon.Framework.Bundles;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Services;
using Loxodon.Framework.Views;
using Loxodon.Framework.Views.InteractionActions;
using Loxodon.Log;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LX
{
    public class Launcher : MonoBehaviour
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Launcher));

        private void Awake()
        {
            GlobalWindowManager windowManager = FindObjectOfType<GlobalWindowManager>();
            if (windowManager == null)
            {
                throw new NotFoundException("Can't find the GlobalWindowManager.");
            }

            ApplicationContext context = ApplicationContext.GetApplicationContext();
            IServiceContainer container = context.GetContainer();

            /*初始化数据绑定服务*/
            BindingServiceBundle bundle = new BindingServiceBundle(context.GetContainer());
            bundle.Start();

            IResources resources = CreateResources();
            container.Register<IResources>(resources);

            /* Initialize the ui view locator and register UIViewLocator */
            container.Register<IUIViewLocator>(new ResourcesViewLocator());

            /*初始化网络连接*/
            //ISession session = new Session();
            //session.Start();
            //container.Register<ISession>(session);


        }
        IEnumerator Start()
        {


            /* Create a window container */
            WindowContainer winContainer = WindowContainer.Create("MAIN");

            yield return null;

            IUIViewLocator locator = ApplicationContext.GetApplicationContext().GetService<IUIViewLocator>();
            StartupWindow window = locator.LoadWindow<StartupWindow>(winContainer, "UI/Startup.prefab");
            window.Create();
            ITransition transition = window.Show().OnStateChanged((w, state) =>
            {
                //log.DebugFormat("Window:{0} State{1}",w.Name,state);
            });

            yield return transition.WaitForDone();


            //ApplicationContext context = ApplicationContext.GetApplicationContext();
            //ISession session = context.GetService<ISession>();
            //session.Send(10001, -1, new Login() { DriveId = "default" });
        }

        IResources CreateResources()
        {
            IResources resources = null;
#if UNITY_EDITOR
            if (SimulationSetting.IsSimulationMode)
            {
                Debug.Log("Use SimulationResources. Run In Editor");

                /* Create a PathInfoParser. */
                //IPathInfoParser pathInfoParser = new SimplePathInfoParser("@");
                IPathInfoParser pathInfoParser = new SimulationAutoMappingPathInfoParser();

                /* Create a BundleManager */
                IBundleManager manager = new SimulationBundleManager();

                /* Create a BundleResources */
                resources = new SimulationResources(pathInfoParser, manager);
            }
            else
#endif
            {

                /* Create a BundleManifestLoader. */
                IBundleManifestLoader manifestLoader = new BundleManifestLoader();

                /* Loads BundleManifest. */
                BundleManifest manifest = manifestLoader.Load(BundleUtil.GetStorableDirectory() + BundleSetting.ManifestFilename);

                //manifest.ActiveVariants = new string[] { "", "sd" };
                //manifest.ActiveVariants = new string[] { "", "hd" };

                /* Create a PathInfoParser. */
                IPathInfoParser pathInfoParser = new AutoMappingPathInfoParser(manifest);

                /* Use a custom BundleLoaderBuilder */
                ILoaderBuilder builder = new CustomBundleLoaderBuilder(new Uri(BundleUtil.GetReadOnlyDirectory()), false);

                /* Create a BundleManager */
                IBundleManager manager = new BundleManager(manifest, builder);

                /* Create a BundleResources */
                resources = new BundleResources(pathInfoParser, manager);
      
            }
            return resources;
        }

        private void Update()
        {
            //ApplicationContext context = ApplicationContext.GetApplicationContext();
            //ISession session = context.GetService<ISession>();
            //session.Update();
        }
    }

}


