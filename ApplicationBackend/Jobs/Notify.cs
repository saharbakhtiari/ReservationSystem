//using Application.Common.QueueManagers;
//using Domain.Common;
//using Domain.Externals.NotifyServer;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Application_Backend.Rules.Commands.AddRuleVersion
//{
//    public partial class AddRuleVersionCommandHandler
//    {
//        public class Notify : IJobService//, IDisposable
//        {
//            private readonly IQueueManager<SendNotification> _queueManager;

//            public Notify(IQueueManager<SendNotification> queueManager)
//            {
//                _queueManager = queueManager;
//            }

//            public async Task RunAsync(CancellationToken cancellationToken)
//            {
//                //logger.LogInformation("Starting background e-mail delivery");
//                // The StartAsync method just needs to start a background task (or a timer)
//                //logger.LogInformation("E-mail background delivery started");

//                while (!cancellationToken.IsCancellationRequested)
//                {
//                    SendNotification message = null;
//                    try
//                    {
//                        // Let's wait for a message to appear in the queue
//                        // If the token gets canceled, then we'll stop waiting
//                        // since an OperationCanceledException will be thrown
//                        if (_queueManager.Count() > 0)
//                        {
//                            message = await _queueManager.ReceiveAsync(cancellationToken);

//                            // As soon as a message is available, we'll send it
//                            await message.SendAsync(cancellationToken);
//                        }

//                        //logger.LogInformation($"E-mail sent to {message.To}");
//                    }
//                    catch (OperationCanceledException)
//                    {
//                        // We need to terminate the delivery, so we'll just break the while loop
//                        break;
//                    }
//                    catch (Exception exc)
//                    {
//                        //logger.LogError(exc, "Couldn't send an e-mail to {recipient}", message.To[0]);
//                        // Just wait a second, perhaps the mail server was busy
//                        await Task.Delay(1000);
//                        // Then re-queue this email for later delivery
//                        //#warning Some email messages will be stuck in the loop if the SMTP server will always reject them because of their content
//                        await _queueManager.SendAsync(message, cancellationToken);
//                    }
//                }
//            }
//        }
//    }
//}
