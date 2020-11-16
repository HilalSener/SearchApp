using System;
using System.Collections.Generic;
using System.Text;

namespace SearchApp.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public ReviewInformationModel ReviewInformation { get; set; }
        public IList<string> USPs { get; set; }
        public int AvailabilityState { get; set; }
        public double SalesPriceIncVat { get; set; }
        public string ProductImage { get; set; }
        public string CoolbluesChoiceInformationTitle { get; set; }
        public PromoIconModel PromoIcon { get; set; }
        public bool NextDayDelivery { get; set; }
        public int? ListPriceIncVat { get; set; }
        public double? ListPriceExVat { get; set; }
    }
}
