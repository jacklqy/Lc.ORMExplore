using Frameworks.SqlFilter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class BaseModel
    {
        [LcKey]
        public long id { get; set; }
    }
}
