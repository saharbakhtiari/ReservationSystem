using log4net.Appender;
using log4net.Core;
using log4net.Util;
using Log4NetEmailAppender.Bulk;
using Log4NetEmailAppender.LogEvent;
using Log4NetEmailAppender.MailSender;
using Log4NetEmailAppender.Timing;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SedTeam.Logging
{

    public class EmailAppender : AppenderSkeleton
    {

        private readonly ILogBulkSet _bulk;
        private EmailSender _client;
        private ILogEventConverter _logEventConverter;
        private readonly ILogEventConverterFactory _logEventConverterFactory;
        private readonly IIndexingTimer _timer;


        public FixFlags FixedFields { get; set; }
        public bool SerializeObjects { get; set; }
        public int BulkSize { get; set; }
        public int BulkIdleTimeout { get; set; }
        public int TimeoutToWaitForTimer { get; set; }

        // elastic configuration
        public string EmailSubject { get; set; }
        public string EmailTitle { get; set; }
        public string EmailServer { get; set; }
        public int EmailPort { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public string EmailUser { get; set; }
        public string EmailPassword { get; set; }
        public string EmailSendTo { get; set; }
        public string EmailSendCC { get; set; }
        public string EmailSendFrom { get; set; }
        public string EmailSendFromName { get; set; }


        public EmailAppender()
            : this(new IndexingTimer(Timeout.Infinite) { WaitTimeout = 5000 }, new LogBulkSet(),
                new BasicLogEventConverterFactory())
        {
        }

        public EmailAppender(IIndexingTimer timer, ILogBulkSet bulk, ILogEventConverterFactory logEventConverterFactory)
        {
            _logEventConverterFactory = logEventConverterFactory;
            _timer = timer;
            _timer.Elapsed += (o, e) => DoIndexNow();
            _bulk = bulk;

            FixedFields = FixFlags.Partial;
            SerializeObjects = true;
            BulkSize = 2000;
            BulkIdleTimeout = 5000;
            TimeoutToWaitForTimer = 5000;
        }

        public override void ActivateOptions()
        {
            _client = new EmailSender(EmailSubject, EmailTitle)
            {
                EmailServer = EmailServer,
                EmailPassword = EmailPassword,
                EmailSendTo = EmailSendTo,
                EmailPort = EmailPort,
                EmailSendCC = EmailSendCC,
                EmailSendFrom = EmailSendFrom,
                EmailSendFromName = EmailSendFromName,
                EmailUser = EmailUser,
                UseDefaultCredentials = UseDefaultCredentials
            };

            _logEventConverter = _logEventConverterFactory.Create(FixedFields, SerializeObjects);

            _timer.Restart(BulkIdleTimeout);
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

            var logEvent = _logEventConverter.ConvertLogEventToDictionary(loggingEvent);
            PrepareAndAddToBulk(logEvent);

            if (_bulk.Count >= BulkSize && BulkSize > 0)
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
            _bulk.AddEventToBulk(logEvent);
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
                _ = _client.SendBulkAsync(bulkToSend);
            }
            catch (Exception ex)
            {
                LogLog.Error(GetType(), "IEmailSenderClient inner exception occurred", ex);
            }
        }
    }
}
