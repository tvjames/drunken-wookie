﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DrunkenWookie.Evictus;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace DrunkenWookie.Evictus
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            AppHost.Start();
        }
    }
}