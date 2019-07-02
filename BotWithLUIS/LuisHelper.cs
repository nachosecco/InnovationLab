// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.3.0

using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BotWithLUIS
{
    public static class LuisHelper
    {
        public static async Task<GlobalDetails> ExecuteLuisQuery(IConfiguration configuration, ILogger logger, ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var details = new GlobalDetails();
            StartConversation(details);

            try
            {
                // Create the LUIS settings from configuration.
                var luisApplication = new LuisApplication(
                    configuration["LuisAppId"],
                    configuration["LuisAPIKey"],
                    "https://" + configuration["LuisAPIHostName"]
                );

                var recognizer = new LuisRecognizer(luisApplication);

                // The actual call to LUIS
                var recognizerResult = await recognizer.RecognizeAsync(turnContext, cancellationToken);

                var (intent, score) = recognizerResult.GetTopScoringIntent();

                details.InfoRequest = recognizerResult.Text;
                details.ActualIntent = intent;

                if (intent == "ShowBenefitsContactInfo")
                {
                    details.InfoRespond = "Llamada a show benefits contacts" ;
                }

                if (intent == "ShowBenefits")
                {
                    details.InfoRespond = "Llamada a show benefits";
                }

                if (intent == "ShowBenefitsWFH")
                {
                    details.InfoRespond = "Llamada a show benefits WFH";
                }

                if (intent == "ShowCommunities")
                {
                    details.InfoRespond = "Llamada a show communities";
                }

                if (intent == "ShowCompanyInfo")
                {
                    details.InfoRespond = "Llamada a show Company Info";
                }

                if (intent == "ShowDaysOff")
                {
                    details.InfoRespond = "Llamada a show Days Off";
                }

                if (intent == "ShowReferralsInfo")
                {
                    details.InfoRespond = "Llamada a show Referrals Info ";
                }

                if (intent == "ShowServiceDeskSupport")
                {
                    details.InfoRespond = "Llamada a Show Service Desk Support ";
                }

                if (intent == "Thanks")
                {
                    details.InfoRespond = "Setear intent null para seguir la charla?";
                }
                if (intent == "None")
                {
                    details.InfoRespond = "No encontró una Intent predefinida. ";
                }
            }
            catch (Exception e)
            {
                logger.LogWarning($"LUIS Exception: {e.Message} Check your LUIS configuration.");
            }

            return details;
        }

        public static void StartConversation(GlobalDetails details)
        {
            details.ConversationStarted = true;
        }
    
    }
}
