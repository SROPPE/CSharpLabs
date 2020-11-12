using ETLService.Extraction;
using System.ServiceProcess;
using System.Threading;

namespace ETLService
{
    public partial class Service1 : ServiceBase
    {
        private ETL etl;
        public Service1()
        {
            InitializeComponent();
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;

        }
        protected override void OnStart(string[] args)
        {
            etl = ETL.Create(new ExtractionStage());
            Thread threadETL = new Thread(new ThreadStart(etl.Start));
            threadETL.Start();
        }
        protected override void OnStop()
        {
            etl.Stop();
            Thread.Sleep(100);
        }
    }
}
