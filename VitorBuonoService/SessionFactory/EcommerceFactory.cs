﻿using NHibernate;

namespace VitorBuonoService.SessionFactory
{
    public static class EcommerceFactory
    {
        private static ISessionFactory factory = null;

        public static ISessionFactory Factory
        {
            get
            {
                if (factory == null)
                {
                    NHibernate.Cfg.Configuration cfg = new NHibernate.Cfg.Configuration();
                    cfg.Properties.Add(NHibernate.Cfg.Environment.ConnectionDriver, "NHibernate.Driver.MySqlDataDriver");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.Dialect, "NHibernate.Dialect.MySQLDialect");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.BatchSize, "500");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.CommandTimeout, "10000");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.ShowSql, "true");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.FormatSql, "true");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.ConnectionString, System.Configuration.ConfigurationManager.ConnectionStrings["EcommerceDSN"].ConnectionString);
                    cfg.AddAssembly("ERP.Entity.Mapping");
                    factory = cfg.BuildSessionFactory();
                }
                return factory;
            }
        }
    }
}