using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Base
{
    /// <summary>
    /// Abstract response class
    /// </summary>
    /// <typeparam name="T">Data object type</typeparam>
    public abstract class SuccessResponse<T>: Response
    {
        /// <summary>
        /// Public constractor for successful response object
        /// </summary>
        /// // <param name="metadata">contains the metadata object for the current response</param>
        /// <param name="data">contains the data object for the response</param>
        public SuccessResponse(Metadata metadata, T data)
            : base(metadata)
        {
            metadata.Success = true;
            Metadata = metadata;
            Data = data;
        }
        /// <summary>
        /// contains the data object for the response
        /// </summary>
        public T Data { get; set; }
    }    
}
