using ETLService.OptionProvider;

namespace ETLService
{
    public abstract class EtlBuilder
    {
        protected IExtractable _extractionStage;

        protected OptionsProvider _optionsProvider;
        public EtlBuilder(OptionsProvider provider)
        {
            _optionsProvider = provider;
        }
        public abstract void SetExtractionStage();
        public abstract void SetAdditionalUtilities();
        public abstract ETL GetResult();

    }
}
