﻿using Loxodon.Framework.Binding;
using Loxodon.Framework.Bundles;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Binding.Converters;
using Loxodon.Framework.Messaging;
using Loxodon.Framework.Services;
using Loxodon.Framework.Views;
using Loxodon.Framework.Views.InteractionActions;
using Loxodon.Log;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

namespace LX
{
    public class Launcher : MonoBehaviour
    {
        //private static readonly ILog log = LogManager.GetLogger(typeof(Launcher));

         //private List<Loading> list = new List<Loading>();

        private void Awake()
        {
            GlobalWindowManager windowManager = FindObjectOfType<GlobalWindowManager>();
            if (windowManager == null)
            {
                throw new NotFoundException("Can't find the GlobalWindowManager.");
            }

            //DontDestroyOnLoad(this);

            ApplicationContext context = ApplicationContext.GetApplicationContext();
            IServiceContainer container = context.GetContainer();

            /*初始化数据绑定服务*/
            BindingServiceBundle bundle = new BindingServiceBundle(context.GetContainer());
            bundle.Start();

            /* Initialize the ui view locator and register UIViewLocator */
            container.Register<IUIViewLocator>(new ResourcesViewLocator());

            /*网络消息分析器注册*/
            IMessageDispatcher messageDispatcher = new MessageDispatcher();
            container.Register<IMessageDispatcher>(messageDispatcher);

            /*初始化网络连接*/
            ISession session = new Session();
            container.Register<ISession>(session);
            //session.Connect("127.0.0.1", 10001);

            SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("Atlas/Icon");
        
            IConverterRegistry converterRegistry = context.GetContainer().Resolve<IConverterRegistry>();
            converterRegistry.Register("spriteConverter", new SpriteConverter(spriteAtlas));

            SpriteAtlas merchandiseAtlas = Resources.Load<SpriteAtlas>("Atlas/Merchandise");
            converterRegistry.Register("merchandiseConverter", new SpriteConverter(merchandiseAtlas));

            SpriteAtlas wheelAtlas = Resources.Load<SpriteAtlas>("Atlas/Wheel");
            converterRegistry.Register("wheelConverter", new SpriteConverter(wheelAtlas));

            /*初始化定时器*/
            ITask taskContext = new TaskContext();
            container.Register<ITask>(taskContext);


            IRewardRepository rewardRepository = new RewardRepository();
            container.Register<IRewardRepository>(rewardRepository);

        }
        IEnumerator Start()
        {


            /* Create a window container */
            WindowContainer winContainer = WindowContainer.Create("MAIN");
            yield return null;

            IUIViewLocator locator = Context.GetApplicationContext().GetService<IUIViewLocator>();
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

            //ApplicationContext context = Context.GetApplicationContext();
            //ISession session = context.GetService<ISession>();
            //session.Connect("127.0.0.1", 10001);
        }

        private void Update()
        {
            //ApplicationContext context = ApplicationContext.GetApplicationContext();
            //ISession session = context.GetService<ISession>();
            //session.Update();
        }
    }

}


