﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.Base
{
    public abstract class Response
    {
        //parameterless constructor for xmlSerializer
        private Response() { }

        protected Response(Metadata metadata)
        {
            Metadata = metadata;
        }

        public Metadata Metadata { get; set; }
    }
}
