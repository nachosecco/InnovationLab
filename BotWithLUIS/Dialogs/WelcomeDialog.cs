// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.3.0

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Recognizers.Text.DataTypes.TimexExpression;

namespace BotWithLUIS.Dialogs
{
    public class WelcomeDialog : ComponentDialog
    {
        protected readonly IConfiguration Configuration;
        protected readonly ILogger Logger;
        protected string stopper;
        protected Boolean newChat;

        public WelcomeDialog(IConfiguration configuration, ILogger<WelcomeDialog> logger)
            : base(nameof(WelcomeDialog))
        {
            Configuration = configuration;
            Logger = logger;
            stopper = "stop";
            newChat = true;

            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                IntroStepAsync,
                ActStepAsync,
                FinalStepAsync,
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> IntroStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(Configuration["LuisAppId"]) || string.IsNullOrEmpty(Configuration["LuisAPIKey"]) || string.IsNullOrEmpty(Configuration["LuisAPIHostName"]))
            {
                await stepContext.Context.SendActivityAsync(
                    MessageFactory.Text("NOTE: LUIS is not configured. To enable all capabilities, add 'LuisAppId', 'LuisAPIKey' and 'LuisAPIHostName' to the appsettings.json file."), cancellationToken);

                return await stepContext.NextAsync(null, cancellationToken);
            }
            else
            {
                if(!newChat || stepContext.Context.Activity.Label == "keep_talking")
                {
                    return await stepContext.NextAsync(stepContext, cancellationToken);
                }
                else
                {

                    var responseMessage = stepContext.Context.Activity.Text.ToLower();

                    if(responseMessage == stopper)
                    {
                        newChat = true;
                        return await FinishConversation(stepContext, cancellationToken);
                    }
                    else
                    {
                        newChat = false;
                        await stepContext.Context.SendActivityAsync(MessageFactory.Text("My name is Chatboarding, if you share your thoughts with me, I can help you! Type \'STOP\' anytime you wanna leave."));
                        return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("Tell me something challenging, come on...") }, cancellationToken);
                    }

                }
            }
        }

        private async Task<DialogTurnResult> ActStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var responseMessage = stepContext.Context.Activity.Text.ToLower();
            if(responseMessage == stopper)
            {
                return await FinishConversation(stepContext, cancellationToken);
            }
            else
            {
                // Call LUIS and gather any potential details. (Note the TurnContext has the response to the prompt.)
                var globalDetails = stepContext.Result != null
                    ?
                await LuisHelper.ExecuteLuisQuery(Configuration, Logger, stepContext.Context, cancellationToken)
                    :
                new GlobalDetails();
                return await stepContext.NextAsync(globalDetails, cancellationToken);
            }

        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            // If the child dialog ("GeneralDialog") was cancelled or the user failed to confirm, the Result here will be null.
            if (stepContext.Result != null)
            {
                var result = (GlobalDetails)stepContext.Result;

                var msg = $"Here is what I've found about: \"{result.InfoRequest}\" \n" +
                    $"  {result.InfoRespond}";
                try
                {
                    await stepContext.Context.SendActivityAsync(result.Reply);
                }
                catch (Exception e)
                {
                    Logger.LogWarning($"LUIS Exception: {e.Message} Check your LUIS configuration.");
                }

                await stepContext.Context.SendActivityAsync(MessageFactory.Text("What else can I do for you?"));

                var responseMessage = stepContext.Context.Activity.Text.ToLower();
                if(responseMessage == stopper)
                {
                    return await FinishConversation(stepContext, cancellationToken);
                }
                else
                {
                    stepContext.Context.Activity.Label = "keep_talking";
                    newChat = false;
                    return await ActStepAsync(stepContext, cancellationToken);
                }
            }
            else
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Thank you."), cancellationToken);
                return await FinishConversation(stepContext, cancellationToken);
            }

        }
        private Task<DialogTurnResult> FinishConversation(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            newChat = true;
            return stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }

    }
}
