using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApplicationInsights
{
    public interface IPageViewPerformanceTelemetry
    {
        /// <summary>
        /// name String - The name of the page. Defaults to the document title.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// url String - a relative or absolute URL that identifies the page or other item. Defaults to the window location.
        /// </summary>
        public string? Uri { get; set; }

        /// <summary>
        /// Performance total in TimeSpan 'G' (general long) format: d:hh:mm:ss.fffffff". This is total duration in timespan format.
        /// </summary>
        public string? PerfTotal { get; set; }

        /// <summary>
        /// Performance total in TimeSpan 'G' (general long) format: d:hh:mm:ss.fffffff". This represents the total page load time.
        /// </summary>
        public string? Duration { get; set; }

        /// <summary>
        /// Sent request time in TimeSpan 'G' (general long) format: d:hh:mm:ss.fffffff.
        /// </summary>
        public string? NetworkConnect { get; set; }

        /// <summary>
        /// Sent request time in TimeSpan 'G' (general long) format: d:hh:mm:ss.fffffff
        /// </summary>
        public string? SentRequest { get; set; }

        /// <summary>
        /// Received response time in TimeSpan 'G' (general long) format: d:hh:mm:ss.fffffff.
        /// </summary>
        public string? ReceivedResponse { get; set; }

        /// <summary>
        /// DOM processing time in TimeSpan 'G' (general long) format: d:hh:mm:ss.fffffff
        /// </summary>
        public string? DomProcessing { get; set; }
    }
}
