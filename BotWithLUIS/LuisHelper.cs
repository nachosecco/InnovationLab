// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.3.0

using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

                var attachments = new List<Attachment>();
                // Reply to the activity we received with an activity.
                details.Reply = MessageFactory.Attachment(attachments);


                if (intent == "ShowBenefitsContactInfo")
                {
                    string textBody = "Deserialized body for the \'benefitsContact\' intent performed by Raidell's method.";
                    details.Reply.Text += "Llamada a show benefitsContact";
                    details.Reply.Attachments.Add(Cards.GetBenefitsContactCard(textBody).ToAttachment());
                }

                if (intent == "ShowBenefits")
                {
                    string textBody = "Deserialized body for the \'benefits\' intent performed by Raidell's method.";
                    details.Reply.Text += textBody;
                    details.Reply.Attachments.Add(Cards.GetBenefitsCard().ToAttachment());
                    
                }

                if (intent == "ShowBenefitsWFH")
                {
                    string textBody = "Deserialized body for the \'benefitsWFH\' intent performed by Raidell's method.";
                    details.Reply.Text += "Llamada a show benefitsWFH";
                    details.Reply.Attachments.Add(Cards.GetBenefitsWFHCard(textBody).ToAttachment());
                }

                if (intent == "ShowCommunities")
                {
                    string textBody = "Deserialized body for the \'Communities\' intent performed by Raidell's method.";
                    details.Reply.Text += "Llamada a show Communities";
                    details.Reply.Attachments.Add(Cards.GetCommunitiesCard(textBody).ToAttachment());
                }

                if (intent == "ShowCompanyInfo")
                {
                    string textBody = "Deserialized body for the \'Company\' intent performed by Raidell's method.";
                    details.Reply.Text += "Llamada a show Company";
                    details.Reply.Attachments.Add(Cards.GetCompanyCard(textBody).ToAttachment());
                }

                if (intent == "ShowDaysOff")
                {
                    string textBody = "Deserialized body for the \'DaysOff\' intent performed by Raidell's method.";
                    details.Reply.Text += "Llamada a show DaysOff";
                   // details.Reply.Attachments.Add(Cards.GetDaysOffCard(textBody).ToAttachment());
                }

                if (intent == "ShowReferralsInfo")
                {
                    string textBody = "Deserialized body for the \'Referrals\' intent performed by Raidell's method.";
                    details.Reply.Text += "Llamada a show Referrals";
                    //details.Reply.Attachments.Add(Cards.GetReferralsCard(textBody).ToAttachment());
                }

                if (intent == "ShowServiceDeskSupport")
                {
                    string textBody = "Deserialized body for the \'ServiceDeskSupport\' intent performed by Raidell's method.";
                    details.Reply.Text += "Llamada a show ServiceDeskSupport";
                    //details.Reply.Attachments.Add(Cards.GetServiceDeskSupportCard(textBody).ToAttachment());
                }

                if (intent == "None")
                {
                    string textBody = "Deserialized body for the \'benefitsContact\' intent performed by Raidell's method.";
                    details.Reply.Text += "Llamada a show benefitsContact";
                    details.Reply.Attachments.Add(Cards.GetBenefitsContactCard(textBody).ToAttachment());
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
