using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.Base
{
    public class Metadata
    {
        private Metadata() { }
        /// <summary>
        /// Public constractor for Metadata object
        /// </summary>
        /// <param name="success">True/False value to indicate the success/failure pof the request</param>
        /// <param name="responseId">Unique identifier for the resource</param>
        /// <param name="description">The description of the required resource</param>
        public Metadata(bool success, string responseId, string description)
        {
            Success = success;
            ResponseId = responseId;
            Description = description;
        }
        /// <summary>
        /// True/False value to indicate the success/failure pof the request
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Unique identifier for the resource
        /// </summary>
        public string ResponseId { get; set; }
        /// <summary>
        /// The description of the required resource
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The TotalRecordCount of the required resource
        /// </summary>
        public int TotalRecordCount { get; set; }

    }
}
