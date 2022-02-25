using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementApi.DAL.Seeds
{
    static public class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<RefBookingStatus>().HasData(
            //                    new RefBookingStatus { BookingStatusCode = 1, BookingStatusDescription = "Confirmed" },
            //                    new RefBookingStatus { BookingStatusCode = 2, BookingStatusDescription = "Provisional" }
            //                    );
        }
    }
}
