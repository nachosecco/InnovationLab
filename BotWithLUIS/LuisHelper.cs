// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.3.0

using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
                string textBody = GetIntentionValue(logger,intent);

                var attachments = new List<Attachment>();
                // Reply to the activity we received with an activity.
                details.Reply = MessageFactory.Attachment(attachments);


                if (intent == "ShowBenefitsContactInfo")
                {

                    details.Reply.Attachments.Add(Cards.GetBenefitsContactCard(textBody).ToAttachment());
                }

                if (intent == "ShowBenefits")
                {
                    details.Reply.Attachments.Add(Cards.GetBenefitsCard(textBody).ToAttachment());
                    details.Reply.Attachments.Add(Cards.GetBenefitsDescriptionCard(textBody).ToAttachment());

                }

                if (intent == "ShowBenefitsWFH")
                {
                    details.Reply.Attachments.Add(Cards.GetBenefitsWFHCard(textBody).ToAttachment());
                }

                if (intent == "ShowCommunities")
                {
                    details.Reply.Attachments.Add(Cards.GetCommunitiesCard(textBody).ToAttachment());
                }

                if (intent == "ShowCompanyInfo")
                {
                    details.Reply.Attachments.Add(Cards.GetCompanyCard(textBody).ToAttachment());
                }

                if (intent == "ShowDaysOff")
                {

                    details.Reply.Attachments.Add(Cards.GetDaysOffCard(textBody).ToAttachment());
                }

                if (intent == "ShowReferralsInfo")
                {
                    details.Reply.Attachments.Add(Cards.GetReferralsCard(textBody).ToAttachment());
                }

                if (intent == "ShowServiceDeskSupport")
                {
                    details.Reply.Attachments.Add(Cards.GetServiceDeskSupportCard(textBody).ToAttachment());
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

        private static string GetIntentionValue(ILogger logger, string intention)
        {
            try
            {

                string[] paths = { ".", "IntentionsInfo", "intentionsInfo.json" };
                string fullPath = Path.Combine(paths);
                using (StreamReader r = new StreamReader(fullPath))
                {
                    var json = r.ReadToEnd();
                    var jobj = JObject.Parse(json);
                    foreach (var item in jobj.Properties())
                    {
                        if (item.Name == intention)
                        {
                            return item.Value.ToString();
                        }
                    }

                } return string.Empty;
            }
            catch (Exception e)
            {
                logger.LogWarning($"LUIS Exception: {e.Message} Check your LUIS configuration.");
                return "";
            }
            
        }
        
        public static void StartConversation(GlobalDetails details)
        {
            details.ConversationStarted = true;
        }
        
    }
}
