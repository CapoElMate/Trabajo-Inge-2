using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface IEmails
    {
        Task SendDNIValidationConfirmation(string email, string userName, string userLastName);
        Task SendPagoConfirmation(string email, string userName, string userLastName);
        Task SendReservaCreationConfirmation(string email, string userName, string userLastName);
        Task SendRegisterConfirmation(string email, string userName, string userLastName);
        Task SendPermisoEspecialApproveConfirmation(string email, string userName, string userLastName, string permiso);
        Task SendPermisoEspecialRejectConfirmation(string email, string userName, string userLastName, string permiso, string reasonOfReject);
    }
}
