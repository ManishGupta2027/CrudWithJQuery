using CrudOperation.Entities;
using CrudOperation.Entities.Trays;
using CrudOperation.Models.Tray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation.Service.Tray
{
    public interface ITrayService
    {
        IList<Entities.Trays.Tray> GetTrays(Guid orgId, Guid domainId, int? currentPage = null, int? pageSize = null, string name = null, Guid? deliveryCenterId = null);
        Entities.Trays.Tray GetTrayDetail(Guid orgId, Guid domainId, Guid id);
        BoolResponse UpsertTray(Guid orgId, Guid domainId, Entities.Trays.Tray model, string savedBy);
        BoolResponse DeleteTray(Guid orgId, Guid domainId, Guid id);
        BoolResponse UpdateTrayBarCode(Guid orgId, Guid domainId, Guid id, string barCodeUrl, string savedBy);
        List<TrayLabelPDF> GetTrayLabelPdfData(Guid orgId, List<TrayForBarCodePrint> tray);

        IList<TrayGroup> GetTrayGroups(Guid orgId, Guid domainId, Guid deliveryCenterId, int? currentPage = null, int? pageSize = null, string name = null);
        BoolResponse DeleteTrayGroup(Guid orgId, Guid domainId, Guid recordId, string savedBy);

        BoolResponse UpsertTrayGroup(Guid orgId, Guid domainId, TrayGroup model, string savedBy);

        TrayGroup GetTrayGroupDetail(Guid orgId, Guid domainId, Guid id);

        IList<TrayListModel> GetAvailableTrays(Guid orgId, Guid domainId, Guid deliveryCenterId,int ? currentPage = null, int? pageSize = null, string name = null);

    }
}