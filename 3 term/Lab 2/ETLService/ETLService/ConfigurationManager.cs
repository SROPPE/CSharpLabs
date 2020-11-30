namespace ETLService
{
    class ConfigurationManager
    {
        private EtlBuilder _etlBuilder;

        public ConfigurationManager(EtlBuilder builder)
        {
            _etlBuilder = builder;
        }
        public void Construct()
        {
            _etlBuilder.SetAdditionalUtilities();
            _etlBuilder.SetExtractionStage();
        }
    }
}
