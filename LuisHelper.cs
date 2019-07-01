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


                if (intent == "ShowBenefitsContactInfo")
                {
                    details.InfoRequest = recognizerResult.Entities["Info"]?.FirstOrDefault()?["Request"]?.FirstOrDefault()?.FirstOrDefault()?.ToString();

                }

                if (intent == "ShowBenefits")
                {
                    details.InfoRequest = recognizerResult.Entities["Info"]?.FirstOrDefault()?["Request"]?.FirstOrDefault()?.FirstOrDefault()?.ToString();
                    details.InfoRespond = "asijdoaiusdoiajslkdjajjjjalskdjasds";
                }

                if (intent == "ShowBenefitsWFH")
                {
                    details.InfoRequest = recognizerResult.Entities["Info"]?.FirstOrDefault()?["Request"]?.FirstOrDefault()?.FirstOrDefault()?.ToString();
                }

                if (intent == "ShowCommunities")
                {
                    details.InfoRequest = recognizerResult.Entities["Info"]?.FirstOrDefault()?["Request"]?.FirstOrDefault()?.FirstOrDefault()?.ToString();
                }

                if (intent == "ShowCompanyInfo")
                {
                    details.InfoRequest = recognizerResult.Entities["Info"]?.FirstOrDefault()?["Request"]?.FirstOrDefault()?.FirstOrDefault()?.ToString();
                }

                if (intent == "ShowDaysOff")
                {
                    details.InfoRequest = recognizerResult.Entities["Info"]?.FirstOrDefault()?["Request"]?.FirstOrDefault()?.FirstOrDefault()?.ToString();

                }
                if (intent == "None")
                {
                    details.InfoRespond = "asijdoaiusdoiajslkdjajjjjalskdjasds";

                }
            }
            catch (Exception e)
            {
                logger.LogWarning($"LUIS Exception: {e.Message} Check your LUIS configuration.");
            }

            return details;
        }
    }
}
