using System;
using System.Collections.Generic;
using Microsoft.Bot.Connector;
using AzureSearchBot.Model;
using Microsoft.Bot.Builder.Dialogs;

namespace AzureSearchBot.Util
{
    public static class CardUtil
    {
        public static async void showHeroCard(IMessageActivity message, SearchResult searchResult, IDialogContext context)
        {
            //Make reply activity and set layout
            var messages = context.MakeMessage();
            messages.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            Activity reply = ((Activity)message).CreateReply();
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            //reply.AttachmentLayout = AttachmentLayoutTypes.List;
            //reply.Text = "";
            //Make each Card for each musician
            foreach (Value value in searchResult.value)
            {
                List<CardImage> cardImages = new List<CardImage>();
                cardImages.Add(new CardImage(url: "https://go.trendmicro.com/sem/sem/www.trendmicro.co.uk/media/image/logo-trendmicro-flat-140.png"));
                //List<CardAction> cardAction = new List<CardAction>();
                //cardAction.Add(new CardAction(Image: "https://go.trendmicro.com/sem/sem/www.trendmicro.co.uk/media/image/logo-trendmicro-flat-140.png"));
                HeroCard card = new HeroCard()
                {
                    Title = value.FAQ_ID_NEW,
                    Subtitle = $"Product_Name: {value.Product_Name } | Score: {value.searchscore} | ID : {value.ID}",
                    Text = value.Description,
                    Images = cardImages
                    // Buttons= cardAction
                };
                //ThumbnailCard plCard = new ThumbnailCard()
                //{
                //    Title = "I'm a thumbnail card",
                //    Subtitle = "Pig Latin Wikipedia Page",
                //    Images = cardImages,
                //    //   Buttons = cardButtons
                //};
                reply.Attachments.Add(card.ToAttachment());
                messages.Attachments.Add(card.ToAttachment());
                // reply.Text += $"FAQ_ID: {value.FAQ_ID } |Product_Name: {value.Product_Name } | Score: {value.searchscore} | ID : {value.ID}\n";
                // reply.Text += value.Description+ "\n\n\n\n";
            }
            //ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

            //await connector.Conversations.ReplyToActivityAsync(reply);
            ////make connector and reply message
            ConnectorClient connector = new ConnectorClient(new Uri(reply.ServiceUrl));
            // await context.PostAsync(messages);
            await connector.Conversations.SendToConversationAsync(reply);
        }

        public static async void showHeroCardTest(IMessageActivity message, SearchResult searchResult, IDialogContext context)
        {
            var messages = context.MakeMessage();
            messages.AttachmentLayout = AttachmentLayoutTypes.Carousel;
           // Activity reply = ((Activity)message).CreateReply();
           // reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            //foreach (Value value in searchResult.value)
            {
                List<CardImage> cardImages = new List<CardImage>();
                cardImages.Add(new CardImage(url: "https://go.trendmicro.com/sem/sem/www.trendmicro.co.uk/media/image/logo-trendmicro-flat-140.png"));
                HeroCard card = new HeroCard()
                {
                    Title = "測試Title",
                    Subtitle = $"測試Subtitle",
                    Text = "測試Text",
                    Images = cardImages
                }; HeroCard card1 = new HeroCard()
                {
                    Title = "測試Title1",
                    Subtitle = $"測試Subtitle1",
                    Text = "測試Text1",
                    Images = cardImages
                };
             //   reply.Attachments.Add(card.ToAttachment());
             //   reply.Attachments.Add(card1.ToAttachment());
                messages.Attachments.Add(card.ToAttachment());
                messages.Attachments.Add(card1.ToAttachment());
            }
            //ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

            //await connector.Conversations.ReplyToActivityAsync(reply);
            ////make connector and reply message
            //ConnectorClient connector = new ConnectorClient(new Uri(reply.ServiceUrl));
            await context.PostAsync(messages);
            //await connector.Conversations.SendToConversationAsync(reply);
        }
        public static async void showHeroCard(IActivity message, SearchResult searchResult)
        {
            //Make reply activity and set layout
            Activity reply = ((Activity)message).CreateReply();
            // reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            //reply.AttachmentLayout = AttachmentLayoutTypes.List;

            ////Make each Card for each musician
            //foreach (Value value in searchResult.value)
            //{
            //    List<CardImage> cardImages = new List<CardImage>();
            //    cardImages.Add(new CardImage(url: "https://go.trendmicro.com/sem/sem/www.trendmicro.co.uk/media/image/logo-trendmicro-flat-140.png"));
            //    //List<CardAction> cardAction = new List<CardAction>();
            //    //cardAction.Add(new CardAction(Image: "https://go.trendmicro.com/sem/sem/www.trendmicro.co.uk/media/image/logo-trendmicro-flat-140.png"));
            //    //HeroCard card = new HeroCard()
            //    //{
            //    //    Title = value.FAQ_ID,
            //    //    Subtitle = $"Product_Name: {value.Product_Name } | Score: {value.searchscore} | ID : {value.ID}",
            //    //    Text = value.Description,
            //    //    Images = cardImages
            //    //   // Buttons= cardAction
            //    //};
            //    ThumbnailCard plCard = new ThumbnailCard()
            //    {
            //        Title = "I'm a thumbnail card",
            //        Subtitle = "Pig Latin Wikipedia Page",
            //        Images = cardImages,
            //        //   Buttons = cardButtons
            //    };
            //    reply.Attachments.Add(plCard.ToAttachment());
            //}
            //ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

            //await connector.Conversations.ReplyToActivityAsync(reply);
            ////make connector and reply message
            ConnectorClient connector = new ConnectorClient(new Uri(reply.ServiceUrl));
            reply.Text = "OK";
            await connector.Conversations.SendToConversationAsync(reply);
        }
    }
}