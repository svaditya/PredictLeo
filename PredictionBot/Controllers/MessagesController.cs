using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;

namespace PredictionBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>

      

        internal static IDialog<InputForm> MakeRootDialog()
        {
            return Chain.From(() => FormDialog.FromForm(InputForm.BuildForm));
        }



        [ResponseType(typeof(void))]
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                //1  ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                //------This is for Dialog
                // 2 Call Azure Machine Learning           

                //   2 string strRet = await AzureML.Program.InvokeRequestResponseService(activity.Text);

                // 2 return our reply to the user
                //    2 Activity reply = activity.CreateReply($"The predicted value is {strRet}");

                // 2  await connector.Conversations.ReplyToActivityAsync(reply);

                // 3 await Conversation.SendAsync(activity, () => new EchoDialog());

                // ----------------- This is for Form Flow
                await Conversation.SendAsync(activity, MakeRootDialog);

            }
            else
            {
                HandleSystemMessage(activity);
            }
         //   var response = Request.CreateResponse(HttpStatusCode.OK);
          //  return response;
            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }

 
}