using ETLService.OptionProvider;
using System.ServiceProcess;
using System.Threading;

namespace ETLService
{
    public partial class Service : ServiceBase
    {
        private ETL etl;
        public Service()
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
            Thread threadETL = new Thread(new ThreadStart(etl.Start));
            threadETL.Start();
        }

        private void InitializeEtl()
        {
            OptionsProvider optionsProvider = new EtlJsonOptions("config.json");
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
