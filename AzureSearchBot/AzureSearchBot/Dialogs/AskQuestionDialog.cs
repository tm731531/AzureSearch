using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using AzureSearchBot.Services;
using Microsoft.Bot.Connector;
using System.Diagnostics;
using AzureSearchBot.Model;
using System.Collections.Generic;
using AzureSearchBot.Util;
using System.Linq;

namespace AzureSearchBot.Dialogs
{
    [Serializable]
    public class AskQuestionDialog : IDialog<object>
    {
        private readonly AzureSearchService searchService = new AzureSearchService();
        public async Task StartAsync(IDialogContext context)
        {

            try
            {
                SearchResult facetResult = await searchService.SearchProductName();
                if (facetResult.value.Length != 0)
                {
                    List<string> eras = new List<string>();
                    foreach (Product_Name era in facetResult.searchfacets.Product_Name)
                    {
                        eras.Add($"{era.value} ({era.count})");
                    }
                    PromptDialog.Choice(context, ChooseProductVersion, eras, "Which product is this question regarding?");
                }
                else
                {
                    await context.PostAsync("I couldn't find any product to show you");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error when faceting by era: {e}");
            }
        }

        private async Task ChooseProductVersion(IDialogContext context, IAwaitable<string> result)
        {
            var optionSelected = await result;
            string selectedEra = optionSelected.Split(' ')[0];
            context.UserData.SetValue("product_name", selectedEra);

            try
            {
                SearchResult facetResult = await searchService.SearchProductVersion(selectedEra);//.FetchFacets();
                if (facetResult.value.Length != 0)
                {
                    List<string> eras = new List<string>();
                    foreach (Product_Version era in facetResult.searchfacets.Product_Version)
                    {
                        eras.Add($"{era.value} ({era.count})");
                    }
                    PromptDialog.Choice(context, TypeDescription, eras, "Which version is this question regarding?");
                }
                else
                {
                    await context.PostAsync("I couldn't find any version to show you");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error when filtering by genre: {e}");
            }
        }
        //private async Task ChooseProductOS(IDialogContext context, IAwaitable<string> result)
        //{
        //    var optionSelected = await result;
        //    string selectedEra = optionSelected.Split(' ')[0];
        //    context.UserData.SetValue("product_version", selectedEra);
        //    string productName = "";
        //    context.UserData.TryGetValue("product_name", out productName);//.SetValue("product_version", selectedEra);

        //    try
        //    {
        //        SearchResult facetResult = await searchService.SearchProductOS(productName, selectedEra);//.FetchFacets();
        //        if (facetResult.value.Length != 0)
        //        {
        //            List<string> eras = new List<string>();
        //            foreach (Product_OS era in facetResult.searchfacets.Product_OS)
        //            {
        //                eras.Add($"{era.value} ({era.count})");
        //            }
        //            PromptDialog.Choice(context, TypeDescription, eras, "Which OS is this question regarding?");
        //        }
        //        else
        //        {
        //            await context.PostAsync("I couldn't find any product OS to show you");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine($"Error when filtering by genre: {e}");
        //    }
        //}
        private async Task TypeDescription(IDialogContext context, IAwaitable<string> result)
        {
            var optionSelected = await result;
            string selectedEra = optionSelected.Split(' ')[0];
            context.UserData.SetValue("product_version", selectedEra);
            //context.UserData.SetValue("product_os", selectedEra);
            try
            {
                PromptDialog.Text(context, FinalResponse, "What is your question?");

            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error when filtering by genre: {e}");
            }
        }


        private async Task FinalResponse(IDialogContext context, IAwaitable<string> result)
        {
            var optionSelected = await result;
            string selectedEra = optionSelected;// optionSelected.Split(' ')[0];
            string productName = "", productVersion = "", productOS = "";
            context.UserData.TryGetValue("product_name", out productName);//.SetValue("product_version", selectedEra);
            context.UserData.TryGetValue("product_version", out productVersion);
           // context.UserData.TryGetValue("product_os", out productOS);



            try
            {
                SearchResult searchResult = await searchService.SearchFinal(productName, productVersion, productOS, selectedEra);
                if (searchResult.value.Length != 0)
                {

                    var messages = context.MakeMessage();
                    messages.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                    //Activity reply = ((Activity)message).CreateReply();
                    //reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                    //reply.AttachmentLayout = AttachmentLayoutTypes.List;
                    //reply.Text = "";
                    //Make each Card for each musician
                   foreach (Value value in searchResult.value.OrderByDescending(x=>x.searchscore).Take(3))
                    {
                        //Value value = ;
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
                        messages.Attachments.Add(card.ToAttachment());
                    }
                    await context.PostAsync(messages);
                    ////ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                    ////await connector.Conversations.ReplyToActivityAsync(reply);
                    //////make connector and reply message
                    //ConnectorClient connector = new ConnectorClient(new Uri(reply.ServiceUrl));
                    //// await context.PostAsync(messages);
                    //await connector.Conversations.SendToConversationAsync(reply);
                    //CardUtil.showHeroCard((IMessageActivity)context.Activity, searchResult, context);
                }
                else
                {
                    await context.PostAsync($"I couldn't find any musicians in that era :0");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error when filtering by genre: {e}");
            }
            context.Done<object>(null);
        }
    }
}