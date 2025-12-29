using System;
using System.Collections.Generic;
using System.Threading;
using log4stash.Extensions;
using log4stash.SmartFormatters;
using log4net.Appender;
using log4net.Core;
using log4stash.Authentication;
using log4stash.Bulk;
using log4stash.Configuration;
using log4stash.ElasticClient;
using log4stash.ErrorHandling;
using log4stash.FileAccess;
using log4stash.LogEvent;
using log4stash.Timing;

namespace log4stash
{

    /// <summary>
    /// log4stash
    /// this is get from that place
    /// https://github.com/urielha/log4stash
    /// 
    /// 
    /// <appender name="ElasticSearchAppender" type="log4stash.ElasticSearchAppender, log4stash">
    /// 	<Server>localhost</Server>
    /// 	<Port>9200</Port>
    /// 	<!-- optional: in case elasticsearch is located behind a reverse proxy the URL is like http://Server:Port/Path, default = empty string -->
    /// 	<Path>/es5</Path>
    /// 	<!-- The time zone for the formatter is based on the character before the index. '+' = local time, '~' = utc time -->
    /// 	<IndexName>log_test_%{+yyyy-MM-dd}</ IndexName >
    /// 
    /// < !--type support was removed in ElasticSearch 7, so if not defined in configuration there won't be a type in the request -->
    /// 	<IndexType>LogEvent</IndexType>
    /// 	<BulkSize>2000</BulkSize>
    /// 	<BulkIdleTimeout>10000</BulkIdleTimeout>
    /// 	<IndexAsync>False</IndexAsync>
    /// 	<DropEventsOverBulkLimit>False</DropEventsOverBulkLimit>
    /// 
    /// 	<!-- Serialize log object as json (default is true).
    ///       --This in case you log the object this way: `logger.Debug(obj);` and not: `logger.Debug("string");` -->
    /// 
    ///    < SerializeObjects > True </ SerializeObjects >
    /// 
    /// 
    ///    < !--optional: elasticsearch timeout for the request, default = 10000 -->
    /// 	<ElasticSearchTimeout>10000</ElasticSearchTimeout>
    /// 
    /// 	<!-- optional: ssl connection -->
    /// 	<Ssl>False</Ssl>
    /// 	<AllowSelfSignedServerCert>False</AllowSelfSignedServerCert>
    /// 
    /// 	<!--You can add parameters to the request to control the parameters sent to ElasticSearch.
    ///     for example, as you can see here, you can add a custom id source to the appender.
    ///     The Key is the key to be added to the request, and the value is the parameter's name in the log event properties.-->
    /// 	<IndexOperationParams>
    /// 		<Parameter>
    /// 			<Key>_id</Key>
    /// 			<Value>%{IdSource}</ Value >
    /// 
    ///         </ Parameter >
    /// 
    ///         < Parameter >
    ///             < Key > key </ Key >
    ///             < Value > value </ Value >
    ///         </ Parameter >
    /// 
    ///     </ IndexOperationParams >
    /// 
    /// 
    ///     < !-- for more information read about log4net.Core.FixFlags-- >
    ///  
    ///      < FixedFields > Partial </ FixedFields >
    ///  
    /// 
    ///      < Template >
    ///  
    ///          < Name > templateName </ Name >
    ///          < FileName > path2template.json </ FileName >
    ///  
    ///      </ Template >
    ///  
    /// 
    ///      < !--Only one credential type can be used at once-- >
    ///  
    ///      < !--Here we list all possible types-- >
    ///  
    ///      < AuthenticationMethod >
    ///  
    ///          < !--For basic authentication purposes-- >
    ///  
    ///          < Basic >
    ///              < Username > Username </ Username >
    ///              < Password > Password </ Password >
    ///          </ Basic >
    ///  
    ///          < !--For AWS ElasticSearch service-- >
    ///          < Aws >
    ///              < Aws4SignerSecretKey > Secret </ Aws4SignerSecretKey >
    ///              < Aws4SignerAccessKey > AccessKey </ Aws4SignerAccessKey >
    ///              < Aws4SignerRegion > Region </ Aws4SignerRegion >
    ///          </ Aws >
    ///  
    ///          < !--For Api Key(X - Pack) authentication-- >
    ///  
    ///          < ApiKey >
    ///              < !--ApiKeyBase64 takes precedence over Id / ApiKey-- >
    ///              < ApiKeyBase64 > aWQ6YXBpa2V5 </ ApiKey >
    ///  
    ///              < !--Or-- >
    ///  
    ///              < Id > id </ Id >
    ///              < ApiKey > apikey </ ApiKey >
    ///  
    ///          </ ApiKey >
    ///  
    ///      </ AuthenticationMethod >
    ///  
    /// 
    ///      < !--all filters goes in ElasticFilters tag-- >
    ///  
    ///      < ElasticFilters >
    ///  
    ///          < Add >
    ///              < Key > @type </ Key >
    ///              < Value > Special </ Value >
    ///          </ Add >
    ///  
    /// 
    ///          < !-- using the @type value from the previous filter -->
    /// 		<Add>
    /// 			<Key>SmartValue</Key>
    /// 			<Value>the type is %{@type}</ Value >
    ///         </ Add >
    /// 
    /// 
    ///         < Remove >
    ///             < Key > @type </ Key >
    ///         </ Remove >
    /// 
    /// 
    ///         < !--you can load custom filters like I do here -->
    /// 		<Filter type="log4stash.Filters.RenameKeyFilter, log4stash">
    /// 			<Key>SmartValue</Key>
    /// 			<RenameTo>SmartValue2</RenameTo>
    /// 		</Filter>
    /// 
    /// 		<!-- converts a json object to fields in the document -->
    /// 		<Json>
    /// 			<SourceKey>JsonRaw</SourceKey>
    /// 			<FlattenJson>false</FlattenJson>
    /// 			<!-- the separator property is only relevant when setting the FlattenJson property to 'true' -->
    /// 			<Separator>_</Separator> 
    /// 		</Json>
    /// 
    /// 		<!-- converts an xml object to fields in the document -->
    /// 		<Xml>
    /// 			<SourceKey>XmlRaw</SourceKey>
    /// 			<FlattenXml>false</FlattenXml>
    /// 		</Xml>
    /// 
    /// 		<!-- kv and grok filters similar to logstash's filters -->
    /// 		<Kv>
    /// 			<SourceKey>Message</SourceKey>
    /// 			<ValueSplit>:=</ ValueSplit >
    ///             < FieldSplit > ,</ FieldSplit >
    ///           </ kv >
    ///   
    ///           < Grok >
    ///               < SourceKey > Message </ SourceKey >
    ///               < Pattern > the message is %{ WORD: Message}
    /// and guid %{UUID:the_guid}</ Pattern >
    /// 
    /// < Overwrite > true </ Overwrite >
    /// 
    /// </ Grok >
    /// 
    /// 
    /// < !--Convert string like: "1,2, 45 9" into array of numbers [1,2,45,9] -->
    /// 
    /// < ConvertToArray >
    /// 
    /// < SourceKey > someIds </ SourceKey >
    /// 
    /// < !--The separators(space and comma)-- >
    /// 
    /// < Seperators >, </ Seperators >
    /// 
    /// </ ConvertToArray >
    /// 
    /// 
    /// < Convert >
    /// 
    /// < !--convert given key to string -->
    /// 			<ToString>shouldBeString</ToString>
    /// 
    /// 			<!-- same as ConvertToArray. Just for convenience -->
    /// 			<ToArray>
    /// 				<SourceKey>anotherIds</SourceKey>
    /// 			</ToArray>
    /// 		</Convert>
    /// 	</ElasticFilters>
    /// </appender>
    /// 
    /// 
    /// </summary>

