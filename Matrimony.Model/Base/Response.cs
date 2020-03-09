using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.Base
{
    public abstract class ResponseType
    {
        //parameterless constructor for xmlSerializer
        private ResponseType() { }

        protected ResponseType(Metadata metadata)
        {
            Metadata = metadata;
        }

        public Metadata Metadata { get; set; }
    }
}
