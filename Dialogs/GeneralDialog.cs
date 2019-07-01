// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.3.0

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace BotWithLUIS.Dialogs
{
    public class GeneralDialog : CancelAndHelpDialog
    {
        public GeneralDialog()
            : base(nameof(GeneralDialog))
        {
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                ShowInfoStepAsync,
                ConfirmStepAsync,
                FinalStepAsync,
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> ShowInfoStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var details = (GlobalDetails)stepContext.Options;

            if (details.InfoRequest == null || details.isNewDialog == false)
            {
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("I can help you with your request. Tell me a bit more, please.") }, cancellationToken);
            }
            else
            {
                return await stepContext.NextAsync(details.InfoRequest, cancellationToken);
            }

        }
        private async Task<DialogTurnResult> ConfirmStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var details = (GlobalDetails)stepContext.Options;

            details.InfoRequest = (string)stepContext.Result;

            var msg = $"Please confirm, I am going to search something about: {details.InfoRequest}";

            return await stepContext.PromptAsync(nameof(ConfirmPrompt), new PromptOptions { Prompt = MessageFactory.Text(msg) }, cancellationToken);
         }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((bool)stepContext.Result)
            {
                var details = (GlobalDetails)stepContext.Options;

                return await stepContext.EndDialogAsync(details, cancellationToken);
            }
            else
            {
                return await stepContext.EndDialogAsync(null, cancellationToken);
            }
        }

    }
}