    public class ElasticSearchAppender : AppenderSkeleton
    {

        private readonly ILogBulkSet _bulk;
        private readonly IFileAccessor _fileAccessor;
        private readonly IExternalEventWriter _eventWriter;
        private IElasticsearchClient _client;
        private readonly IElasticClientFactory _elasticClientFactory;
        private LogEventSmartFormatter _indexName;
        private LogEventSmartFormatter _indexType;
        private TolerateCallsBase _tolerateCalls;
        private ILogEventConverter _logEventConverter;
        private readonly ILogEventConverterFactory _logEventConverterFactory;
        private readonly IIndexingTimer _timer;
        private readonly ITolerateCallsFactory _tolerateCallsFactory;

        
        public FixFlags FixedFields { get; set; }
        public bool SerializeObjects { get; set; }
        public IndexOperationParamsDictionary IndexOperationParams { get; set; }
        public int BulkSize { get; set; }
        public int BulkIdleTimeout { get; set; }
        public int TimeoutToWaitForTimer { get; set; }

        public int TolerateLogLogInSec
        {
            set
            {
                _tolerateCalls = _tolerateCallsFactory.Create(value);
            }
        }

        // elastic configuration
        public string Server { get; set; }
        public int Port { get; set; }
        public string Path { get; set; }
        public string CertPath { get; set; }
        public string CertPass { get; set; }
        public ServerDataCollection Servers { get; set; }
        public int ElasticSearchTimeout { get; set; }
        public bool Ssl { get; set; }
        public bool AllowSelfSignedServerCert { get; set; }
        public AuthenticationMethodChooser AuthenticationMethod { get; set; }
        public bool IndexAsync { get; set; }
        public TemplateInfo Template { get; set; }
        public ElasticAppenderFilters ElasticFilters { get; set; }
        public bool DropEventsOverBulkLimit { get; set; }

        public string IndexName
        {
            set { _indexName = value; }
            get { return _indexName.ToString();  }
        }

        public string IndexType
        {
            set { _indexType = value; }
            get { return _indexType.ToString(); }
        }

        public ElasticSearchAppender()
            : this(new WebElasticClientFactory(), "LogEvent-%{+yyyy.MM}",
                string.Empty, new IndexingTimer(Timeout.Infinite) { WaitTimeout = 5000 },
                new TolerateCallsFactory(), new LogBulkSet(),
                new BasicLogEventConverterFactory(), new ElasticAppenderFilters(), 
                new BasicFileAccessor(), new LogLogEventWriter())
        {
        }

