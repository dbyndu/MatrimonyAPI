using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Base
{
    public class ErrorResponse: Response
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
