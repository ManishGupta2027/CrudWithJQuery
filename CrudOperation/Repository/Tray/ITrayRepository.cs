using CrudOperation.Entities.Trays;
using System;
using System.Collections.Generic;
using CrudOperation.Entities;

namespace CrudOperation.Repository.Tray
{
    public interface ITrayRepository
    {
        IList<Entities.Trays.Tray> GetTrays(Guid orgId, Guid domainId, int? currentPage = null, int? pageSize = null, string name = null, Guid? deliveryCenterId = null);
        Entities.Trays.Tray GetTrayDetail(Guid orgId, Guid domainId, Guid id);
        BoolResponse UpsertTray(Guid orgId, Guid domainId, Entities.Trays.Tray model, string savedBy);
        BoolResponse DeleteTray(Guid orgId, Guid domainId, Guid id);
        List<TrayLabelPDF> GetTrayLabelPdfData(Guid orgId, List<TrayForBarCodePrint> tray);
        BoolResponse UpdateTrayBarCode(Guid orgId, Guid domainId, Guid id, string barCodeUrl, string savedBy);
    }
}