        public ElasticSearchAppender(IElasticClientFactory clientFactory, LogEventSmartFormatter indexName,
            LogEventSmartFormatter indexType, IIndexingTimer timer, ITolerateCallsFactory tolerateCallsFactory,
            ILogBulkSet bulk, ILogEventConverterFactory logEventConverterFactory, ElasticAppenderFilters elasticFilters,
            IFileAccessor fileAccessor, IExternalEventWriter eventWriter)
        {
            _logEventConverterFactory = logEventConverterFactory;
            _elasticClientFactory = clientFactory;
            IndexName = indexName;
            IndexType = indexType;
            _timer = timer;
            _timer.Elapsed += (o,e) => DoIndexNow();
            _tolerateCallsFactory = tolerateCallsFactory;
            _bulk = bulk;
            _fileAccessor = fileAccessor;
            _eventWriter = eventWriter;

            FixedFields = FixFlags.Partial;
            SerializeObjects = true;
            BulkSize = 2000;
            BulkIdleTimeout = 5000;
            DropEventsOverBulkLimit = false;
            TimeoutToWaitForTimer = 5000;
            ElasticSearchTimeout = 10000;
            IndexAsync = true;
            Template = null;
            AllowSelfSignedServerCert = false;
            Ssl = false; 
            _tolerateCalls = _tolerateCallsFactory.Create(0);
            Servers = new ServerDataCollection();
            ElasticFilters = elasticFilters;
            AuthenticationMethod = new AuthenticationMethodChooser();
            IndexOperationParams = new IndexOperationParamsDictionary();
        }

        public override void ActivateOptions()
        {
            AddOptionalServer();

            AuthenticationMethod.UpdateEventWriter(_eventWriter);

            _client = _elasticClientFactory.CreateClient(Servers, ElasticSearchTimeout, Ssl, AllowSelfSignedServerCert, _eventWriter, AuthenticationMethod);

            _logEventConverter = _logEventConverterFactory.Create(FixedFields, SerializeObjects);

            if (Template != null && Template.IsValid)
            {
                if (IndexAsync)
                {
                    _client.PutTemplateRaw(Template.Name, _fileAccessor.ReadAllText(Template.FileName));
                }
                else
                {
                    _client.PutTemplateRaw(Template.Name, _fileAccessor.ReadAllText(Template.FileName));
                }
            }

            ElasticFilters.PrepareConfiguration(_client);

            _timer.Restart(BulkIdleTimeout);
        }

        private void AddOptionalServer()
        {
            if (!string.IsNullOrEmpty(Server) && Port != 0)
            {
                var serverData = new ServerData { Address = Server, Port = Port, Path = Path, CertPath = CertPath, CertPass = CertPass };
                Servers.Add(serverData);
            }
        }

        /// <summary>
        /// On case of error or when the appender is closed before loading configuration change.
        /// </summary>
        protected override void OnClose()
        {
            DoIndexNow();

            // let the timer finish its job
            _timer.Dispose();
            if (_client != null)
            {
                _client.Dispose();
            }
        }

        /// <summary>
        /// Add a log event to the ElasticSearch Repo
        /// </summary>
        /// <param name="loggingEvent"></param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            if (_client == null || loggingEvent == null)
            {
                return;
            }

            if (DropEventsOverBulkLimit && _bulk.Count >= BulkSize)
            {
                _tolerateCalls.Call(() =>
                    _eventWriter.Warn(GetType(),
                        "Message lost due to bulk overflow! Set DropEventsOverBulkLimit to false in order to prevent that."),
                    GetType(), 0);
                return;
            }

            var logEvent = _logEventConverter.ConvertLogEventToDictionary(loggingEvent);
            PrepareAndAddToBulk(logEvent);

            if (!DropEventsOverBulkLimit && _bulk.Count >= BulkSize && BulkSize > 0)
            {
                DoIndexNow();
            }
        }

        /// <summary>
        /// Prepare the event and add it to the BulkDescriptor.
        /// </summary>
        /// <param name="logEvent"></param>
        private void PrepareAndAddToBulk(Dictionary<string, object> logEvent)
        {
            logEvent.ApplyFilter(ElasticFilters);
            _bulk.AddEventToBulk(logEvent, _indexName, _indexType, IndexOperationParams);
        }

        /// <summary>
        /// Send the bulk to Elasticsearch and creating new bluk.
        /// </summary>
        private void DoIndexNow()
        {
            var bulkToSend = _bulk.ResetBulk();
            if (bulkToSend != null && bulkToSend.Count <= 0) return;
            try
            {
                if (IndexAsync)
                {
                    _client.IndexBulkAsync(bulkToSend);
                }
                else
                {
                    _client.IndexBulk(bulkToSend);
                }
            }
            catch (Exception ex)
            {
                _eventWriter.Error(GetType(), "IElasticsearchClient inner exception occurred", ex);
            }
        }
    }
}
