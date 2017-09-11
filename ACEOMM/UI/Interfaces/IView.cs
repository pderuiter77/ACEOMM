using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACEOMM.UI.Interfaces
{
    public interface IView
    {
        void ShowMessage(string text);
        void ShowError(string text);
        bool AskYesNoConfirmation(string text);
        bool AskOkCancelConfirmation(string text);
        bool? AskYesNoCancelConfirmation(string text);
        void Close();
    }
}
