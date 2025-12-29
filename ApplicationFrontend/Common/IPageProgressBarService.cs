using System;

namespace Application_Frontend.Common
{
    public interface IPageProgressBarService
    {
        /// <summary>
        /// Update this to change the progress bar state
        /// </summary>
        bool IsLoading { get; set; }

        /// <summary>
        /// Subscribe to this event to listen for updates
        /// </summary>
        event Action OnUpdate;
    }
}
