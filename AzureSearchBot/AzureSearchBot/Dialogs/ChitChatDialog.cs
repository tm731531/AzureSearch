using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using AzureSearchBot.Services;
using AzureSearchBot.Model;
using System.Diagnostics;
using AzureSearchBot.Util;
using System.Collections.Generic;

namespace AzureSearchBot.Dialogs
{
    [Serializable]
    public class ChitChatDialog : IDialog<object>
    {
        private readonly AzureSearchService searchService = new AzureSearchService();
        public async Task StartAsync(IDialogContext context)
        {

           // SearchResult facetResult = await searchService.SearchProductName();
            
                List<string> eras = new List<string>();
                {
                    eras.Add("Choose 1");
                    eras.Add("Choose 2");
                }
                PromptDialog.Choice(context, ChooseProductVersion, eras, "Which product is this question regarding?");
            



            /*-------------------------------------------分隔島-----------------------------------------*/
            //// await context.PostAsync("Type in the name of the musician you are searching for:");
            //var messages = context.MakeMessage();
            //messages.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            //// Activity reply = ((Activity)message).CreateReply();
            //// reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            ////foreach (Value value in searchResult.value)
            //{
            //    List<CardImage> cardImages = new List<CardImage>();
            //    cardImages.Add(new CardImage(url: "https://go.trendmicro.com/sem/sem/www.trendmicro.co.uk/media/image/logo-trendmicro-flat-140.png"));
            //    HeroCard card = new HeroCard()
            //    {
            //        Title = "測試Title",
            //        Subtitle = $"測試Subtitle",
            //        Text = "測試Text",
            //        Images = cardImages
            //    }; HeroCard card1 = new HeroCard()
            //    {
            //        Title = "測試Title1",
            //        Subtitle = $"測試Subtitle1",
            //        Text = "測試Text1",
            //        Images = cardImages
            //    };
            //    messages.Attachments.Add(card.ToAttachment());
            //    messages.Attachments.Add(card1.ToAttachment());
            //}
            //await context.PostAsync(messages);

            //context.Wait(MessageRecievedAsync);
        }


        private async Task ChooseProductVersion(IDialogContext context, IAwaitable<string> result)
        {
            var optionSelected = await result;
            string selectedEra = optionSelected.Split(' ')[0];
            //context.UserData.SetValue("product_name", selectedEra);

            try
            {
                // await context.PostAsync("Type in the name of the musician you are searching for:");
                var messages = context.MakeMessage();
                messages.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                // Activity reply = ((Activity)message).CreateReply();
                // reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                //foreach (Value value in searchResult.value)
                for (int i = 0; i<30;++i)
                {
                    List<CardImage> cardImages = new List<CardImage>();
                    cardImages.Add(new CardImage(url: "https://go.trendmicro.com/sem/sem/www.trendmicro.co.uk/media/image/logo-trendmicro-flat-140.png"));
                    HeroCard card = new HeroCard()
                    {
                        Title = $"測試Title{i}",
                        Subtitle = $"測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle" + $"測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle" +
                        $"測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle" +
                        $"測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle" + $"測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle" +
                        $"測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle" +
                        $"測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle" +
                        $"測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle" +
                        $"測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle" +
                        $"測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle" + $"測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle" + $"測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle" +
                        $"測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle" + $"測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle" +
                        $"測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle" +
$"測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle測試Subtitle{i}",
                        Text = $"測試Text{i}",
                        Images = cardImages
                    };
                    messages.Attachments.Add(card.ToAttachment());
                    
                }
                await context.PostAsync(messages);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error when filtering by genre: {e}");
            }
        }

        public async Task StartAsync2(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            // await context.PostAsync("Type in the name of the musician you are searching for:");
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
                messages.Attachments.Add(card.ToAttachment());
                messages.Attachments.Add(card1.ToAttachment());
            }
            await context.PostAsync(messages);

           // context.Wait(MessageRecievedAsync);
        }
        public virtual async Task MessageRecievedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            try
            {
                SearchResult searchResult = new SearchResult();
                //CardUtil.showHeroCardTest(message, searchResult , context);
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
                    messages.Attachments.Add(card.ToAttachment());
                    messages.Attachments.Add(card1.ToAttachment());
                }
                 await context.PostAsync(messages);
                //SearchResult searchResult = await searchService.SearchByName(message.Text);
                //if(searchResult.value.Length != 0)
                //{
                //    CardUtil.showHeroCard(message, searchResult);
                //}
                //else{
                //    await context.PostAsync($"No musicians by the name {message.Text} found");
                //}
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error when searching for musician: {e.Message}");
            }
            context.Done<object>(null);
        }
    }
}