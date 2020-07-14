using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace VitorBuonoService
{
    [RunInstaller(true)]
    public sealed class ProcessInstaller : ServiceProcessInstaller
    {
        public ProcessInstaller() => this.Account = ServiceAccount.LocalSystem;
    }


    [RunInstaller(true)]
    [DesignerCategory("code")]
    public sealed class VitorBuonoServiceInstaller : ServiceInstaller
    {
        public VitorBuonoServiceInstaller()
        {
            this.StartType = ServiceStartMode.Automatic;
            this.Description = "Serviço de Sincronização Sanhkya x WooEcommerce";
            this.DisplayName = "Sincronizador ERP x Ecommerce";
            this.ServiceName = "SINCRONIZADOR_ERP_ECOMMERCE";
        }

        private void After_Install()
        {
            using (ServiceController sc = new ServiceController(this.ServiceName))
            {
                sc.Start();
                Console.WriteLine("Iniciando o Processo de Instalação, por favor aguarde...");
            }
        }


        public void Install()
        {
            Console.WriteLine("Instalando o Serviço de Sincronização, por favor aguarde...");
            IDictionary state = new Hashtable();
            try
            {
                using (AssemblyInstaller inst = new AssemblyInstaller(typeof(Program).Assembly, null))
                {

                    inst.UseNewContext = true;
                    try
                    {
                        inst.Install(state);
                        inst.Commit(state);
                        this.After_Install();
                    }
                    catch
                    {
                        try
                        {
                            inst.Rollback(state);
                        }
                        catch { }
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }


        public void Uninstall()
        {
            Console.WriteLine("Removendo o Serviço de Sincronização, por favor aguarde...");
            IDictionary state = new Hashtable();

            try
            {
                using (AssemblyInstaller inst = new AssemblyInstaller(typeof(Program).Assembly, null))
                {

                    inst.UseNewContext = true;
                    try
                    {
                        inst.Uninstall(state);
                        inst.Commit(state);
                    }
                    catch
                    {
                        try
                        {
                            inst.Rollback(state);
                        }
                        catch { }
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}