using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingApp.Business;
using TrainingApp.Data.Mock;
using TrainingApp.Data.SqlServer;
using TrainingApp.Shared.Interfaces;

namespace TrainingApp.StructureMap
{
    public class ScanningRegistry : Registry
    {
        public ScanningRegistry()
        {
            // Usually this value should be taken from web.config, but it's hardcoded here for simplicity
            //var databaseConnectionString = "testconnectionstring";

            //this.For<ISalesData>().Use<TrainingAppDataMock>();
            this.For<IProductData>().Use<ProductData>();
            this.For<ISalesData>().Use<SalesData>();
            this.For<IProductInformation>().Use<ProductInformation>();
            this.For<ISalesInformation>().Use<SalesInformation>();

            //this.Policies.SetAllProperties(y => y.WithAnyTypeFromNamespaceContainingType<TrainingAppDataMock>());

            this.Policies.SetAllProperties(c =>
            {
                c.WithAnyTypeFromNamespaceContainingType<IProductData>();
                c.WithAnyTypeFromNamespaceContainingType<ISalesData>();
                c.WithAnyTypeFromNamespaceContainingType<IProductInformation>();
                c.WithAnyTypeFromNamespaceContainingType<ISalesInformation>();
            });
        }
    }
}