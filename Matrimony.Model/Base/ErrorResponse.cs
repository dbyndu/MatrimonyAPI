using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.Base
{
    public class ErrorResponse: ResponseType
    {
        public ErrorResponse(Metadata metadata, List<Error> errors)
            :base(metadata)
        {
            metadata.Success = false;
            Metadata = metadata;
            Errors = errors;
        }

        public List<Error> Errors { get; set; }
    }

}
