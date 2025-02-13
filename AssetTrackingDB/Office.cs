using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssetTrackingDB
{
	public class Office
	{
        [Key]
        public int OfficeId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int CurrencyId { get; set; }

        public Currency Currency { get; set; }
        public List<Asset> AssetList { get; set; }

        public Office()
        {
        }

        public static List<Office> getOfficeList()
        {
            using (var context = new MyDbContext())
            {
                return context.Offices.Include(x => x.Currency).ToList();
            }
        }
    }

   
}

