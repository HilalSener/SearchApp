using System;
using System.Collections.Generic;
using System.Text;

namespace SearchApp.Models
{
    public class ReviewInformationModel
    {
        public IList<object> Reviews { get; set; }
        public ReviewSummaryModel ReviewSummary { get; set; }
    }
}
