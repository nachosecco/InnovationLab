using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BotWithLUIS
{
    public static class Cards
    {
        public static Attachment CreateAdaptiveCardAttachment()
        {
            // combine path for cross platform support
            string[] paths = { ".", "Cards", "benefitsCard.json" };
            var adaptiveCardJson = File.ReadAllText(Path.Combine(paths));

            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };
            return adaptiveCardAttachment;
        }

        public static HeroCard GetWelcomeCard()
        {
            var welcomeCard = new HeroCard
            {
                Title = "Endava's ChatBoarding Bot",
                Subtitle = "InnovationLabs",
                Text = "Built to help you on the process of OnBoarding to Endava. " +
                       "I will fetch you only important info from Office 365 mail and other popular services.",
                Images = new List<CardImage> { new CardImage("https://is4-ssl.mzstatic.com/image/thumb/Purple113/v4/41/f9/5f/41f95fdd-90d0-365e-2a35-2f847826b9aa/AppIcon-0-1x_U007emarketing-0-0-85-220-7.png/320x0w.jpg") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Get an overview of the company", value: "https://www.endava.com/"), new CardAction(ActionTypes.OpenUrl, "Know where are our offices", value: "https://www.google.com/search?rlz=1C1CHBF_esUY850UY850&biw=1536&bih=754&q=endava+uruguay&npsic=0&rflfq=1&rlha=0&rllag=-34905902,-56194884,141&tbm=lcl&ved=2ahUKEwjlwviw35jjAhUQIbkGHQEnAhgQtgN6BAgKEAQ&tbs=lrf:!2m4!1e17!4m2!17m1!1e2!2m1!1e3!3sIAE,lf:1,lf_ui:4&rldoc=1#rlfi=hd:;si:;mv:!1m2!1d-34.9054849!2d-56.193191799999994!2m2!1d-34.9062491!2d-56.196577999999995!3m12!1m3!1d374.9193153319924!2d-56.1948849!3d-34.905867!2m3!1f0!2f0!3f0!3m2!1i79!2i22!4f13.1;tbs:lrf:!2m1!1e3!2m4!1e17!4m2!17m1!1e2!3sIAE,lf:1,lf_ui:4"), new CardAction(ActionTypes.OpenUrl, "Know a little bit more about it..", value: "https://endava.sharepoint.com/") },
            };

            return welcomeCard;
        }

        public static ThumbnailCard GetBenefitsWFHCard(string textBody)
        {
            var benefitsWFHCard = new ThumbnailCard
            {
                Title = "Benefits of Endavans",
                Subtitle = "We believe in Home Office!",
                Text = textBody,
                Images = new List<CardImage> { new CardImage("https://i.ytimg.com/vi/eDuAqIsfKds/maxresdefault.jpg") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Show me more.", value: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRTujZhHZ6BUq1J9mi0IkuCvqCmtoQN6JkWB3gOS90KkOupuIAe") },
            };

            return benefitsWFHCard;
        }

        public static ThumbnailCard GetBenefitsContactCard(string textBody)
        {
            var benefitsContactCard = new ThumbnailCard
            {
                Title = "Benefits of Endavans",
                Subtitle = "Who to talk about:",
                Text = textBody,
                Images = new List<CardImage> { new CardImage("https://sec.ch9.ms/ch9/7ff5/e07cfef0-aa3b-40bb-9baa-7c9ef8ff7ff5/buildreactionbotframework_960.jpg") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "How to manage them.", value: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRTujZhHZ6BUq1J9mi0IkuCvqCmtoQN6JkWB3gOS90KkOupuIAe") },
            };

            return benefitsContactCard;
        }
        public static ReceiptCard GetBenefitsCard(string textBody)
        {
            var receiptCard = new ReceiptCard
            {
                Title = "Benefits of Endavans:",
                Items = new List<ReceiptItem>
                {
                    new ReceiptItem(
                        "Gym allowance",
                        quantity: "$1150",
                        image: new CardImage(url: "http://icons.iconarchive.com/icons/sonya/swarm/256/gym-icon.png")),
                    new ReceiptItem(
                        "Books allowance",
                        quantity: "$1150",
                        image: new CardImage(url: "https://www.clipartmax.com/png/middle/254-2544942_ebooks-white-papers-open-book-icon.png")),
                    new ReceiptItem(
                        "Child care",
                        quantity: "$1150",
                        image: new CardImage(url: "https://www.nottingham.ac.uk/sharedresources/images/iconography/icon-childcare.png")),
                },
            };

            return receiptCard;
        }
        public static ThumbnailCard GetBenefitsDescriptionCard(string textBody)
        {
            var companyCard = new ThumbnailCard
            {
                Title = "Description of the benefits",
                Text = textBody,
                Images = new List<CardImage> { new CardImage("https://s2.studylib.net/store/data/005525748_1-bb9ce29028162a7ed07a21c641cffd86.png") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Be more..", value: "https://www.endava.com/en") },
            };

            return companyCard;
        }
        public static HeroCard GetCommunitiesCard(string textBody)
        {
            var communitiesCard = new HeroCard
            {
                Title = "Endava's Communities",
                Subtitle = "Technical communities",
                Text = textBody,
                Images = new List<CardImage> { new CardImage("https://www.endava.com/-/media/EndavaDigital/Endava/Images/ImagesWithOurPeople/Desktop/Inner_650x650_SS14-B.ashx") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Let's get you started!", value: "https://confluence.endava.com/") },
            };

            return communitiesCard;
        }

        public static ThumbnailCard GetCompanyCard(string textBody)
        {
            var companyCard = new ThumbnailCard
            {
                Title = "Endava",
                Subtitle = "Welcome to Endava",
                Text = textBody,
                Images = new List<CardImage> { new CardImage("https://s2.studylib.net/store/data/005525748_1-bb9ce29028162a7ed07a21c641cffd86.png") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Be more..", value: "https://www.endava.com/en") },
            };

            return companyCard;
        }

        public static ThumbnailCard GetDaysOffCard(string textBody)
        {
            var daysOff = new ThumbnailCard
            {
                Title = "Days Off availability",
                Subtitle = "Welcome to Endava",
                Text = textBody,
                Images = new List<CardImage> { new CardImage("https://s2.studylib.net/store/data/005525748_1-bb9ce29028162a7ed07a21c641cffd86.png") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Be more..", value: "https://www.endava.com/en") },
            };

            return daysOff;
        }

        public static SigninCard GetSigninCard()
        {
            var signinCard = new SigninCard
            {
                Text = "BotFramework Sign-in Card",
                Buttons = new List<CardAction> { new CardAction(ActionTypes.Signin, "Sign-in", value: "https://login.microsoftonline.com/") },
            };

            return signinCard;
        }

        public static AnimationCard GetAnimationCard()
        {
            var animationCard = new AnimationCard
            {
                Title = "Microsoft Bot Framework",
                Subtitle = "Animation Card",
                Image = new ThumbnailUrl
                {
                    Url = "https://docs.microsoft.com/en-us/bot-framework/media/how-it-works/architecture-resize.png",
                },
                Media = new List<MediaUrl>
                {
                    new MediaUrl()
                    {
                        Url = "http://i.giphy.com/Ki55RUbOV5njy.gif",
                    },
                },
            };

            return animationCard;
        }

        public static VideoCard GetVideoCard()
        {
            var videoCard = new VideoCard
            {
                Title = "Big Buck Bunny",
                Subtitle = "by the Blender Institute",
                Text = "Big Buck Bunny (code-named Peach) is a short computer-animated comedy film by the Blender Institute," +
                       " part of the Blender Foundation. Like the foundation's previous film Elephants Dream," +
                       " the film was made using Blender, a free software application for animation made by the same foundation." +
                       " It was released as an open-source film under Creative Commons License Attribution 3.0.",
                Image = new ThumbnailUrl
                {
                    Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c5/Big_buck_bunny_poster_big.jpg/220px-Big_buck_bunny_poster_big.jpg",
                },
                Media = new List<MediaUrl>
                {
                    new MediaUrl()
                    {
                        Url = "http://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4",
                    },
                },
                Buttons = new List<CardAction>
                {
                    new CardAction()
                    {
                        Title = "Learn More",
                        Type = ActionTypes.OpenUrl,
                        Value = "https://peach.blender.org/",
                    },
                },
            };

            return videoCard;
        }

        public static AudioCard GetAudioCard()
        {
            var audioCard = new AudioCard
            {
                Title = "I am your father",
                Subtitle = "Star Wars: Episode V - The Empire Strikes Back",
                Text = "The Empire Strikes Back (also known as Star Wars: Episode V – The Empire Strikes Back)" +
                       " is a 1980 American epic space opera film directed by Irvin Kershner. Leigh Brackett and" +
                       " Lawrence Kasdan wrote the screenplay, with George Lucas writing the film's story and serving" +
                       " as executive producer. The second installment in the original Star Wars trilogy, it was produced" +
                       " by Gary Kurtz for Lucasfilm Ltd. and stars Mark Hamill, Harrison Ford, Carrie Fisher, Billy Dee Williams," +
                       " Anthony Daniels, David Prowse, Kenny Baker, Peter Mayhew and Frank Oz.",
                Image = new ThumbnailUrl
                {
                    Url = "https://upload.wikimedia.org/wikipedia/en/3/3c/SW_-_Empire_Strikes_Back.jpg",
                },
                Media = new List<MediaUrl>
                {
                    new MediaUrl()
                    {
                        Url = "http://www.wavlist.com/movies/004/father.wav",
                    },
                },
                Buttons = new List<CardAction>
                {
                    new CardAction()
                    {
                        Title = "Read More",
                        Type = ActionTypes.OpenUrl,
                        Value = "https://en.wikipedia.org/wiki/The_Empire_Strikes_Back",
                    },
                },
            };

            return audioCard;
        }
    }
}