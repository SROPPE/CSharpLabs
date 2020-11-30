using ETLService.Option;
using ETLService.OptionProvider;
using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using Utilities;

namespace ETLService
{
    public partial class Service1 : ServiceBase
    {
        private ETL etl;
        public Service1()
        {
            InitializeComponent();
;
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }
        protected override void OnStart(string[] args)
        {
            InitializeEtl();
            Logger.Log("Start");
            Thread threadETL = new Thread(new ThreadStart(etl.Start));
            threadETL.Start();
        }

        private void InitializeEtl()
        {
            OptionsProvider optionsProvider = new EtlJsonOptions(typeof(EtlOptions),"D:\\config.json");
            EtlBuilder builder = new LabEtlBuilder(optionsProvider);
            ConfigurationManager configuration = new ConfigurationManager(builder);
       
            configuration.Construct();
            etl = builder.GetResult();

        }

        protected override void OnStop()
        {
            etl.Stop();
            Thread.Sleep(100);
        }
    }
}
