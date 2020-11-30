using System;
using ETLService.Extraction;
using ETLService.OptionProvider;
using ETLService.Option;
using Utilities;

namespace ETLService
{
    public class LabEtlBuilder : EtlBuilder
    {
        public LabEtlBuilder(OptionsProvider optionProvider) : base(optionProvider)
        {

        }
        public override ETL GetResult()
        {
            return ETL.Create(_extractionStage);
        }
        public override void SetExtractionStage()
        {
            _extractionStage = new ExtractionStage();

            try
            {
                _extractionStage.Options = _optionsProvider.GetOption<ExtractionOptions>().Value;
            }
            catch (Exception exc)
            {
                _extractionStage.Options = new ExtractionOptions();
                Logger.Log(exc.Message);
            }
        }

        public override void SetAdditionalUtilities()
        {
            SetLoggerOptions();
            SetArchiverOptions();
        }

        private void SetLoggerOptions()
        {

            try
            {
                Logger.options = _optionsProvider.GetOption<LoggerOptions>().Value;
            }
            catch (Exception exc)
            {
                Logger.options = new LoggerOptions();
                Logger.Log(exc.Message);
            }
        }
        private void SetArchiverOptions()
        {
            try
            {
                Archiver.options = _optionsProvider.GetOption<ArchiveOptions>().Value;
            }
            catch (Exception exc)
            {
                Archiver.options = new ArchiveOptions();
                Logger.Log(exc.Message);
            }
        }
    }
}