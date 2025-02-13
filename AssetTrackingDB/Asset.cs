using System;
using System.Globalization;
using Mini_Project_v49_Asset_Tracking;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetTrackingDB
{
	public class Asset
	{
        [Key]
        public int AssetId { get; set; } 
        public string Category { get; set; } // Asset Category Table
        public string Brand { get; set; } 
        public string Model { get; set; }
        public string Configuration { get; set; }
        public string PurchaseDate { get; set; }
        public float Price { get; set; }
        public int OfficeId { get; set; }

        public Office Office { get; set; } // Office Table

        public Asset()
		{
		}

        public static List<Asset> getAssetList()
        {
            using (var context = new MyDbContext())
            {
                return context.Assets.Include(x => x.Office).ThenInclude(o => o.Currency).ToList();
            }
        }

        
    }
}

