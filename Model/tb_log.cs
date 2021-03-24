using Frameworks.SqlMapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 数据库表是tb_log  但是程序是tb_log_model
    /// </summary>
    [LcTable("tb_log")]
    public class tb_log_model : BaseModel
    {
        /// <summary>
        /// 数据库字段是mqpathid  但是程序是mqpath_id   
        /// </summary>
        [LcCloumn("mqpathid")]
        public int mqpath_id { get; set; }
        public string mqpath { get; set; }
        public string methodname { get; set; }
        public string info { get; set; }
        public DateTime createtime { get; set; }
    }
}
