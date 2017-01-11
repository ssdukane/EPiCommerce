﻿/*
Commerce Starter Kit for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System.Web.Mvc;
using EPiServer.Cms.Shell;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc.Html;
using EPICommerce.Core.Repositories;
using EPICommerce.Core.Repositories.Interfaces;
using EPICommerce.Core.Services;
using EPICommerce.Web.Business.Rendering;
using StructureMap;

namespace EPICommerce.Web.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(InitializableModule))]
    public class DependencyResolverInitialization : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            
            context.Container.Configure(ConfigureContainer);
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(context.Container));
            
        }

        private static void ConfigureContainer(ConfigurationExpression container)
        {
            
            //Swap out the default ContentRenderer for our custom
            //container.For<IContentRenderer>().Use<ErrorHandlingContentRenderer>();
            container.For<ContentAreaRenderer>().Use<ContentAreaWithDefaultsRenderer>();
            container.For<ICustomerAddressRepository>().Use<CustomerAddressRepository>();
            container.For<IOrderRepository>().Use<OrderRepository>();
	        container.For<IExportOrderService>().Use<ExportOrderService>();
        }

        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }
    }
}
