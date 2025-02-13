using System;
using System.ComponentModel.DataAnnotations;

namespace AssetTrackingDB
{
	public class Currency
	{
        [Key]
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string ShortName { get; set; }

		public List<Office> OfficeList { get; set; }

        public Currency()
		{
		}
	}
}

