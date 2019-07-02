
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Bot.Builder.Dialogs
{
    public class GeneralDialogContext
    {
        public DialogSet Dialogs { get; }
        public ITurnContext Context { get; }
        public List<DialogInstance> Stack { get; }
        public DialogContext Parent { get; set; }
        public DialogInstance ActiveDialog { get; }

        //public Task<DialogTurnResult> ContinueActualDialogAsync(CancellationToken cancellationToken) {

       // }


    }
}