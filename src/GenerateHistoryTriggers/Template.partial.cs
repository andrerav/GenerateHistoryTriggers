using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateHistoryTriggers
{
    /// <summary>
    /// A superclass for the trigger template that lets us specify inputs for the template
    /// </summary>
    /// <param name="tables"></param>
    public partial class Template(List<HistoryTableDefinition> tables)
    {
        public List<HistoryTableDefinition> Tables { get; } = tables;
    }
}
