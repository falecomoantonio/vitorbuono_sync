using System;
using System.ServiceProcess;

namespace VitorBuonoService
{
    class Program
    {
        #region Nested classes to support running as service
        public const string ServiceName = "VitorBuonoService";
        static IDisposable timerService;
        static bool stop = false;

        public class Service : System.ServiceProcess.ServiceBase
        {
            public Service()
            {
                ServiceName = ServiceName;
            }

            protected override void OnStart(string[] args)
            {
                Program.OnStart(args);
            }

            protected override void OnStop()
            {
                Program.OnStop();
            }
        }
        #endregion

        internal static void OnStart(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            timerService = new TimerService();
        }
        internal static void OnStop()
        {
            stop = true;
            timerService.Dispose();
        }

        #region Replace For Service Install
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                using (var service = new Service())
                {
                    ServiceBase.Run(service);
                    Console.Read();
                }
            }
            else if (args[0].Equals("-r", StringComparison.InvariantCulture))
            {
                try
                {
                    OnStart(null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Falha ao iniciar o serviço de Sincronização.");
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            }

            else if (args[0].Equals("-i", StringComparison.InvariantCultureIgnoreCase))
            {
                using (VitorBuonoServiceInstaller installer = new VitorBuonoServiceInstaller())
                {
                    installer.Install();
                }
            }
            else if (args[0].Equals("-u", StringComparison.InvariantCultureIgnoreCase))
            {
                using (VitorBuonoServiceInstaller installer = new VitorBuonoServiceInstaller())
                {
                    installer.Uninstall();
                }
            }
            else
            {
                Console.WriteLine("Comando inválido.");
            }
        }
        #endregion
    }
}
