using CrudOperation.Models;
using CrudOperation.Models.Tray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation.Helper
{
    public static class DemoData
    {
        public class SessionDataModel
        {
            public Guid OrgId { get; set; }
            public Guid CurrentDomainId { get; set; }
            public string UserEmail { get; set; }
        }
        // Static Guids for consistent usage
        public static readonly Guid NorthDistributionCenterId = Guid.Parse("b3e9c0ad-10a8-4e5b-9fb0-0b1cb9123456");
        public static readonly Guid EastDistributionCenterId = Guid.Parse("f2e9c0ad-10a8-4e5b-9fb0-0b1cb9876543");
        public static readonly Guid WestDistributionCenterId = Guid.Parse("c1e9c0ad-10a8-4e5b-9fb0-0b1cb9012345");

        public static readonly Guid OrgId = Guid.Parse("a1234567-b890-4cde-f123-456789abcdef");
        public static readonly Guid DomainId = Guid.Parse("b1234567-b890-4cde-f123-456789abcdef");
        // public static readonly string MappedModel = "DemoModel123";
        public static readonly string UserEmail = "user@example.com";

        public static List<DeliveryCenterDetailModel> DeliveryCenters = new List<DeliveryCenterDetailModel>
                {
                    new DeliveryCenterDetailModel { DeliveryCenterId = NorthDistributionCenterId, Code = "DC001", DeliveryCenterName = "North Distribution Center" },
                    new DeliveryCenterDetailModel { DeliveryCenterId = EastDistributionCenterId, Code = "DC002", DeliveryCenterName = "East Distribution Center" },
                    new DeliveryCenterDetailModel { DeliveryCenterId = WestDistributionCenterId, Code = "DC003", DeliveryCenterName = "West Distribution Center" }
                };

        public static SessionDataModel _sessionContext = new SessionDataModel
        {
            OrgId = OrgId,
            CurrentDomainId = DomainId,
            UserEmail = UserEmail
        };

        public static readonly Guid Tray1 = Guid.Parse("a1234567-b890-4cde-f123-456789abcdef");
        public static readonly Guid Tray2 = Guid.Parse("b1234567-b890-4cde-f123-456789abcdef");
        public static readonly Guid Tray3 = Guid.Parse("c1234567-b890-4cde-f123-456789abcdef");
        public static readonly Guid Tray4 = Guid.Parse("d1234567-b890-4cde-f123-456789abcdef");
        public static readonly Guid Tray5 = Guid.Parse("e1234567-b890-4cde-f123-456789abcdef");
        public static readonly Guid Tray6 = Guid.Parse("f1234567-b890-4cde-f123-456789abcdef");
        public static readonly Guid Tray7 = Guid.Parse("6d9f8d2d-16ec-40ac-b452-6facf970a5d8");
        public static readonly Guid Tray8 = Guid.Parse("a3942c41-7a98-4dd5-ac50-d52ee1770de9");
        public static readonly Guid Tray9 = Guid.Parse("bd5216b9-ab85-4721-8be5-280e0292f9c4");
        public static readonly Guid Tray10 = Guid.Parse("f192d0ad-d9f9-4330-b22a-9c269d25f9ff");

        public static List<TrayListModel> TrayList = new List<TrayListModel>
        {
            new TrayListModel { RecordId = Tray1, Name = "Tray 1" },
            new TrayListModel { RecordId = Tray2, Name = "Tray 3" },
            new TrayListModel { RecordId = Tray3, Name = "Tray 4" },
            new TrayListModel { RecordId = Tray4, Name = "Tray 2" },
            new TrayListModel { RecordId = Tray5, Name = "Tray 5" },
            new TrayListModel { RecordId = Tray6, Name = "Tray 6" },
            new TrayListModel { RecordId = Tray7, Name = "Tray 7" },
            new TrayListModel { RecordId = Tray8, Name = "Tray 8" },
            new TrayListModel { RecordId = Tray9, Name = "Tray 9" },
            new TrayListModel { RecordId = Tray10, Name = "Tray 10" }
        };
    }
}